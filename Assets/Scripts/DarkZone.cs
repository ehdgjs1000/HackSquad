using System.Collections;
using UnityEngine;

// 로보틱 기본 공격: 지속딜 장판 — lifetime 동안 tickInterval마다 radius 내 전체에게 피해
// 스킬1 최종(어둠의 눈): 중앙 eyeRadius 내 추가 큰 피해 / 스킬2 최종(독무): 생성 시 범위 내 전체에게 독 부여
// 스킬3(검은가시): 생성 즉시 1회 범위 피해 / 스킬3 최종(공허붕괴): 종료 시 거대한 폭발
public class DarkZone : MonoBehaviour
{
    DarkZoneConfig _cfg;
    int _monsterLayer;

    public static void Spawn(GameObject vfxPrefab, Vector3 pos, DarkZoneConfig config)
    {
        GameObject go = vfxPrefab != null
            ? Instantiate(vfxPrefab, pos, Quaternion.identity)
            : new GameObject("DarkZone");
        go.transform.position = pos;

        if (!go.TryGetComponent(out DarkZone zone))
            zone = go.AddComponent<DarkZone>();
        zone.Init(config);
    }

    void Init(DarkZoneConfig config)
    {
        _cfg = config;
        _monsterLayer = LayerMask.GetMask("Monster");

        if (_cfg.hasInitialBurst)
            DealAreaDamage(_cfg.radius, _cfg.initialBurstDamage);

        if (_cfg.hasPoisonMist)
            ApplyPoisonToArea();

        StartCoroutine(Lifecycle());
    }

    IEnumerator Lifecycle()
    {
        var tickWait = new WaitForSeconds(_cfg.tickInterval);
        float elapsed = 0f;

        while (elapsed < _cfg.lifetime)
        {
            yield return tickWait;
            elapsed += _cfg.tickInterval;

            DealAreaDamage(_cfg.radius, _cfg.damagePerTick);
            if (_cfg.hasEye)
                DealAreaDamage(_cfg.eyeRadius, _cfg.eyeDamagePerTick);
        }

        if (_cfg.hasVoidCollapse)
            TriggerVoidCollapse();

        Destroy(gameObject);
    }

    void DealAreaDamage(float radius, float damage)
    {
        var cols = Physics.OverlapSphere(transform.position, radius, _monsterLayer);
        foreach (var col in cols)
            if (col.TryGetComponent(out Monster m))
                m.TakeDamage(damage);
    }

    void ApplyPoisonToArea()
    {
        var cols = Physics.OverlapSphere(transform.position, _cfg.radius, _monsterLayer);
        foreach (var col in cols)
            if (col.TryGetComponent(out Monster m))
                m.ApplyPoison(_cfg.poison);
    }

    void TriggerVoidCollapse()
    {
        if (_cfg.voidCollapseVfxPrefab != null)
        {
            var vfx = Instantiate(_cfg.voidCollapseVfxPrefab, transform.position, Quaternion.identity);
            if (!vfx.TryGetComponent<ParticleSystem>(out var ps))
                Destroy(vfx, 3f);
            else if (ps.main.stopAction != ParticleSystemStopAction.Destroy)
                Destroy(vfx, ps.main.duration + ps.main.startLifetime.constantMax);
        }

        DealAreaDamage(_cfg.voidCollapseRadius, _cfg.voidCollapseDamage);
    }
}
