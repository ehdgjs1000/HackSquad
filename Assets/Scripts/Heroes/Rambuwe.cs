using System.Collections.Generic;
using UnityEngine;

public class Rambuwe : Hero
{
    [Header("스킬 1: 발사 속도 증가")]
    public float attackSpeedEquipMultiplier = 0.8f;    // 장착 시 attackSpeed 배율
    public float attackSpeedUpgradeMultiplier = 0.85f; // 강화당 attackSpeed 배율

    [Header("스킬 1 최종: 최대탄약 증가")]
    public int maxAmmoBonusFinal = 50;

    [Header("스킬 2: 정확도 증가")]
    public float spreadAngleEquipDecrease = 10f;
    public float spreadAngleUpgradeDecrease = 10f;

    [Header("스킬 2 최종: 후방 지원")]
    public float backAttackRatioFinal = 0.5f;

    [Header("스킬 3: 과열 (한 발마다 공격력 증가, 장전 시 초기화)")]
    public float overheatStackRatio = 0.01f; // 한 발당 공격력 증가율(1%)

    [Header("스킬 3 최종: 관통 증가")]
    public int overheatPierceBonusFinal = 1;

    [Header("Skill Data")]
    public SkillDataSO attackSpeedSkillData;
    public SkillDataSO accuracySkillData;
    public SkillDataSO overheatSkillData;

    protected override void Init()
    {
        attackBehavior = new AutoAttackBehavior();
    }

    public override List<SkillBase> GetSkillCandidates()
    {
        return new List<SkillBase>
        {
            new RambuweAttackSpeedSkill(attackSpeedSkillData),
            new RambuweAccuracySkill(accuracySkillData),
            new RambuweOverheatSkill(overheatSkillData)
        };
    }
}
