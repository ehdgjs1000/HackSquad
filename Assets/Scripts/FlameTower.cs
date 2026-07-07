using System.Collections;
using UnityEngine;

public class FlameTower : MonoBehaviour
{
    [Header("References")]
    public Transform firePos;

    [Header("Attack")]
    [SerializeField] float attackRange = 5f;   // 사거리(길이)
    [SerializeField] float attackAngle = 60f;  // 공격 범위(콘 각도, 좌우 절반씩)

    const float TickInterval = 0.1f; // 0.1초마다 데미지

    float _damage;
    int _monsterLayer;
    ParticleSystem _flamePS;

    public static void Spawn(GameObject prefab, Vector3 pos, float damage, float lifetime)
    {
        GameObject go = prefab != null
            ? Instantiate(prefab, pos, Quaternion.identity)
            : new GameObject("FlameTower");

        go.transform.position = pos;

        if (!go.TryGetComponent(out FlameTower ft))
            ft = go.AddComponent<FlameTower>();
        ft.Init(damage);

        Destroy(go, lifetime);
    }

    void Init(float damage)
    {
        _damage = damage;
        _monsterLayer = LayerMask.GetMask("Monster");

        // 자식 ParticleSystem(화염 VFX) 자동 탐색 후 재생
        _flamePS = GetComponentInChildren<ParticleSystem>();
        if (_flamePS != null) _flamePS.Play();

        StartCoroutine(AttackLoop());
    }

    void Update()
    {
        Monster nearest = FindNearest();
        if (nearest == null) return;

        Vector3 dir = nearest.transform.position - transform.position;
        dir.y = 0f;
        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir);
    }

    IEnumerator AttackLoop()
    {
        var wait = new WaitForSeconds(TickInterval);
        while (true)
        {
            DamageInCone();
            yield return wait;
        }
    }

    void DamageInCone()
    {
        Vector3 origin  = firePos != null ? firePos.position : transform.position;
        Vector3 forward = firePos != null ? firePos.forward  : transform.forward;

        var cols = Physics.OverlapSphere(origin, attackRange, _monsterLayer);
        foreach (var col in cols)
        {
            Vector3 toMonster = (col.transform.position - origin).normalized;
            if (Vector3.Angle(forward, toMonster) > attackAngle * 0.5f) continue;
            if (col.TryGetComponent(out Monster m))
                m.TakeDamage(_damage * TickInterval);
        }
    }

    Monster FindNearest()
    {
        var cols = Physics.OverlapSphere(transform.position, attackRange, _monsterLayer);
        Monster nearest = null;
        float minSqr = float.MaxValue;
        foreach (var col in cols)
        {
            float sqr = (col.transform.position - transform.position).sqrMagnitude;
            if (sqr < minSqr && col.TryGetComponent(out Monster m))
            {
                minSqr  = sqr;
                nearest = m;
            }
        }
        return nearest;
    }
}
