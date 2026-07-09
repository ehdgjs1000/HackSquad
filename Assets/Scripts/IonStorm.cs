using UnityEngine;

// 복면 스킬3 최종: 이온폭풍 — 승급 즉시 생성, 이후 주기적으로 재생성되는 일회성 광역 강타
public class IonStorm : MonoBehaviour
{
    [Header("크기 / 판정 범위")]
    public float vfxScale = 1f;
    public float radius = 3f;

    [Header("데미지")]
    public float damageRatio = 3f; // 기본 공격력 대비 배율(일회성 강타라 크게)

    public static void Spawn(GameObject prefab, Vector3 pos, float baseDamage, float vfxLifetime)
    {
        GameObject go = prefab != null
            ? Instantiate(prefab, pos, Quaternion.identity)
            : new GameObject("IonStorm");

        go.transform.position = pos;

        if (!go.TryGetComponent(out IonStorm storm))
            storm = go.AddComponent<IonStorm>();
        storm.Explode(baseDamage);

        Destroy(go, vfxLifetime);
    }

    void Explode(float baseDamage)
    {
        transform.localScale *= vfxScale;

        int monsterLayer = LayerMask.GetMask("Monster");
        float damage = baseDamage * damageRatio;

        var cols = Physics.OverlapSphere(transform.position, radius, monsterLayer);
        foreach (var col in cols)
            if (col.TryGetComponent(out Monster m))
                m.TakeDamage(damage);
    }
}
