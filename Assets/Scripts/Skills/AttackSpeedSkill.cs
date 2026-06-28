// 발사 속도 증가 스킬 (람붜 스킬1)
public class AttackSpeedSkill : SkillBase
{
    public AttackSpeedSkill()
    {
        skillName = "발사 속도 증가";
        description = "공격 간격 20% 감소";
    }

    protected override void OnUpgrade()
    {
        // 이미 부착된 Hero에게 적용은 OnAttach나 SquadManager에서 처리
    }

    public override void OnAttack(Hero hero, Monster target)
    {
        // 패시브: 스탯 자체를 줄여주는 방식으로 Hero.Start에서 적용
    }
}