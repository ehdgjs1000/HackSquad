// 스킬 2: 헤드샷 — 치명타 확률/치명타 피해 증가 / 최종: 더블 공격
// Data.equipValue = 치명타 확률 증가량, Data.upgradeValue = 치명타 피해 증가량 (equip/upgrade 시 매번 동일하게 적용)
public class GhillieHeadshotSkill : SkillBase
{
    public GhillieHeadshotSkill(SkillDataSO data) : base(data) { }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        hero.stats.critChance += Data.equipValue;
        hero.stats.critDamage += Data.upgradeValue;
    }

    protected override void OnUpgrade()
    {
        Owner.stats.critChance += Data.equipValue;
        Owner.stats.critDamage += Data.upgradeValue;
    }

    protected override void OnFinalize()
    {
        if (Owner is Ghillie gh)
            gh.extraShotCount += 1; // 더블 공격: 같은 방향으로 총알 1발 추가 발사
    }
}
