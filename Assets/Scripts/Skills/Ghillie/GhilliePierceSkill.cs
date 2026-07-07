// 스킬 1: 관통 수 증가 / 최종: 무한관통
// Data.equipValue/upgradeValue = pierceCount 증가량
public class GhilliePierceSkill : SkillBase
{
    public GhilliePierceSkill(SkillDataSO data) : base(data) { }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        hero.stats.pierceCount += (int)Data.equipValue;
    }

    protected override void OnUpgrade()
    {
        Owner.stats.pierceCount += (int)Data.upgradeValue;
    }

    protected override void OnFinalize()
    {
        Owner.stats.pierceCount = -1; // 무한관통 (Bullet에서 -1을 무한으로 처리)
    }
}
