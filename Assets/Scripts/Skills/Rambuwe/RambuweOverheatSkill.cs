// 스킬 3: 과열 (한발마다 데미지 1% 증가, 장전 시 초기화) / 최종: 관통 +1
public class RambuweOverheatSkill : SkillBase
{
    float _baseDamage;
    int _stack;

    public RambuweOverheatSkill()
    {
        skillName = "과열";
        description = "한 발마다 공격력 1% 증가 (장전 시 초기화)";
        finalDescription = "한 발마다 공격력 1% 증가 + 관통 1회";
    }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        _baseDamage = hero.stats.attackDamage;
        _stack = 0;
    }

    public override void OnAttack(Hero hero, Monster target)
    {
        _stack++;
        hero.stats.attackDamage = _baseDamage * (1f + _stack * 0.01f);
    }

    public override void OnReload(Hero hero)
    {
        _stack = 0;
        hero.stats.attackDamage = _baseDamage;
    }

    protected override void OnFinalize()
    {
        Owner.stats.pierceCount += 1;
        description = "한 발마다 공격력 1% 증가 + 관통 1회";
    }
}
