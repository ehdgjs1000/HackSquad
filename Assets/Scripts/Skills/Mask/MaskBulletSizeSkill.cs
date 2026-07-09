// 스킬 1: 강화탄 — 장착/강화마다 총알 크기와 데미지가 증가
// 크기 증가량은 MaskHero.bulletSizeIncreasePerLevel, 데미지는 bulletDamageIncreasePerLevel(퍼센트, 0.1=10%)
// 최종: 더블샷 — 앞뒤 텀 없이 좌우로 살짝 간격을 둔 2발을 동시 발사 (MaskHero.doubleShotOffset)
public class MaskBulletSizeSkill : SkillBase
{
    float _baseDamage;
    float _damagePercent;

    public MaskBulletSizeSkill(SkillDataSO data) : base(data) { }

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
        if (Owner is MaskHero mask)
            mask.hasDoubleShot = true;
    }

    void Apply(Hero hero)
    {
        if (hero is not MaskHero mask) return;
        mask.bulletScaleMultiplier += mask.bulletSizeIncreasePerLevel;
        _damagePercent += mask.bulletDamageIncreasePerLevel;
        hero.stats.attackDamage = _baseDamage * (1f + _damagePercent);
    }
}
