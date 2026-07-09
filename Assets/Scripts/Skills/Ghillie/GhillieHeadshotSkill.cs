// 스킬 2: 헤드샷 — 치명타 확률/치명타 피해 증가 / 최종: 더블 공격
public class GhillieHeadshotSkill : SkillBase
{
    public GhillieHeadshotSkill(SkillDataSO data) : base(data) { }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        Apply(hero);
    }

    protected override void OnUpgrade()
    {
        Apply(Owner);
    }

    void Apply(Hero hero)
    {
        if (hero is not Ghillie gh) return;
        hero.stats.critChance += gh.critChanceIncreasePerLevel;
        hero.stats.critDamage += gh.critDamageIncreasePerLevel;
    }

    protected override void OnFinalize()
    {
        if (Owner is Ghillie gh)
            gh.extraShotCount += 1; // 더블 공격: 같은 방향으로 총알 1발 추가 발사
    }
}
