using System.Collections.Generic;

public class Rambuwe : Hero
{
    protected override void Init()
    {
        attackBehavior = new AutoAttackBehavior();
    }

    public override List<SkillBase> GetSkillCandidates()
    {
        return new List<SkillBase>
        {
            new RambuweAttackSpeedSkill(),
            new RambuweAccuracySkill(),
            new RambuweOverheatSkill()
        };
    }
}
