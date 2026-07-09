using UnityEngine;

// 스킬 2: 정확도 증가 / 최종: 후방 지원
public class RambuweAccuracySkill : SkillBase
{
    public RambuweAccuracySkill(SkillDataSO data) : base(data) { }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        if (hero is Rambuwe ram)
            hero.stats.spreadAngle = Mathf.Max(0f, hero.stats.spreadAngle - ram.spreadAngleEquipDecrease);
    }

    protected override void OnUpgrade()
    {
        if (Owner is Rambuwe ram)
            Owner.stats.spreadAngle = Mathf.Max(0f, Owner.stats.spreadAngle - ram.spreadAngleUpgradeDecrease);
    }

    protected override void OnFinalize()
    {
        if (Owner is Rambuwe ram)
            Owner.stats.backAttackRatio = ram.backAttackRatioFinal;
        description = finalDescription;
    }
}
