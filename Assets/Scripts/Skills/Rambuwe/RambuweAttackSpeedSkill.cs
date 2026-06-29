// 스킬 1: 발사 속도 증가 / 최종: 최대탄약 +50
public class RambuweAttackSpeedSkill : SkillBase
{
    public RambuweAttackSpeedSkill()
    {
        skillName = "발사 속도 증가";
        description = "공격 간격 20% 감소";
    }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        hero.stats.attackSpeed *= 0.8f;
    }

    protected override void OnUpgrade()
    {
        Owner.stats.attackSpeed *= 0.85f;
    }

    protected override void OnFinalize()
    {
        Owner.stats.maxAmmo += 50;
        description = "공격 간격 감소 + 최대탄약 +50";
    }
}
