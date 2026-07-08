// 스킬 3: 냉기 파쇄 — 이미 슬로우 상태인 적에게 추가 데미지 배율 / 최종: 결빙 폭풍 (기본 공격의 장판 생성 시 그 위치에서 고드름 4방향 발사)
// Data.equipValue = 최초 배율, Data.upgradeValue = 강화당 추가 배율
public class IcemanShatterSkill : SkillBase
{
    float _multiplier;

    public IcemanShatterSkill(SkillDataSO data) : base(data) { }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        _multiplier = Data.equipValue;
    }

    protected override void OnUpgrade()
    {
        _multiplier += Data.upgradeValue;
    }

    public override float GetDamageMultiplier(Monster target)
        => target != null && target.IsSlowed ? _multiplier : 1f;

    protected override void OnFinalize()
    {
        if (Owner is Iceman ice)
            ice.hasIcicleBurst = true;
    }
}
