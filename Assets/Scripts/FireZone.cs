using System.Collections;
using UnityEngine;

public class FireZone : MonoBehaviour
{
    float _damagePerTick;
    float _radius;
    int _monsterLayer;

    public static void Spawn(Vector3 pos, float damagePerTick, float radius, float lifetime, GameObject vfxPrefab = null)
    {
        var go = new GameObject("FireZone");
        go.transform.position = pos;
        var fz = go.AddComponent<FireZone>();
        fz._damagePerTick = damagePerTick;
        fz._radius        = radius;
        fz._monsterLayer  = LayerMask.GetMask("Monster");

        if (vfxPrefab != null)
        {
            var vfx = Instantiate(vfxPrefab, pos, Quaternion.identity, go.transform);
            // 파티클 수명 관리는 부모 오브젝트(FireZone)가 Destroy하므로 별도 처리 불필요
            if (!vfx.TryGetComponent<ParticleSystem>(out _))
                Destroy(vfx, lifetime);
        }

        Destroy(go, lifetime);
    }

    void Start() => StartCoroutine(TickLoop());

    IEnumerator TickLoop()
    {
        var wait = new WaitForSeconds(0.5f);
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
