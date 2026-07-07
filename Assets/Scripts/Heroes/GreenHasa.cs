using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenHasa : Hero
{
    [Header("스킬 1 최종: 원산폭격")]
    public GameObject nuclearWarningVFX;    // 경고 원 VFX (2초 표시)
    public GameObject nuclearExplosionVFX;  // 폭발 VFX

    [Header("스킬 2 최종: 융단폭격")]
    public GameObject carpetExplosionVFX;   // 개별 폭발 VFX

    [Header("스킬 3 최종: 플레임타워")]
    public GameObject flameTowerPrefab;

    [Header("네이팜 (스킬 3 - hasNapalm)")]
    public GameObject napalmVFX;

    [HideInInspector] public int clusterCount;
    [HideInInspector] public bool hasNapalm;
    [HideInInspector] public float napalmDamageRatio = 0.3f;
    [HideInInspector] public float napalmDuration = 5f;
    [HideInInspector] public float napalmTickInterval = 0.6f;

    [Header("Skill Data")]
    public SkillDataSO explosionSkillData;
    public SkillDataSO clusterSkillData;
    public SkillDataSO napalmSkillData;

    protected override void Init()
    {
        attackBehavior = new BazookaAttackBehavior();
    }

    public override List<SkillBase> GetSkillCandidates() => new()
    {
        new GreenHasaExplosionSkill(explosionSkillData),
        new GreenHasaClusterSkill(clusterSkillData),
        new GreenHasaNapalmSkill(napalmSkillData)
    };

    public void StartOhsanBombing(SkillDataSO data)   => StartCoroutine(OhsanBombingLoop(data));
    public void StartCarpetBombing(SkillDataSO data)  => StartCoroutine(CarpetBombingLoop(data));
    public void StartFlameTowerLoop(SkillDataSO data) => StartCoroutine(FlameTowerLoop(data));

    // 원산폭격: data.loopInterval초마다 랜덤 몬스터 위치에 경고 원 → 폭발
    IEnumerator OhsanBombingLoop(SkillDataSO data)
    {
        while (true)
        {
            yield return new WaitForSeconds(data.loopInterval);

            var monsters = FindObjectsByType<Monster>(FindObjectsSortMode.None);
            Vector3 targetPos = monsters.Length > 0
                ? monsters[Random.Range(0, monsters.Length)].transform.position
                : transform.position;
            targetPos.y = 0f;

            // 경고 원 표시
            GameObject warning = null;
            if (nuclearWarningVFX != null)
                warning = Instantiate(nuclearWarningVFX, targetPos, Quaternion.identity);

            yield return new WaitForSeconds(data.warningDuration);

            if (warning != null) Destroy(warning);

            // 폭발 VFX + 데미지
            SpawnExplosionVFX(nuclearExplosionVFX, targetPos);
            float radius = stats.explosionRadius * data.radiusMultiplier;
            int monsterLayer = LayerMask.GetMask("Monster");
            var cols = Physics.OverlapSphere(targetPos, radius, monsterLayer);
            foreach (var col in cols)
                if (col.TryGetComponent(out Monster m))
                    m.TakeDamage(stats.attackDamage * data.damageMultiplier);
        }
    }

    // 융단폭격: data.loopInterval초마다 무작위 방향 1자 라인 경고 → 순차 폭발
    IEnumerator CarpetBombingLoop(SkillDataSO data)
    {
        int bombCount = data.bombCount;
        float spacing = data.bombSpacing;

        while (true)
        {
            yield return new WaitForSeconds(data.loopInterval);

            Vector3 dir = Random.insideUnitSphere;
            dir.y = 0f;
            dir.Normalize();

            Vector3[] positions = new Vector3[bombCount];
            for (int i = 0; i < bombCount; i++)
                positions[i] = transform.position + dir * (i * spacing);

            // LineRenderer로 1자 라인 표시
            GameObject lineGO = CreateLineIndicator(positions[0], positions[bombCount - 1]);

            yield return new WaitForSeconds(data.warningDuration);

            if (lineGO != null) Destroy(lineGO);

            // 순차 폭발
            int monsterLayer = LayerMask.GetMask("Monster");
            float bombRadius = stats.explosionRadius * data.radiusMultiplier;
            for (int i = 0; i < bombCount; i++)
            {
                SpawnExplosionVFX(carpetExplosionVFX, positions[i]);
                var cols = Physics.OverlapSphere(positions[i], bombRadius, monsterLayer);
                foreach (var col in cols)
                    if (col.TryGetComponent(out Monster m))
                        m.TakeDamage(stats.attackDamage * data.damageMultiplier);

                yield return new WaitForSeconds(data.bombDelay);
            }
        }
    }

    // 플레임타워: 최종 승급 즉시 1개 설치 후, data.loopInterval초마다 추가 설치 (data.lifetime초 지속)
    IEnumerator FlameTowerLoop(SkillDataSO data)
    {
        while (true)
        {
            Vector3 rand = Random.insideUnitSphere * data.spawnRange;
            rand.y = 0f;
            FlameTower.Spawn(flameTowerPrefab, rand, stats.attackDamage * data.damageMultiplier, data.lifetime);

            yield return new WaitForSeconds(data.loopInterval);
        }
    }

    // LineRenderer로 융단폭격 라인 인디케이터 생성
    GameObject CreateLineIndicator(Vector3 start, Vector3 end)
    {
        var go = new GameObject("CarpetLine");
        var lr = go.AddComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPosition(0, start + Vector3.up * 0.05f);
        lr.SetPosition(1, end   + Vector3.up * 0.05f);
        lr.startWidth = 0.3f;
        lr.endWidth   = 0.3f;
        lr.useWorldSpace = true;

        // 머티리얼 없으면 기본 Unlit/Color 사용
        var mat = new Material(Shader.Find("Unlit/Color"));
        mat.color = new Color(1f, 0.3f, 0f, 0.9f); // 주황색
        lr.material = mat;

        return go;
    }

    void SpawnExplosionVFX(GameObject vfxPrefab, Vector3 pos)
    {
        if (vfxPrefab == null) return;
        var vfx = Instantiate(vfxPrefab, pos, Quaternion.identity);
        if (!vfx.TryGetComponent<ParticleSystem>(out var ps))
            Destroy(vfx, 3f);
        else if (ps.main.stopAction != ParticleSystemStopAction.Destroy)
            Destroy(vfx, ps.main.duration + ps.main.startLifetime.constantMax);
    }
}
