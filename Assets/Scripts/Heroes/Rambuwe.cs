using System.Collections.Generic;
using UnityEngine;

public class Rambuwe : Hero
{
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
