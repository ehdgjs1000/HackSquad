using UnityEngine;

// 화상(Burn) 적용에 필요한 수치를 한 번에 묶어 전달하기 위한 데이터 (Bullet.Init 파라미터 폭증 방지)
public struct BurnApplication
{
    public float damagePerTick;
    public float explosionDamage;
    public float explosionRadius;
    public int maxTicks;
    public int explodeEveryTicks;
    public GameObject explosionVfxPrefab;

    public bool IsActive => maxTicks > 0;
}
