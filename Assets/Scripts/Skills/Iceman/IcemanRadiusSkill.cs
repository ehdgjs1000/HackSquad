// 스킬 2: 장판 확장 — 얼음 장판 범위 증가(누적 배율) / 최종: 연쇄 빙결 (공격과 무관하게 필드를 떠도는 토네이도를 주기적으로 생성)
// Data.equipValue/upgradeValue = stats.explosionRadius 증가 비율(0.3 = 이전 대비 30% 증가)
// Data.loopInterval = 최종 승급 시 토네이도 생성 주기(초), Data.lifetime = 토네이도 지속시간(초)
public class IcemanRadiusSkill : SkillBase
{
    public IcemanRadiusSkill(SkillDataSO data) : base(data) { }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        hero.stats.explosionRadius *= 1f + Data.equipValue;
    }

    protected override void OnUpgrade()
    {
        Owner.stats.explosionRadius *= 1f + Data.upgradeValue;
    }

    protected override void OnFinalize()
    {
        if (Owner is Iceman ice)
            ice.StartTornadoLoop(Data);
    }
}
