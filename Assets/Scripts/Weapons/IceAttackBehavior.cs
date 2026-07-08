using UnityEngine;

// 아이스맨 — 탄환 대신 가까운 적 위치에 즉시 얼음 장판 생성
// 생성 시 1회 데미지, 장판 위에 머무는 동안 슬로우 지속
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
        SpawnZoneAt(iceman, target.transform.position, damage, isCrit);

        // 스킬 2 최종: 연쇄 빙결 — 다른 적들에게도 동시에 장판 생성
        if (iceman.chainZoneCount > 0)
            SpawnChainZones(iceman, target, damage, isCrit);

        hero.ConsumeAmmo(1);
    }

    void SpawnChainZones(Iceman iceman, Monster excludeTarget, float damage, bool isCrit)
    {
        var monsters = Object.FindObjectsByType<Monster>(FindObjectsSortMode.None);
        int spawned = 0;
        foreach (var m in monsters)
        {
            if (spawned >= iceman.chainZoneCount) break;
            if (m == excludeTarget) continue;

            SpawnZoneAt(iceman, m.transform.position, damage, isCrit);
            spawned++;
        }
    }

    void SpawnZoneAt(Iceman iceman, Vector3 pos, float damage, bool isCrit)
    {
        pos.y = 0f;
        IceZone.Spawn(iceman.iceVfxPrefab, pos, damage, iceman.stats.explosionRadius, iceman.slowMultiplier, iceman.iceDuration, isCrit);
    }
}
