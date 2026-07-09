using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenHasa : Hero
{
    [Header("스킬 1: 폭발범위 증가")]
    public float explosionRadiusEquipMultiplier = 1.3f;
    public float explosionRadiusUpgradeMultiplier = 1.2f;

    [Header("스킬 1 최종: 원산폭격")]
    public GameObject nuclearWarningVFX;    // 경고 원 VFX (2초 표시)
    public GameObject nuclearExplosionVFX;  // 폭발 VFX
    public float ohsanLoopInterval = 10f;
    public float ohsanWarningDuration = 2f;
    public float ohsanRadiusMultiplier = 2f;
    public float ohsanDamageMultiplier = 5f;

    [Header("스킬 2: 클러스터 생성")]
    public int clusterCountEquip = 3;
    public int clusterCountUpgrade = 2;

    [Header("스킬 2 최종: 융단폭격")]
    public GameObject carpetExplosionVFX;   // 개별 폭발 VFX
    public float carpetLoopInterval = 10f;
    public float carpetWarningDuration = 1.5f;
    public float carpetRadiusMultiplier = 0.5f;
    public float carpetDamageMultiplier = 0.6f;
    public int carpetBombCount = 8;
    public float carpetBombSpacing = 2.5f;
    public float carpetBombDelay = 0.15f;

    [Header("스킬 3: 네이팜탄 (장착 시 napalmDamageRatio/napalmDuration 초기화)")]
    public float napalmDamageRatioEquip = 0.3f;
    public float napalmDurationEquip = 5f;
    public float napalmTickDecreasePerUpgrade = 0.1f;

    [Header("스킬 3 최종: 플레임타워")]
    public GameObject flameTowerPrefab;
    public GameObject napalmVFX;
    public float flameTowerLoopInterval = 20f;
    public float flameTowerDamageMultiplier = 0.4f;
    public float flameTowerLifetime = 10f;
    public float flameTowerSpawnRange = 8f;

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

    public void StartOhsanBombing()   => StartCoroutine(OhsanBombingLoop());
    public void StartCarpetBombing()  => StartCoroutine(CarpetBombingLoop());
    public void StartFlameTowerLoop() => StartCoroutine(FlameTowerLoop());

    // 원산폭격: ohsanLoopInterval초마다 랜덤 몬스터 위치에 경고 원 → 폭발
    IEnumerator OhsanBombingLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(ohsanLoopInterval);

            var monsters = FindObjectsByType<Monster>(FindObjectsSortMode.None);
            Vector3 targetPos = monsters.Length > 0
                ? monsters[Random.Range(0, monsters.Length)].transform.position
                : transform.position;
            targetPos.y = 0f;

            // 경고 원 표시
            GameObject warning = null;
            if (nuclearWarningVFX != null)
                warning = Instantiate(nuclearWarningVFX, targetPos, Quaternion.identity);

            yield return new WaitForSeconds(ohsanWarningDuration);

            if (warning != null) Destroy(warning);

            // 폭발 VFX + 데미지
            SpawnExplosionVFX(nuclearExplosionVFX, targetPos);
            float radius = stats.explosionRadius * ohsanRadiusMultiplier;
            int monsterLayer = LayerMask.GetMask("Monster");
            var cols = Physics.OverlapSphere(targetPos, radius, monsterLayer);
            foreach (var col in cols)
                if (col.TryGetComponent(out Monster m))
                    m.TakeDamage(stats.attackDamage * ohsanDamageMultiplier);
        }
    }

    // 융단폭격: carpetLoopInterval초마다 무작위 방향 1자 라인 경고 → 순차 폭발
    IEnumerator CarpetBombingLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(carpetLoopInterval);

            Vector3 dir = Random.insideUnitSphere;
            dir.y = 0f;
            dir.Normalize();

            Vector3[] positions = new Vector3[carpetBombCount];
            for (int i = 0; i < carpetBombCount; i++)
                positions[i] = transform.position + dir * (i * carpetBombSpacing);

            // LineRenderer로 1자 라인 표시
            GameObject lineGO = CreateLineIndicator(positions[0], positions[carpetBombCount - 1]);

            yield return new WaitForSeconds(carpetWarningDuration);

            if (lineGO != null) Destroy(lineGO);

            // 순차 폭발
            int monsterLayer = LayerMask.GetMask("Monster");
            float bombRadius = stats.explosionRadius * carpetRadiusMultiplier;
            for (int i = 0; i < carpetBombCount; i++)
            {
                SpawnExplosionVFX(carpetExplosionVFX, positions[i]);
                var cols = Physics.OverlapSphere(positions[i], bombRadius, monsterLayer);
                foreach (var col in cols)
                    if (col.TryGetComponent(out Monster m))
                        m.TakeDamage(stats.attackDamage * carpetDamageMultiplier);

                yield return new WaitForSeconds(carpetBombDelay);
            }
        }
    }

    // 플레임타워: 최종 승급 즉시 1개 설치 후, flameTowerLoopInterval초마다 추가 설치 (flameTowerLifetime초 지속)
    IEnumerator FlameTowerLoop()
    {
        while (true)
        {
            Vector3 rand = Random.insideUnitSphere * flameTowerSpawnRange;
            rand.y = 0f;
            FlameTower.Spawn(flameTowerPrefab, rand, stats.attackDamage * flameTowerDamageMultiplier, flameTowerLifetime);

            yield return new WaitForSeconds(flameTowerLoopInterval);
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
