// 스킬 1: 발사 속도 증가 / 최종: 최대탄약 증가
public class RambuweAttackSpeedSkill : SkillBase
{
    public RambuweAttackSpeedSkill(SkillDataSO data) : base(data) { }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        if (hero is Rambuwe ram)
            hero.stats.attackSpeed *= ram.attackSpeedEquipMultiplier;
    }

    protected override void OnUpgrade()
    {
        if (Owner is Rambuwe ram)
            Owner.stats.attackSpeed *= ram.attackSpeedUpgradeMultiplier;
    }

    protected override void OnFinalize()
    {
        if (Owner is Rambuwe ram)
            Owner.stats.maxAmmo += ram.maxAmmoBonusFinal;
        description = finalDescription;
    }
}
