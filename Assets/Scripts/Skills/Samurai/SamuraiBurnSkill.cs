// 스킬 1: 고온연소 — 장착/강화마다 화상 지속시간과 피해가 증가 / 최종: 메테오 (n초마다 낙하 공격)
public class SamuraiBurnSkill : SkillBase
{
    public SamuraiBurnSkill(SkillDataSO data) : base(data) { }

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
        if (Owner is Samurai sam)
            sam.StartMeteorLoop();
    }

    void Apply(Hero hero)
    {
        if (hero is not Samurai sam) return;
        sam.burnMaxTicks += sam.burnMaxTicksIncreasePerLevel;
        sam.burnDamagePerTickRatio += sam.burnDamageIncreasePerLevel;
    }
}
