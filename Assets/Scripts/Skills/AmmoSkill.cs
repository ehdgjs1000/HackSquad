// 최대탄약 증가 스킬 (람붜 스킬1 최종형)
public class AmmoSkill : SkillBase
{
    public AmmoSkill()
    {
        skillName = "최대탄약 +50";
        description = "최대 탄약 50 증가";
    }

    protected override void OnUpgrade()
    {
        // Hero에게 적용은 SkillSelectionUI의 SelectSkill에서 별도 처리
    }
}