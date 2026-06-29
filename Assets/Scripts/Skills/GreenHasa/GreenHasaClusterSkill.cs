// 스킬 2: 클러스터 생성 — 폭발 후 클러스터탄 추가 폭발 / 최종: 융단폭격 (10초마다 1자 폭격)
public class GreenHasaClusterSkill : SkillBase
{
    public GreenHasaClusterSkill()
    {
        skillName = "클러스터 생성";
        description = "폭발 시 클러스터탄 3개 추가 폭발";
        finalDescription = "클러스터탄 증가 + 융단폭격 (10초마다 1자 폭격)";
    }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        if (hero is GreenHasa gh) gh.clusterCount += 3;
    }

    protected override void OnUpgrade()
    {
        if (Owner is GreenHasa gh) gh.clusterCount += 2;
    }

    protected override void OnFinalize()
    {
        if (Owner is GreenHasa gh)
            gh.StartCarpetBombing();
    }
}
