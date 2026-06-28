using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [Header("Stats")]
    public HeroStats stats = HeroStats.Default;

    [Header("Weapon")]
    public IAttackBehavior attackBehavior;

    [Header("Skills (max 3 slots)")]
    public List<SkillBase> skills = new();

    // 탄약 상태
    int _currentAmmo;
    bool _reloading;
    float _attackTimer;

    Monster _currentTarget;

    void Start()
    {
        _currentAmmo = stats.maxAmmo;
        // 기본 공격 행동: 연사형
        attackBehavior ??= new AutoAttackBehavior();
    }

    void Update()
    {
        _currentTarget = FindNearestMonster();
        if (_currentTarget == null) return;

        // 히어로가 몬스터 방향으로 회전
        Vector3 dir = (_currentTarget.transform.position - transform.position).normalized;
        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir);

        if (_reloading) return;

        _attackTimer -= Time.deltaTime;
        if (_attackTimer <= 0f)
        {
            TryAttack();
        }
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
    }

    public float CalcDamage()
    {
        bool isCrit = Random.value < stats.critChance;
        return stats.attackDamage * (isCrit ? stats.critDamage : 1f);
    }

    Monster FindNearestMonster()
    {
        Monster nearest = null;
        float minDist = stats.range;
        foreach (var m in FindObjectsByType<Monster>(FindObjectsSortMode.None))
        {
            float d = Vector3.Distance(transform.position, m.transform.position);
            if (d < minDist) { minDist = d; nearest = m; }
        }
        return nearest;
    }
}