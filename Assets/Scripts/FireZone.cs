using System.Collections;
using UnityEngine;

public class FireZone : MonoBehaviour
{
    [SerializeField] float tickInterval = 0.5f;

    float _damagePerTick;
    float _radius;
    int _monsterLayer;

    public static void Spawn(GameObject vfxPrefab, Vector3 pos, float damagePerTick, float radius, float lifetime, float vfxScale = 1f)
    {
        GameObject go = vfxPrefab != null
            ? Instantiate(vfxPrefab, pos, Quaternion.identity)
            : new GameObject("FireZone");

        go.transform.position = pos;
        go.transform.localScale *= vfxScale;

        if (!go.TryGetComponent(out FireZone fz))
            fz = go.AddComponent<FireZone>();
        fz.Init(damagePerTick, radius);

        Destroy(go, lifetime);
    }

    void Init(float damagePerTick, float radius)
    {
        _damagePerTick = damagePerTick;
        _radius        = radius;
        _monsterLayer  = LayerMask.GetMask("Monster");
    }

    void Start() => StartCoroutine(TickLoop());

    IEnumerator TickLoop()
    {
        var wait = new WaitForSeconds(tickInterval);
        while (true)
        {
            var cols = Physics.OverlapSphere(transform.position, _radius, _monsterLayer);
            foreach (var col in cols)
                if (col.TryGetComponent(out Monster m))
                    m.TakeDamage(_damagePerTick);
            yield return wait;
        }
    }
}
