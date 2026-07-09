using System.Collections;
using UnityEngine;

// 사무라이 스킬: 메테오 — 하늘에서 낙하하는 연출 후 착지 지점에 폭발 + 광역 피해
public class Meteor : MonoBehaviour
{
    public static void Spawn(GameObject meteorPrefab, GameObject explosionVfxPrefab, Vector3 groundPos,
        float fallHeight, float fallDuration, float damage, float radius)
    {
        Vector3 startPos = groundPos + Vector3.up * fallHeight;

        GameObject go = meteorPrefab != null
            ? Instantiate(meteorPrefab, startPos, Quaternion.identity)
            : new GameObject("Meteor");
        go.transform.position = startPos;

        if (!go.TryGetComponent(out Meteor meteor))
            meteor = go.AddComponent<Meteor>();
        meteor.StartCoroutine(meteor.FallRoutine(groundPos, fallDuration, explosionVfxPrefab, damage, radius));
    }

    IEnumerator FallRoutine(Vector3 groundPos, float duration, GameObject explosionVfxPrefab, float damage, float radius)
    {
        Vector3 start = transform.position;
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(start, groundPos, t / duration);
            yield return null;
        }
        transform.position = groundPos;

        Explode(explosionVfxPrefab, groundPos, damage, radius);
        Destroy(gameObject);
    }

    void Explode(GameObject explosionVfxPrefab, Vector3 pos, float damage, float radius)
    {
        if (explosionVfxPrefab != null)
        {
            var vfx = Instantiate(explosionVfxPrefab, pos, Quaternion.identity);
            if (!vfx.TryGetComponent<ParticleSystem>(out var ps))
                Destroy(vfx, 3f);
            else if (ps.main.stopAction != ParticleSystemStopAction.Destroy)
                Destroy(vfx, ps.main.duration + ps.main.startLifetime.constantMax);
        }

        int monsterLayer = LayerMask.GetMask("Monster");
        var cols = Physics.OverlapSphere(pos, radius, monsterLayer);
        foreach (var col in cols)
            if (col.TryGetComponent(out Monster m))
                m.TakeDamage(damage);
    }
}
