// 스킬 3: 검은가시 — 장착 시 활성화, 장판 생성 즉시 범위 피해 부여(강화마다 피해 증가) / 최종: 공허붕괴 (장판 종료 시 거대한 폭발)
public class RoboticThornSkill : SkillBase
{
    public RoboticThornSkill(SkillDataSO data) : base(data) { }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        if (hero is Robotic bot)
            bot.hasInitialBurst = true;
    }

    protected override void OnUpgrade()
    {
        if (Owner is Robotic bot)
            bot.initialBurstDamageRatio += bot.initialBurstDamageIncreasePerLevel;
    }

    protected override void OnFinalize()
    {
        if (Owner is Robotic bot)
            bot.hasVoidCollapse = true;
    }
}
