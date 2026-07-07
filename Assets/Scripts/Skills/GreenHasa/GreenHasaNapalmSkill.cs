using UnityEngine;

// 스킬 3: 네이팜탄 — 폭발 지역에 화염 지대 생성 / 최종: 플레임타워
// Data.equipValue = 네이팜 데미지 비율(로켓 데미지 대비), Data.upgradeValue = 화염 지대 지속시간(초)
// Data.finalValue = 강화당 틱 감소량(초, napalmTickInterval에서 차감)
// Data.loopInterval/damageMultiplier/lifetime/spawnRange = 플레임타워 튜닝
public class GreenHasaNapalmSkill : SkillBase
{
    public GreenHasaNapalmSkill(SkillDataSO data) : base(data) { }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        if (hero is GreenHasa gh)
        {
            gh.hasNapalm = true;
            gh.napalmDamageRatio = Data.equipValue;
            gh.napalmDuration = Data.upgradeValue;
        }
    }

    protected override void OnUpgrade()
    {
        if (Owner is GreenHasa gh)
            gh.napalmTickInterval = Mathf.Max(0.1f, gh.napalmTickInterval - Data.finalValue);
    }

    protected override void OnFinalize()
    {
        if (Owner is GreenHasa gh)
            gh.StartFlameTowerLoop(Data);
    }
}
