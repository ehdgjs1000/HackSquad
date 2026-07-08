using UnityEngine;

// 아이스맨 고드름 투사체 — 기본 공격마다 발사되며, 스킬3 최종(결빙 폭풍)에서는 장판 위치에서 사방으로 발사됨
public class Icicle : MonoBehaviour
{
    public float speed = 12f;
    public float maxLifetime = 3f;
    [Range(0f, 5f)] public float damageRatio = 0.5f; // 기본 데미지 대비 비율

    float _damage;
    Vector3 _direction;
    int _monsterLayer;

    static readonly Quaternion SpawnRotation = Quaternion.Euler(0f, 0f, 0f);

    public static void Spawn(GameObject prefab, Vector3 pos, float baseDamage, Vector3 direction)
    {
        GameObject go = prefab != null
            ? Instantiate(prefab, pos, SpawnRotation)
            : new GameObject("Icicle");

        go.transform.position = pos;
        go.transform.rotation = SpawnRotation;

        if (!go.TryGetComponent(out Icicle icicle))
            icicle = go.AddComponent<Icicle>();
        icicle.Init(baseDamage, direction);
    }

    void Init(float baseDamage, Vector3 direction)
    {
        _damage = baseDamage * damageRatio;
        _direction = direction.normalized;
        _monsterLayer = LayerMask.GetMask("Monster");
        Destroy(gameObject, maxLifetime);
    }

    void Update()
    {
        transform.position += _direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _monsterLayer) == 0) return;
        if (!other.TryGetComponent(out Monster monster)) return;

        monster.TakeDamage(_damage);
        Destroy(gameObject);
    }
}
