using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    public static DamageTextManager Instance { get; private set; }

    public GameObject damageTextPrefab;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public static void Show(Vector3 worldPos, float damage, bool isCrit = false)
    {
        if (Instance == null || Instance.damageTextPrefab == null) return;
        var go = Instantiate(Instance.damageTextPrefab, worldPos + Vector3.up * 1.5f, Quaternion.identity);
        if (go.TryGetComponent(out DamageText dt))
            dt.Init(damage, isCrit);
    }
}
