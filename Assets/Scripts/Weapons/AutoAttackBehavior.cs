using UnityEngine;

// 연사형 (람붜 미니건 / 중세기술자 SMG / 킴승기)
public class AutoAttackBehavior : IAttackBehavior
{
    public float AmmoCostPerShot => 1f;

    public void Execute(Hero hero, Monster target)
    {
        if (target == null) return;

        if (hero.bulletPrefab == null)
        {
            Debug.LogWarning($"[{hero.name}] bulletPrefab이 할당되지 않았습니다.");
            return;
        }

        Vector3 spawnPos = hero.firePos != null ? hero.firePos.position : hero.transform.position;
        Vector3 dir = target.transform.position - spawnPos;
        dir.y = 0f;
        dir.Normalize();

        // 발사각 적용 (Y축 기준 수평 랜덤 편차)
        if (hero.stats.spreadAngle > 0f)
        {
            float offset = Random.Range(-hero.stats.spreadAngle * 0.5f, hero.stats.spreadAngle * 0.5f);
            dir = Quaternion.AngleAxis(offset, Vector3.up) * dir;
        }

        SpawnBullet(hero, spawnPos, dir, hero.CalcDamage());

        // 후방 지원: 반대 방향으로 추가 발사
        if (hero.stats.backAttackRatio > 0f)
            SpawnBullet(hero, spawnPos, -dir, hero.CalcDamage() * hero.stats.backAttackRatio);

        hero.ConsumeAmmo(1);
    }

    void SpawnBullet(Hero hero, Vector3 pos, Vector3 dir, float damage)
    {
        var go = Object.Instantiate(hero.bulletPrefab, pos, Quaternion.LookRotation(dir));
        if (go.TryGetComponent(out Bullet bullet))
            bullet.Init(damage, dir, hero.stats.pierceCount, hero.hitVFX);
    }
}
