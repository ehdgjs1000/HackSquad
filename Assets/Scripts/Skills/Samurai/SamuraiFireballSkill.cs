// 스킬 2: 화염구체 강화 — 장착/강화마다 총알 크기와 데미지가 증가 / 최종: 슬래시 (n초마다 무작위 다수 적 피해)
// 크기 증가량은 Samurai.fireballSizeIncreasePerLevel, 데미지는 fireballDamageIncreasePerLevel(퍼센트)
public class SamuraiFireballSkill : SkillBase
{
    float _baseDamage;
    float _damagePercent;

    public SamuraiFireballSkill(SkillDataSO data) : base(data) { }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        _baseDamage = hero.stats.attackDamage;
        Apply(hero);
    }

    protected override void OnUpgrade()
    {
        Apply(Owner);
    }

    protected override void OnFinalize()
    {
        if (Owner is Samurai sam)
            sam.StartSlashLoop();
    }

    void Apply(Hero hero)
    {
        if (hero is not Samurai sam) return;
        sam.bulletScaleMultiplier += sam.fireballSizeIncreasePerLevel;
        _damagePercent += sam.fireballDamageIncreasePerLevel;
        hero.stats.attackDamage = _baseDamage * (1f + _damagePercent);
    }
}
