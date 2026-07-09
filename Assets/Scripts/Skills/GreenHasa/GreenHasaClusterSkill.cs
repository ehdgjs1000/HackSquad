// 스킬 2: 클러스터 생성 — 폭발 후 클러스터탄 추가 폭발 / 최종: 융단폭격
public class GreenHasaClusterSkill : SkillBase
{
    public GreenHasaClusterSkill(SkillDataSO data) : base(data) { }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        if (hero is GreenHasa gh) gh.clusterCount += gh.clusterCountEquip;
    }

    protected override void OnUpgrade()
    {
        if (Owner is GreenHasa gh) gh.clusterCount += gh.clusterCountUpgrade;
    }

    protected override void OnFinalize()
    {
        if (Owner is GreenHasa gh)
            gh.StartCarpetBombing();
    }
}
