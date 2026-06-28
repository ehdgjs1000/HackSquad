using UnityEngine;

public abstract class SkillBase
{
    public string skillName;
    public string description;
    public int level;
    public const int MaxLevel = 3;
    public bool IsFinalized { get; private set; }

    public bool IsMaxLevel => level >= MaxLevel;

    public void Upgrade()
    {
        if (IsMaxLevel) return;
        level++;
        OnUpgrade();
    }

    public void Finalize()
    {
        IsFinalized = true;
        OnFinalize();
    }

    protected virtual void OnUpgrade() { }
    protected virtual void OnFinalize() { }

    // 히어로가 공격 직전/후에 호출
    public virtual void OnAttack(Hero hero, Monster target) { }
    public virtual void OnKill(Hero hero, Monster target) { }
    public virtual void OnTick(Hero hero) { }
}