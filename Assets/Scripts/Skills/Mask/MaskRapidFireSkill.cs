// 스킬 2: 연사 — 장착/강화마다 추가 발사 확률 증가 / 최종: 분열탄 (명중 시 확률로 좌우 대칭 추가탄 생성)
// 확률 증가량은 MaskHero.extraShotChancePerLevel, 최종 분열 확률/각도는 MaskHero.splitChance/splitAngle (인스펙터에서 조절)
public class MaskRapidFireSkill : SkillBase
{
    public MaskRapidFireSkill(SkillDataSO data) : base(data) { }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        if (hero is MaskHero mask)
            mask.extraShotChance += mask.extraShotChancePerLevel;
    }

    protected override void OnUpgrade()
    {
        if (Owner is MaskHero mask)
            mask.extraShotChance += mask.extraShotChancePerLevel;
    }

    protected override void OnFinalize()
    {
        if (Owner is MaskHero mask)
            mask.hasSplitShot = true;
    }
}
