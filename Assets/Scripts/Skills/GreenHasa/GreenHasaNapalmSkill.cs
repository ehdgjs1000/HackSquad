using UnityEngine;

// 스킬 3: 네이팜탄 — 폭발 지역에 화염 지대 생성 / 최종: 플레임타워
public class GreenHasaNapalmSkill : SkillBase
{
    public GreenHasaNapalmSkill(SkillDataSO data) : base(data) { }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        if (hero is GreenHasa gh)
        {
            gh.hasNapalm = true;
            gh.napalmDamageRatio = gh.napalmDamageRatioEquip;
            gh.napalmDuration = gh.napalmDurationEquip;
        }
    }

    protected override void OnUpgrade()
    {
        if (Owner is GreenHasa gh)
            gh.napalmTickInterval = Mathf.Max(0.1f, gh.napalmTickInterval - gh.napalmTickDecreasePerUpgrade);
    }

    protected override void OnFinalize()
    {
        if (Owner is GreenHasa gh)
            gh.StartFlameTowerLoop();
    }
}
