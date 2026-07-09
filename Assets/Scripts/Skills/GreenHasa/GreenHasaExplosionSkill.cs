// 스킬 1: 폭발범위 증가 / 최종: 원산폭격 — ohsanLoopInterval초마다 전체 핵 투하
public class GreenHasaExplosionSkill : SkillBase
{
    public GreenHasaExplosionSkill(SkillDataSO data) : base(data) { }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        if (hero is GreenHasa gh)
            hero.stats.explosionRadius *= gh.explosionRadiusEquipMultiplier;
    }

    protected override void OnUpgrade()
    {
        if (Owner is GreenHasa gh)
            Owner.stats.explosionRadius *= gh.explosionRadiusUpgradeMultiplier;
    }

    protected override void OnFinalize()
    {
        if (Owner is GreenHasa gh)
            gh.StartOhsanBombing();
    }
}
