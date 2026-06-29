// 스킬 1: 폭발범위 증가 / 최종: 원산폭격 — 15초마다 전체 핵 투하
public class GreenHasaExplosionSkill : SkillBase
{
    public GreenHasaExplosionSkill()
    {
        skillName = "폭발범위 증가";
        description = "폭발 반경 30% 증가";
        finalDescription = "폭발 반경 증가 + 원산폭격 (15초마다 핵 투하)";
    }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        hero.stats.explosionRadius *= 1.3f;
    }

    protected override void OnUpgrade()
    {
        Owner.stats.explosionRadius *= 1.2f;
    }

    protected override void OnFinalize()
    {
        if (Owner is GreenHasa gh)
            gh.StartOhsanBombing();
    }
}
