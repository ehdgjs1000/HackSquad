using UnityEngine;

public abstract class SkillBase
{
    public string skillName;
    public string description;
    public int level;
    public const int MaxLevel = 3;
    public bool IsFinalized { get; private set; }
    public bool IsMaxLevel => level >= MaxLevel;

    protected Hero Owner { get; private set; }

    public virtual void OnEquip(Hero hero)
    {
        Owner = hero;
    }

    public void Upgrade()
    {
        if (IsMaxLevel) return;
        level++;
        OnUpgrade();
        if (IsMaxLevel) Finalize();
    }

    public void Finalize()
    {
        IsFinalized = true;
        OnFinalize();
    }

    protected virtual void OnUpgrade() { }
    protected virtual void OnFinalize() { }

    public virtual void OnAttack(Hero hero, Monster target) { }
    public virtual void OnKill(Hero hero, Monster target) { }
    public virtual void OnTick(Hero hero) { }
    public virtual void OnReload(Hero hero) { }
}
