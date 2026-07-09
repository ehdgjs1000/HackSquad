using System.Collections.Generic;
using UnityEngine;

// 로보틱 — 가까운 적 위치에 지속딜 장판을 생성하는 히어로 (Hero.md 7번 항목)
public class Robotic : Hero
{
    [Header("기본 공격: 지속딜 장판")]
    public GameObject zoneVfxPrefab;
    public float zoneRadius = 2.5f;
    public float zoneLifetime = 4f;
    public float zoneTickInterval = 1f;
    public float zoneDamagePerTickRatio = 0.3f; // 틱당 피해 = attackDamage * 비율

    [Header("스킬 1: 부패 (장판 크기/지속시간 증가, 비율)")]
    public float zoneRadiusIncreasePerLevel = 0.15f;
    public float zoneLifetimeIncreasePerLevel = 0.15f;

    [Header("스킬 1 최종: 어둠의 눈 (장판 중앙 반경에 추가 큰 피해)")]
    public float eyeRadius = 1f;
    public float eyeDamagePerTickRatio = 1f; // 중앙 추가 피해 = attackDamage * 비율
    [HideInInspector] public bool hasDarkEye;

    [Header("스킬 2: 암흑확산 (장판 개수 추가)")]
    public int zoneCountPerLevel = 1;
    [HideInInspector] public int extraZoneCount;

    [Header("스킬 2 최종: 독무 (장판 생성 시 범위 내 전체에게 독 부여)")]
    public GameObject poisonUiPrefab;
    public GameObject poisonExplosionVfxPrefab;
    public float poisonDamagePerStackRatio = 0.3f; // 스택 1개당 피해 비율
    public int poisonMaxStacks = 10;
    public float poisonExplodeDelay = 5f;
    [HideInInspector] public bool hasPoisonMist;

    [Header("스킬 3: 검은가시 (장판 생성 즉시 1회 범위 피해, 강화마다 피해 증가)")]
    public float initialBurstDamageRatio = 1f; // attackDamage 대비 비율
    public float initialBurstDamageIncreasePerLevel = 0.2f;
    [HideInInspector] public bool hasInitialBurst;

    [Header("스킬 3 최종: 공허붕괴 (장판 종료 시 거대한 폭발)")]
    public GameObject voidCollapseVfxPrefab;
    public float voidCollapseDamageRatio = 3f;
    public float voidCollapseRadius = 4f;
    [HideInInspector] public bool hasVoidCollapse;

    [Header("Skill Data")]
    public SkillDataSO decaySkillData;
    public SkillDataSO spreadSkillData;
    public SkillDataSO thornSkillData;

    protected override void Init()
    {
        attackBehavior = new DarkZoneAttackBehavior();
    }

    public override List<SkillBase> GetSkillCandidates() => new()
    {
        new RoboticDecaySkill(decaySkillData),
        new RoboticSpreadSkill(spreadSkillData),
        new RoboticThornSkill(thornSkillData)
    };
}
