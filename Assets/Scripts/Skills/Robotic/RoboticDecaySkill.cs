// 스킬 1: 부패 — 장착/강화마다 장판 크기/지속시간 증가 / 최종: 어둠의 눈 (장판 중앙에 추가 큰 피해)
public class RoboticDecaySkill : SkillBase
{
    public RoboticDecaySkill(SkillDataSO data) : base(data) { }

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
            bot.hasDarkEye = true;
    }

    void Apply(Hero hero)
    {
        if (hero is not Robotic bot) return;
        bot.zoneRadius *= 1f + bot.zoneRadiusIncreasePerLevel;
        bot.zoneLifetime *= 1f + bot.zoneLifetimeIncreasePerLevel;
    }
}
