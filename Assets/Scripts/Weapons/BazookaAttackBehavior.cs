using UnityEngine;

// 바주카 — 타겟 추적 로켓, 착탄 시 AoE 폭발
public class BazookaAttackBehavior : IAttackBehavior
{
    public float AmmoCostPerShot => 1f;

    public void Execute(Hero hero, Monster target)
    {
        if (target == null) return;

        if (hero.bulletPrefab == null)
        {
            Debug.LogWarning($"[{hero.name}] bulletPrefab(Rocket)이 할당되지 않았습니다.");
            return;
        }

        Vector3 spawnPos = hero.firePos != null ? hero.firePos.position : hero.transform.position;
        Vector3 dir = target.transform.position - spawnPos;
        dir.y = 0f;
        dir.Normalize();

        int clusterCount    = hero is GreenHasa gh  ? gh.clusterCount : 0;
        bool hasNapalm      = hero is GreenHasa gh2 && gh2.hasNapalm;
        GameObject napalmVFX = hero is GreenHasa gh3 ? gh3.napalmVFX : null;
        float napalmDamageRatio = hero is GreenHasa gh4 ? gh4.napalmDamageRatio : 0.3f;
        float napalmDuration    = hero is GreenHasa gh5 ? gh5.napalmDuration : 5f;
        float napalmTickInterval = hero is GreenHasa gh6 ? gh6.napalmTickInterval : -1f;
        float vfxScale = hero.baseExplosionRadius > 0f ? hero.stats.explosionRadius / hero.baseExplosionRadius : 1f;

        float damage = hero.CalcDamage(out bool isCrit);

        var go = Object.Instantiate(hero.bulletPrefab, spawnPos, Quaternion.LookRotation(dir));
        if (go.TryGetComponent(out Rocket rocket))
            rocket.Init(damage, target.transform, hero.stats.explosionRadius, hero.hitVFX, clusterCount, hasNapalm, napalmVFX, vfxScale, napalmDamageRatio, napalmDuration, napalmTickInterval, isCrit);

        hero.ConsumeAmmo(1);
    }
}
