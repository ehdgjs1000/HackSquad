using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    [Header("Data")]
    public HeroDataSO heroData;
    [HideInInspector] public HeroStats stats = HeroStats.Default;

    [Header("Weapon")]
    public Transform firePos;
    public GameObject bulletPrefab;
    public GameObject hitVFX;
    public IAttackBehavior attackBehavior;

    [Header("Skills (max 3 slots)")]
    public List<SkillBase> skills = new();

    int _currentAmmo;
    bool _reloading;
    float _attackTimer;
    LayerMask _monsterLayer;

    Monster _currentTarget;

    // OverlapSphere 결과 버퍼 — Update는 단일 스레드라 static 공유 안전
    static readonly Collider[] _overlapBuffer = new Collider[64];

    void Start()
    {
        _monsterLayer = LayerMask.GetMask("Monster");
        if (heroData != null && heroData.statsSO != null)
            stats = heroData.statsSO.stats;
        else
            Debug.LogWarning($"[{name}] heroData 또는 statsSO가 할당되지 않았습니다. 기본값을 사용합니다.");
        Init();
        _currentAmmo = stats.maxAmmo;
        attackBehavior ??= new AutoAttackBehavior();
    }

    protected abstract void Init();

    public virtual List<SkillBase> GetSkillCandidates()
    {
        return new List<SkillBase>
        {
            new AttackSpeedSkill(),
            new DamageSkill(),
            new AmmoSkill()
        };
    }

    void Update()
    {
        _currentTarget = FindNearestMonster();
        if (_currentTarget == null) return;

        Vector3 dir = (_currentTarget.transform.position - transform.position).normalized;
        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir);

        if (_reloading) return;

        _attackTimer -= Time.deltaTime;
        if (_attackTimer <= 0f)
            TryAttack();
    }

    void TryAttack()
    {
        if (_currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        attackBehavior?.Execute(this, _currentTarget);
        _attackTimer = stats.attackSpeed;

        foreach (var skill in skills) skill.OnAttack(this, _currentTarget);
    }

    public void ConsumeAmmo(int amount = 1)
    {
        _currentAmmo = Mathf.Max(0, _currentAmmo - amount);
    }

    IEnumerator Reload()
    {
        _reloading = true;
        yield return new WaitForSeconds(stats.reloadSpeed);
        _currentAmmo = stats.maxAmmo;
        _reloading = false;
        foreach (var skill in skills) skill.OnReload(this);
    }

    public float CalcDamage()
    {
        bool isCrit = Random.value < stats.critChance;
        return stats.attackDamage * (isCrit ? stats.critDamage : 1f);
    }

    Monster FindNearestMonster()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, stats.range, _overlapBuffer, _monsterLayer);

        Monster nearest = null;
        float minSqrDist = float.MaxValue;

        for (int i = 0; i < count; i++)
        {
            if (!_overlapBuffer[i].TryGetComponent(out Monster m)) continue;
            float sqrDist = (_overlapBuffer[i].transform.position - transform.position).sqrMagnitude;
            if (sqrDist < minSqrDist) { minSqrDist = sqrDist; nearest = m; }
        }

        return nearest;
    }
}
