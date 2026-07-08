using UnityEngine;

// 스킬 1: 냉기 강화 — 슬로우 세기 강화(배율 감소) / 최종: 완전 빙결 + 지속시간 증가
// Data.equipValue = 최초 슬로우 배율(1=느려짐 없음, 0=완전정지), Data.upgradeValue = 강화당 배율 추가 감소량
// Data.finalValue = 최종 승급 시 장판 지속시간 증가량(초)
public class IcemanChillSkill : SkillBase
{
    public IcemanChillSkill(SkillDataSO data) : base(data) { }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        if (hero is Iceman ice)
            ice.slowMultiplier = Data.equipValue;
    }

    protected override void OnUpgrade()
    {
        if (Owner is Iceman ice)
            ice.slowMultiplier = Mathf.Max(0f, ice.slowMultiplier - Data.upgradeValue);
    }

    protected override void OnFinalize()
    {
        if (Owner is Iceman ice)
        {
            ice.slowMultiplier = 0f; // 완전 빙결: 장판 위 몬스터 완전 정지
            ice.iceDuration += Data.finalValue;
        }
    }
}
