using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceman : Hero
{
    [Header("얼음 장판")]
    public GameObject iceVfxPrefab;
    [HideInInspector] public float slowMultiplier = 0.5f; // 장판 위 이동속도 배율 (1=정상, 0=완전정지)
    [HideInInspector] public float iceDuration = 3f;       // 장판 유지시간(초)
    [HideInInspector] public int chainZoneCount;           // 스킬 2 최종: 공격 시 동시 생성되는 추가 장판 수

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

    public void StartFrostStormLoop(SkillDataSO data) => StartCoroutine(FrostStormLoop(data));

    // 결빙 폭풍: data.loopInterval초마다 무작위 적 위치에 광역 장판 생성 (데미지 1회 + 슬로우 지속)
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
            float damage = stats.attackDamage * data.damageMultiplier;
            IceZone.Spawn(iceVfxPrefab, pos, damage, radius, slowMultiplier, data.lifetime);
        }
    }
}
