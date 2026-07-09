using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskHero : Hero
{
    [Header("스킬 1: 강화탄")]
    public float bulletSizeIncreasePerLevel = 0.15f; // 레벨당 총알 크기 증가율(0.15=15%)
    public float bulletDamageIncreasePerLevel = 0.1f; // 레벨당 데미지 증가율(0.1=10%)
    [HideInInspector] public float bulletScaleMultiplier = 1f;

    [Header("스킬 1 최종: 더블샷 (앞뒤 텀 없이 좌우로 살짝 간격을 둔 2발을 동시 발사)")]
    public float doubleShotOffset = 0.6f; // 좌우 발사 간격(각 발 기준 firePos에서 좌우로 떨어지는 거리)
    [HideInInspector] public bool hasDoubleShot;

    [Header("스킬 2: 연사 (한발 발사 후 매우 짧은 텀으로 한발 더 발사할 확률)")]
    public float extraShotChancePerLevel = 0.2f; // 레벨당 추가 발사 확률 증가량(0.2=20%, 3레벨 누적 시 60%)
    public float extraShotDelay = 0.08f; // 추가 발사까지의 지연 시간(초)
    [HideInInspector] public float extraShotChance;

    [Header("스킬 2 최종: 분열탄 (명중 시 확률로 진행방향 기준 좌우 수직 방향 추가탄 생성, 기존 총알 프리팹 그대로 사용)")]
    public float splitChance = 0.3f;
    public float splitAngle = 90f; // 분열탄 좌우 각도(도, 90=진행방향에 수직)
    [HideInInspector] public bool hasSplitShot;

    [Header("스킬 3: 관통 강화 (관통마다 데미지 누적 증가)")]
    public float damageIncreasePerPierce = 0.1f;
    [HideInInspector] public bool hasPierceDamageBonus;

    [Header("스킬 3 최종: 이온폭풍 (승급 즉시 생성, 이후 주기적으로 재생성)")]
    public GameObject ionStormVfxPrefab;
    public float ionStormLoopInterval = 8f;
    public float ionStormLifetime = 4f;

    [Header("Skill Data")]
    public SkillDataSO bulletSizeSkillData;
    public SkillDataSO rapidFireSkillData;
    public SkillDataSO pierceDamageSkillData;

    protected override void Init()
    {
        attackBehavior = new AutoAttackBehavior();
    }

    public override List<SkillBase> GetSkillCandidates() => new()
    {
        new MaskBulletSizeSkill(bulletSizeSkillData),
        new MaskRapidFireSkill(rapidFireSkillData),
        new MaskPierceDamageSkill(pierceDamageSkillData)
    };

    public void StartIonStormLoop() => StartCoroutine(IonStormLoop());

    // 이온폭풍: 최종 승급 즉시 생성 후, ionStormLoopInterval초 쿨마다 재생성
    IEnumerator IonStormLoop()
    {
        while (true)
        {
            SpawnIonStorm();
            yield return new WaitForSeconds(ionStormLoopInterval);
        }
    }

    void SpawnIonStorm()
    {
        var monsters = FindObjectsByType<Monster>(FindObjectsSortMode.None);
        Vector3 pos = monsters.Length > 0
            ? monsters[Random.Range(0, monsters.Length)].transform.position
            : transform.position;
        pos.y = 0f;

        IonStorm.Spawn(ionStormVfxPrefab, pos, stats.attackDamage, ionStormLifetime);
    }
}
