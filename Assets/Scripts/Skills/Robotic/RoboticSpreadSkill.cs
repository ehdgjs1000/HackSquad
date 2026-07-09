// 스킬 2: 암흑확산 — 장착/강화마다 장판 개수 추가 / 최종: 독무 (장판 생성 시 범위 내 전체에게 독 부여)
public class RoboticSpreadSkill : SkillBase
{
    public RoboticSpreadSkill(SkillDataSO data) : base(data) { }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        Apply(hero);
    }

    protected override void OnUpgrade()
    {
        Apply(Owner);
    }

    protected override void OnFinalize()
    {
        if (Owner is Robotic bot)
            bot.hasPoisonMist = true;
    }

    void Apply(Hero hero)
    {
        if (hero is not Robotic bot) return;
        bot.extraZoneCount += bot.zoneCountPerLevel;
    }
}
