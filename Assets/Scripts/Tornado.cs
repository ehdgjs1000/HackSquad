using System.Collections;
using UnityEngine;

// 아이스맨 스킬2 최종: 연쇄 빙결 — 필드를 떠돌아다니며, 닿아있는 적에게 1초마다 데미지를 주는 이동형 장판
public class Tornado : MonoBehaviour
{
    [Header("크기 / 판정 범위")]
    public float vfxScale = 1f;
    public float radius = 2f;

    [Header("데미지")]
    [Range(0f, 5f)] public float damageRatio = 0.5f; // 기본 데미지 대비 비율

    [Header("이동")]
    public float moveSpeed = 2f;
    public float wanderRadius = 6f;           // 스폰 위치 기준 이동 가능 반경
    public float directionChangeInterval = 2f; // 새 목적지를 고르는 주기(초)

    const float TickInterval = 1f;

    float _damage;
    int _monsterLayer;
    Vector3 _origin;
    Vector3 _destination;

    public static void Spawn(GameObject prefab, Vector3 pos, float baseDamage, float duration)
    {
        GameObject go = prefab != null
            ? Instantiate(prefab, pos, Quaternion.identity)
            : new GameObject("Tornado");

        go.transform.position = pos;

        if (!go.TryGetComponent(out Tornado tornado))
            tornado = go.AddComponent<Tornado>();
        tornado.Init(baseDamage);

        Destroy(go, duration);
    }

    void Init(float baseDamage)
    {
        _damage = baseDamage * damageRatio;
        _monsterLayer = LayerMask.GetMask("Monster");
        _origin = transform.position;
        transform.localScale *= vfxScale;

        PickNewDestination();
        StartCoroutine(TickLoop());
        StartCoroutine(WanderLoop());
    }

    IEnumerator TickLoop()
    {
        var wait = new WaitForSeconds(TickInterval);
        while (true)
        {
            var cols = Physics.OverlapSphere(transform.position, radius, _monsterLayer);
            foreach (var col in cols)
                if (col.TryGetComponent(out Monster m))
                    m.TakeDamage(_damage);
            yield return wait;
        }
    }

    IEnumerator WanderLoop()
    {
        var wait = new WaitForSeconds(directionChangeInterval);
        while (true)
        {
            yield return wait;
            PickNewDestination();
        }
    }

    void PickNewDestination()
    {
        Vector2 rand = Random.insideUnitCircle * wanderRadius;
        _destination = _origin + new Vector3(rand.x, 0f, rand.y);
    }

    void Update()
    {
        Vector3 dir = _destination - transform.position;
        dir.y = 0f;

        if (dir.sqrMagnitude < 0.1f)
        {
            PickNewDestination();
            return;
        }

        transform.position += dir.normalized * moveSpeed * Time.deltaTime;
    }
}
