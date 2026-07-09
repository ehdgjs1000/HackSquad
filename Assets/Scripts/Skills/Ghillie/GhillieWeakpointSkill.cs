using System.Collections.Generic;

// 스킬 3: 약점 분석 — 이 히어로가 이미 명중시킨 적을 다시 맞히면 데미지 배율 적용 / 최종: 지원사격
public class GhillieWeakpointSkill : SkillBase
{
    readonly HashSet<Monster> _hitMonsters = new();
    float _multiplier;

    public GhillieWeakpointSkill(SkillDataSO data) : base(data) { }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        if (hero is Ghillie gh)
            _multiplier = gh.weakpointMultiplierEquip;
    }

    protected override void OnUpgrade()
    {
        if (Owner is Ghillie gh)
            _multiplier += gh.weakpointMultiplierUpgrade;
    }

    public override float GetDamageMultiplier(Monster target)
        => target != null && _hitMonsters.Contains(target) ? _multiplier : 1f;

    public override void OnAttack(Hero hero, Monster target)
    {
        if (target != null)
            _hitMonsters.Add(target);
    }

    protected override void OnFinalize()
    {
        if (Owner is Ghillie gh)
            gh.StartSupportFireLoop();
    }
}
