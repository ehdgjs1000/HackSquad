using UnityEngine;

// 여러 히어로가 공용으로 쓰는 상태이상 VFX 저장소 (씬에 1개 배치)
public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance { get; private set; }

    [Header("상태이상 VFX")]
    public GameObject burnVfxPrefab; // 적 화상(불붙음) VFX — 몬스터 자식으로 붙었다가 화상 종료 시 몬스터가 직접 파괴

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public static GameObject SpawnBurnVFX(Transform parent)
    {
        if (Instance == null || Instance.burnVfxPrefab == null) return null;
        return Instantiate(Instance.burnVfxPrefab, parent.position, Quaternion.identity, parent);
    }
}
