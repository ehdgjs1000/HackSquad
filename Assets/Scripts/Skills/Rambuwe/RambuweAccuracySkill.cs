using UnityEngine;
public class RambuweAccuracySkill : SkillBase
{
    public RambuweAccuracySkill()
    {
        skillName = "정확도 증가";
        description = "발사각 감소";
        finalDescription = "발사각 감소 + 후방 지원 (50% 데미지)";
    }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        hero.stats.spreadAngle = Mathf.Max(0f, hero.stats.spreadAngle - 10f);
    }

    protected override void OnUpgrade()
    {
        Owner.stats.spreadAngle = Mathf.Max(0f, Owner.stats.spreadAngle - 10f);
    }

    protected override void OnFinalize()
    {
        Owner.stats.backAttackRatio = 0.5f;
        description = "발사각 감소 + 후방 지원 (50% 데미지)";
    }
}
