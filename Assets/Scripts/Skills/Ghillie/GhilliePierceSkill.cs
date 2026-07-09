// 스킬 1: 관통 수 증가 / 최종: 무한관통
public class GhilliePierceSkill : SkillBase
{
    public GhilliePierceSkill(SkillDataSO data) : base(data) { }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        if (hero is Ghillie gh)
            hero.stats.pierceCount += gh.pierceCountEquip;
    }

    protected override void OnUpgrade()
    {
        if (Owner is Ghillie gh)
            Owner.stats.pierceCount += gh.pierceCountUpgrade;
    }

    protected override void OnFinalize()
    {
        Owner.stats.pierceCount = -1; // 무한관통 (Bullet에서 -1을 무한으로 처리)
    }
}
