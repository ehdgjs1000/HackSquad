// 스킬 1: 폭발범위 증가 / 최종: 원산폭격 — Data.loopInterval초마다 전체 핵 투하
// Data.equipValue/upgradeValue = explosionRadius 배율
// Data.loopInterval/warningDuration/radiusMultiplier/damageMultiplier = 원산폭격 튜닝
public class GreenHasaExplosionSkill : SkillBase
{
    public GreenHasaExplosionSkill(SkillDataSO data) : base(data) { }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        hero.stats.explosionRadius *= Data.equipValue;
    }

    protected override void OnUpgrade()
    {
        Owner.stats.explosionRadius *= Data.upgradeValue;
    }

    protected override void OnFinalize()
    {
        if (Owner is GreenHasa gh)
            gh.StartOhsanBombing(Data);
    }
}
