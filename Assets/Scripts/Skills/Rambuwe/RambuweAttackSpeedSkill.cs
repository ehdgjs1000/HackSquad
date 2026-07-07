// 스킬 1: 발사 속도 증가 / 최종: 최대탄약 증가
// Data.equipValue/upgradeValue = attackSpeed 배율, Data.finalValue = maxAmmo 증가량
public class RambuweAttackSpeedSkill : SkillBase
{
    public RambuweAttackSpeedSkill(SkillDataSO data) : base(data) { }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        hero.stats.attackSpeed *= Data.equipValue;
    }

    protected override void OnUpgrade()
    {
        Owner.stats.attackSpeed *= Data.upgradeValue;
    }

    protected override void OnFinalize()
    {
        Owner.stats.maxAmmo += (int)Data.finalValue;
        description = finalDescription;
    }
}
