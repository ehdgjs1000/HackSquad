using UnityEngine;

// 로보틱 — 탄환 대신 가까운 적 위치에 지속딜 장판 생성 (개수는 암흑확산 스킬로 증가)
public class DarkZoneAttackBehavior : IAttackBehavior
{
    public float AmmoCostPerShot => 1f;

    public void Execute(Hero hero, Monster target)
    {
        if (target == null) return;

        if (hero is not Robotic bot)
        {
            Debug.LogWarning($"[{hero.name}] DarkZoneAttackBehavior는 Robotic 전용입니다.");
            return;
        }

        SpawnZone(bot, target.transform.position);

        // 암흑확산: 장판 추가 개수만큼 무작위 몬스터 위치에 추가 생성
        if (bot.extraZoneCount > 0)
        {
            var monsters = Object.FindObjectsByType<Monster>(FindObjectsSortMode.None);
            for (int i = 0; i < bot.extraZoneCount; i++)
            {
                Vector3 pos = monsters.Length > 0
                    ? monsters[Random.Range(0, monsters.Length)].transform.position
                    : target.transform.position;
                SpawnZone(bot, pos);
            }
        }

        hero.ConsumeAmmo(1);
    }

    void SpawnZone(Robotic bot, Vector3 targetPos)
    {
        targetPos.y = 0f;
        float damage = bot.stats.attackDamage;

        var config = new DarkZoneConfig
        {
            radius = bot.zoneRadius,
            lifetime = bot.zoneLifetime,
            tickInterval = bot.zoneTickInterval,
            damagePerTick = damage * bot.zoneDamagePerTickRatio,

            hasEye = bot.hasDarkEye,
            eyeRadius = bot.eyeRadius,
            eyeDamagePerTick = damage * bot.eyeDamagePerTickRatio,

            hasInitialBurst = bot.hasInitialBurst,
            initialBurstDamage = damage * bot.initialBurstDamageRatio,

            hasPoisonMist = bot.hasPoisonMist,
            poison = new PoisonApplication
            {
                damagePerStack = damage * bot.poisonDamagePerStackRatio,
                maxStacks = bot.poisonMaxStacks,
                explodeDelay = bot.poisonExplodeDelay,
                explosionVfxPrefab = bot.poisonExplosionVfxPrefab,
                poisonUiPrefab = bot.poisonUiPrefab
            },

            hasVoidCollapse = bot.hasVoidCollapse,
            voidCollapseVfxPrefab = bot.voidCollapseVfxPrefab,
            voidCollapseDamage = damage * bot.voidCollapseDamageRatio,
            voidCollapseRadius = bot.voidCollapseRadius
        };

        DarkZone.Spawn(bot.zoneVfxPrefab, targetPos, config);
    }
}
