// 스킬 2: 장판 확장 — 얼음 장판 범위 증가(누적 배율) / 최종: 연쇄 빙결 (공격과 무관하게 필드를 떠도는 토네이도를 주기적으로 생성)
public class IcemanRadiusSkill : SkillBase
{
    public IcemanRadiusSkill(SkillDataSO data) : base(data) { }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        if (hero is Iceman ice)
            hero.stats.explosionRadius *= 1f + ice.explosionRadiusEquipIncrease;
    }

    protected override void OnUpgrade()
    {
        if (Owner is Iceman ice)
            Owner.stats.explosionRadius *= 1f + ice.explosionRadiusUpgradeIncrease;
    }

    protected override void OnFinalize()
    {
        if (Owner is Iceman ice)
            ice.StartTornadoLoop();
    }
}
