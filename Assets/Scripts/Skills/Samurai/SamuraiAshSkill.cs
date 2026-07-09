// 스킬 3: 잿불 — 장착/강화마다 화상 최대 지속시간 증가 / 최종: 연소폭발 (화상 n틱마다 큰 데미지)
public class SamuraiAshSkill : SkillBase
{
    public SamuraiAshSkill(SkillDataSO data) : base(data) { }

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
            sam.hasCombustionExplosion = true;
    }

    void Apply(Hero hero)
    {
        if (hero is not Samurai sam) return;
        sam.burnMaxTicks += sam.ashMaxTicksIncreasePerLevel;
    }
}
