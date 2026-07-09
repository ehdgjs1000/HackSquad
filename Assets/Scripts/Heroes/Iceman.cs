using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceman : Hero
{
    [Header("얼음 장판")]
    public GameObject iceVfxPrefab;
    [HideInInspector] public float slowMultiplier = 0.5f; // 장판 위 이동속도 배율 (1=정상, 0=완전정지)
    [HideInInspector] public float iceDuration = 3f;       // 장판 유지시간(초)
    [HideInInspector] public bool hasIcicleBurst;          // 스킬 3 최종: 장판 생성 시 고드름 4방향 발사 여부

    [Header("스킬 1: 냉기 강화")]
    public float slowMultiplierEquip = 0.7f;           // 최초 슬로우 배율(1=느려짐 없음, 0=완전정지)
    public float slowMultiplierUpgradeDecrease = 0.15f; // 강화당 배율 추가 감소량

    [Header("스킬 1 최종: 완전 빙결")]
    public float iceDurationBonusFinal = 2f; // 최종 승급 시 장판 지속시간 증가량(초)

    [Header("스킬 2: 장판 확장 (이전 대비 비율 증가)")]
    public float explosionRadiusEquipIncrease = 0.15f;
    public float explosionRadiusUpgradeIncrease = 0.15f;

    [Header("스킬 2 최종: 연쇄 빙결(토네이도)")]
    public GameObject tornadoVfxPrefab;
    public float tornadoLoopInterval = 6f;
    public float tornadoLifetime = 6f;

    [Header("스킬 3: 냉기 파쇄 (이미 슬로우 상태인 적에게 추가 데미지 배율)")]
    public float shatterMultiplierEquip = 1.3f;
    public float shatterMultiplierUpgrade = 0.2f;

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

    public void StartTornadoLoop() => StartCoroutine(TornadoLoop());

    // 연쇄 빙결: 승급 즉시 토네이도 생성 후, tornadoLoopInterval초 쿨마다 재생성 (공격과 무관한 독립 루프)
    IEnumerator TornadoLoop()
    {
        while (true)
        {
            SpawnTornado();
            yield return new WaitForSeconds(tornadoLoopInterval);
        }
    }

    void SpawnTornado()
    {
        var monsters = FindObjectsByType<Monster>(FindObjectsSortMode.None);
        Vector3 pos = monsters.Length > 0
            ? monsters[Random.Range(0, monsters.Length)].transform.position
            : transform.position;
        pos.y = 0f;

        Tornado.Spawn(tornadoVfxPrefab, pos, stats.attackDamage, tornadoLifetime);
    }
}
