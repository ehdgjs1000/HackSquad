using UnityEngine;

// 연사형 (람붜 미니건 / 중세기술자 SMG / 킴승기)
public class AutoAttackBehavior : IAttackBehavior
{
    public float AmmoCostPerShot => 1f;

    public void Execute(Hero hero, Monster target)
    {
        if (target == null) return;

        Vector3 spawnPos = hero.firePos != null ? hero.firePos.position : hero.transform.position;
        Vector3 dir = (target.transform.position - spawnPos).normalized;

        if (hero.bulletPrefab == null)
        {
            Debug.LogWarning($"[{hero.name}] bulletPrefab이 할당되지 않았습니다.");
            return;
        }

        var go = Object.Instantiate(hero.bulletPrefab, spawnPos, Quaternion.LookRotation(dir));
        if (go.TryGetComponent(out Bullet bullet))
            bullet.Init(hero.CalcDamage(), dir, hero.stats.pierceCount);

        hero.ConsumeAmmo(1);
    }
}
