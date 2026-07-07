using UnityEngine;

// Data.equipValue/upgradeValue = spreadAngle 감소량, Data.finalValue = backAttackRatio 설정값
public class RambuweAccuracySkill : SkillBase
{
    public RambuweAccuracySkill(SkillDataSO data) : base(data) { }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        hero.stats.spreadAngle = Mathf.Max(0f, hero.stats.spreadAngle - Data.equipValue);
    }

    protected override void OnUpgrade()
    {
        Owner.stats.spreadAngle = Mathf.Max(0f, Owner.stats.spreadAngle - Data.upgradeValue);
    }

    protected override void OnFinalize()
    {
        Owner.stats.backAttackRatio = Data.finalValue;
        description = finalDescription;
    }
}
