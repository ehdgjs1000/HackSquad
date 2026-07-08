using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceman : Hero
{
    [Header("얼음 장판")]
    public GameObject iceVfxPrefab;
    [HideInInspector] public float slowMultiplier = 0.5f; // 장판 위 이동속도 배율 (1=정상, 0=완전정지)
    [HideInInspector] public float iceDuration = 3f;       // 장판 유지시간(초)

    [Header("스킬 2 최종: 연쇄 빙결(토네이도)")]
    public GameObject tornadoVfxPrefab;

    [Header("스킬 3 최종: 결빙 폭풍(고드름)")]
    public GameObject icicleVfxPrefab;

    [Header("Skill Data")]
    public SkillDataSO chillSkillData;
    public SkillDataSO radiusSkillData;
    public SkillDataSO shatterSkillData;

    protected override void Init()
    {
        attackBehavior = new IceAttackBehavior();
    }

    public override List<SkillBase> GetSkillCandidates() => new()
    {
        new IcemanChillSkill(chillSkillData),
        new IcemanRadiusSkill(radiusSkillData),
        new IcemanShatterSkill(shatterSkillData)
    };

    public void StartTornadoLoop(SkillDataSO data) => StartCoroutine(TornadoLoop(data));
    public void StartFrostStormLoop(SkillDataSO data) => StartCoroutine(FrostStormLoop(data));

    // 연쇄 빙결: data.loopInterval초마다 무작위 적 위치에 토네이도 생성 (공격과 무관한 독립 루프)
    IEnumerator TornadoLoop(SkillDataSO data)
    {
        while (true)
        {
            yield return new WaitForSeconds(data.loopInterval);

            var monsters = FindObjectsByType<Monster>(FindObjectsSortMode.None);
            if (monsters.Length == 0) continue;

            Vector3 pos = monsters[Random.Range(0, monsters.Length)].transform.position;
            pos.y = 0f;

            Tornado.Spawn(tornadoVfxPrefab, pos, stats.attackDamage, data.lifetime);
        }
    }

    // 결빙 폭풍: data.loopInterval초마다 무작위 적 위치에 광역 장판 생성(데미지 1회 + 슬로우 지속) + 그 위치에서 90도 간격으로 고드름 4개 발사
    IEnumerator FrostStormLoop(SkillDataSO data)
    {
        while (true)
        {
            yield return new WaitForSeconds(data.loopInterval);

            var monsters = FindObjectsByType<Monster>(FindObjectsSortMode.None);
            if (monsters.Length == 0) continue;

            Vector3 pos = monsters[Random.Range(0, monsters.Length)].transform.position;
            pos.y = 0f;

            float radius = stats.explosionRadius * data.radiusMultiplier;
            float zoneDamage = stats.attackDamage * data.damageMultiplier;
            float vfxScale = baseExplosionRadius > 0f ? radius / baseExplosionRadius : 1f;
            IceZone.Spawn(iceVfxPrefab, pos, zoneDamage, radius, slowMultiplier, data.lifetime, vfxScale: vfxScale);

            // 장판 위치에서 90도 간격으로 고드름 4방향 발사
            float baseAngle = Random.Range(0f, 360f);
            for (int i = 0; i < 4; i++)
            {
                Vector3 dir = Quaternion.Euler(0f, baseAngle + i * 90f, 0f) * Vector3.forward;
                Icicle.Spawn(icicleVfxPrefab, pos, stats.attackDamage, dir);
            }
        }
    }
}
