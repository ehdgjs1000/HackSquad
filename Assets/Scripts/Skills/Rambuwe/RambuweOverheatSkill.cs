// 스킬 3: 과열 (한발마다 데미지 증가, 장전 시 초기화) / 최종: 관통 증가
public class RambuweOverheatSkill : SkillBase
{
    float _baseDamage;
    int _stack;

    public RambuweOverheatSkill(SkillDataSO data) : base(data) { }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        _baseDamage = hero.stats.attackDamage;
        _stack = 0;
    }

    public override void OnAttack(Hero hero, Monster target)
    {
        if (hero is not Rambuwe ram) return;
        _stack++;
        hero.stats.attackDamage = _baseDamage * (1f + _stack * ram.overheatStackRatio);
    }

    public override void OnReload(Hero hero)
    {
        _stack = 0;
        hero.stats.attackDamage = _baseDamage;
    }

    protected override void OnFinalize()
    {
        if (Owner is Rambuwe ram)
            Owner.stats.pierceCount += ram.overheatPierceBonusFinal;
    }
}
