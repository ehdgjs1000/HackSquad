// 스킬 2: 장판 확장 — 얼음 장판 범위 증가 / 최종: 연쇄 빙결 (공격 시 다른 적에게도 장판 추가 생성)
// Data.equipValue/upgradeValue = stats.explosionRadius 증가량, Data.bombCount = 최종 승급 시 추가 생성 장판 수
public class IcemanRadiusSkill : SkillBase
{
    public IcemanRadiusSkill(SkillDataSO data) : base(data) { }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        hero.stats.explosionRadius += Data.equipValue;
    }

    protected override void OnUpgrade()
    {
        Owner.stats.explosionRadius += Data.upgradeValue;
    }

    protected override void OnFinalize()
    {
        if (Owner is Iceman ice)
            ice.chainZoneCount += (int)Data.bombCount;
    }
}
