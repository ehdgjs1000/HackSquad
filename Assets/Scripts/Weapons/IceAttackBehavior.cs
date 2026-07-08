using UnityEngine;

// 아이스맨 — 탄환 대신 가까운 적 위치에 즉시 얼음 장판 생성
// 생성 시 1회 데미지, 장판 위에 머무는 동안 슬로우 지속
// 스킬3 최종(냉기 파쇄) 승급 시, 장판이 생성될 때마다 그 위치에서 고드름 4방향 발사
public class IceAttackBehavior : IAttackBehavior
{
    public float AmmoCostPerShot => 1f;

    public void Execute(Hero hero, Monster target)
    {
        if (target == null) return;

        if (hero is not Iceman iceman)
        {
            Debug.LogWarning($"[{hero.name}] IceAttackBehavior는 Iceman 전용입니다.");
            return;
        }

        float damage = hero.CalcDamage(out bool isCrit) * hero.GetDamageMultiplier(target);
        Vector3 pos = target.transform.position;
        pos.y = 0f;

        // 장판 확장 스킬로 늘어난 비율만큼 VFX 크기도 함께 키움
        float vfxScale = iceman.baseExplosionRadius > 0f ? iceman.stats.explosionRadius / iceman.baseExplosionRadius : 1f;

        IceZone.Spawn(iceman.iceVfxPrefab, pos, damage, iceman.stats.explosionRadius, iceman.slowMultiplier, iceman.iceDuration, isCrit, vfxScale);

        if (iceman.hasIcicleBurst)
            SpawnIcicleBurst(iceman, pos, damage);

        hero.ConsumeAmmo(1);
    }

    // 장판 위치에서 90도 간격으로 고드름 4방향 발사
    void SpawnIcicleBurst(Iceman iceman, Vector3 pos, float damage)
    {
        float baseAngle = Random.Range(0f, 360f);
        for (int i = 0; i < 4; i++)
        {
            Vector3 dir = Quaternion.Euler(0f, baseAngle + i * 90f, 0f) * Vector3.forward;
            Icicle.Spawn(iceman.icicleVfxPrefab, pos, damage, dir);
        }
    }
}
