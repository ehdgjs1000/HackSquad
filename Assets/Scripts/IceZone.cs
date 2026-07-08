using System.Collections.Generic;
using UnityEngine;

// 아이스맨 얼음 장판 — 생성 시 1회 데미지, 장판 위에 머무는 동안 슬로우 지속 (틱 데미지 없음)
public class IceZone : MonoBehaviour
{
    float _radius;
    float _slowMultiplier;
    int _monsterLayer;

    readonly HashSet<Monster> _inside = new();
    readonly List<Monster> _toRemove = new();

    static readonly Collider[] _buffer = new Collider[32];

    const float GroundOffset = 0.1f; // 바닥에 파묻히지 않도록 생성 위치보다 살짝 위에 스폰

    public static void Spawn(GameObject vfxPrefab, Vector3 pos, float damage, float radius, float slowMultiplier, float duration, bool isCrit = false, float vfxScale = 1f)
    {
        pos.y += GroundOffset;

        GameObject go = vfxPrefab != null
            ? Instantiate(vfxPrefab, pos, Quaternion.identity)
            : new GameObject("IceZone");

        go.transform.position = pos;
        go.transform.localScale *= vfxScale; // 자식 파티클은 Scaling Mode: Hierarchy로 설정되어 함께 스케일됨

        if (!go.TryGetComponent(out IceZone zone))
            zone = go.AddComponent<IceZone>();
        zone.Init(damage, radius, slowMultiplier, duration, isCrit);
    }

    void Init(float damage, float radius, float slowMultiplier, float duration, bool isCrit)
    {
        _radius = radius;
        _slowMultiplier = slowMultiplier;
        _monsterLayer = LayerMask.GetMask("Monster");

        // 생성 즉시 1회 데미지 (지속 틱 데미지 없음)
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _radius, _buffer, _monsterLayer);
        for (int i = 0; i < hitCount; i++)
            if (_buffer[i].TryGetComponent(out Monster m))
                m.TakeDamage(damage, isCrit);

        Destroy(gameObject, duration);
    }

    void Update()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, _radius, _buffer, _monsterLayer);

        _toRemove.Clear();
        _toRemove.AddRange(_inside);

        for (int i = 0; i < count; i++)
        {
            if (!_buffer[i].TryGetComponent(out Monster m)) continue;
            _toRemove.Remove(m);

            if (_inside.Add(m))
                m.AddSlow(_slowMultiplier);
        }

        foreach (var m in _toRemove)
        {
            _inside.Remove(m);
            if (m != null) m.RemoveSlow(_slowMultiplier);
        }
    }

    void OnDestroy()
    {
        foreach (var m in _inside)
            if (m != null) m.RemoveSlow(_slowMultiplier);
    }
}
