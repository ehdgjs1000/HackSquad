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

        float damage = hero.CalcDamage(out bool isCrit) * hero.GetDamageMultiplier(target);
        FireMainShot(hero, spawnPos, dir, damage, isCrit);

        // 더블 공격: 같은 방향으로 추가 발사 (길리슈트 헤드샷 최종)
        int extraShots = hero is Ghillie gh ? gh.extraShotCount : 0;
        for (int i = 0; i < extraShots; i++)
            SpawnBullet(hero, spawnPos, dir, damage, isCrit);

        // 확률 추가 발사 (복면 연사 스킬): 매우 짧은 텀을 두고 한 발 더 발사
        if (hero is MaskHero mask && Random.value < mask.extraShotChance)
            hero.RunDelayed(mask.extraShotDelay, () => SpawnBullet(hero, spawnPos, dir, damage, isCrit));

        // 후방 지원: 반대 방향으로 추가 발사
        if (hero.stats.backAttackRatio > 0f)
            SpawnBullet(hero, spawnPos, -dir, damage * hero.stats.backAttackRatio, isCrit);

        hero.ConsumeAmmo(1);
    }

    // 기본 발사: 복면 강화탄 최종(더블샷)이면 진행방향에 수직으로 살짝 벌어진 2발을 나란히 동시 발사
    void FireMainShot(Hero hero, Vector3 pos, Vector3 dir, float damage, bool isCrit)
    {
        if (hero is MaskHero mask && mask.hasDoubleShot)
        {
            Vector3 side = Vector3.Cross(Vector3.up, dir).normalized * (mask.doubleShotOffset * 0.5f);
            SpawnBullet(hero, pos + side, dir, damage, isCrit);
            SpawnBullet(hero, pos - side, dir, damage, isCrit);
        }
        else
        {
            SpawnBullet(hero, pos, dir, damage, isCrit);
        }
    }

    void SpawnBullet(Hero hero, Vector3 pos, Vector3 dir, float damage, bool isCrit)
    {
        var go = Object.Instantiate(hero.bulletPrefab, pos, Quaternion.LookRotation(dir));

        float damageIncreasePerPierce = 0f;
        float splitChance = 0f;
        float splitAngle = 0f;

        if (hero is MaskHero mask)
        {
            go.transform.localScale *= mask.bulletScaleMultiplier;

            if (mask.hasPierceDamageBonus)
                damageIncreasePerPierce = mask.damageIncreasePerPierce;

            if (mask.hasSplitShot)
            {
                splitChance = mask.splitChance;
                splitAngle = mask.splitAngle;
            }
        }

        if (go.TryGetComponent(out Bullet bullet))
            bullet.Init(damage, dir, hero.stats.pierceCount, hero.hitVFX, isCrit, damageIncreasePerPierce, splitChance, splitAngle);
    }
}
