using UnityEngine;

// 사무라이 — 관통형 화염구체를 발사하는 히어로. 명중 시 화상 적용
// 화상은 1틱=1초(burnMaxTicks만큼 지속), burnExplodeEveryTicks마다 연소폭발(광역 피해) 발생
// 기본 공격의 관통은 stats.pierceCount(HeroStatsSO)로 설정
public class Samurai : Hero
{
    [Header("화상 (기본 공격 명중 시 적용, 피해는 그 발의 데미지 대비 비율)")]
    public float burnDamagePerTickRatio = 0.2f; // 틱당 화상 피해 = 명중 데미지 * 비율
    public int burnMaxTicks = 3;                // 화상 최대 지속 틱(1틱=1초)
    public int burnExplodeEveryTicks = 3;       // 몇 틱마다 연소폭발이 발생하는지

    [Header("연소폭발 (burnExplodeEveryTicks마다 발생하는 광역 폭발)")]
    public GameObject combustionExplosionVfxPrefab;
    public float burnExplosionDamageRatio = 1.5f; // 폭발 피해 = 명중 데미지 * 비율
    public float burnExplosionRadius = 2.5f;

    [Header("최종 스킬 VFX (로직 미구현, 프리팹만 우선 캐싱)")]
    public GameObject meteorVfxPrefab;
    public GameObject fireWaveVfxPrefab;

    protected override void Init()
    {
        attackBehavior = new AutoAttackBehavior();
    }
}
