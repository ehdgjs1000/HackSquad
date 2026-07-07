using UnityEngine;

public abstract class SkillBase
{
    public string skillName;
    public string initDescription;
    public string description;
    public string finalDescription;
    public int level;
    public const int MaxLevel = 3;
    public bool IsFinalized { get; private set; }
    public bool IsMaxLevel => level >= MaxLevel;

    protected SkillDataSO Data { get; private set; }
    protected Hero Owner { get; private set; }

    protected SkillBase(SkillDataSO data)
    {
        Data = data;
        skillName = data.skillName;
        initDescription = data.initDescription;
        description = data.description;
        finalDescription = data.finalDescription;
    }

    // 미사용 레거시 스킬(AmmoSkill 등)을 위한 하드코딩 호환용 생성자
    protected SkillBase() { }

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
