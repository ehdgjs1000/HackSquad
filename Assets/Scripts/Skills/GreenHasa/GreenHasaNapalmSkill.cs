// 스킬 3: 네이팜탄 — 폭발 지역에 화염 지대 생성 / 최종: 플레임타워 (20초마다 화염 지대 설치)
public class GreenHasaNapalmSkill : SkillBase
{
    public GreenHasaNapalmSkill()
    {
        skillName = "네이팜탄";
        description = "폭발 지역에 화염 지대 생성 (0.5초마다 데미지)";
        finalDescription = "화염 지대 생성 + 플레임타워 (20초마다 화염 지대 설치)";
    }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        if (hero is GreenHasa gh) gh.hasNapalm = true;
    }

    protected override void OnFinalize()
    {
        if (Owner is GreenHasa gh)
            gh.StartFlameTowerLoop();
    }
}
