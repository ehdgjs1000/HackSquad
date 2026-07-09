using UnityEngine;

// 로보틱 장판(DarkZone) 생성에 필요한 수치를 한 번에 묶은 데이터
public struct DarkZoneConfig
{
    public float radius;
    public float lifetime;
    public float tickInterval;
    public float damagePerTick;

    // 스킬1 최종: 어둠의 눈 — 장판 중앙 별도 반경에 추가 큰 피해
    public bool hasEye;
    public float eyeRadius;
    public float eyeDamagePerTick;

    // 스킬3: 검은가시 — 생성 즉시 1회 범위 피해
    public bool hasInitialBurst;
    public float initialBurstDamage;

    // 스킬2 최종: 독무 — 생성 시 범위 내 전체에게 독 부여
    public bool hasPoisonMist;
    public PoisonApplication poison;

    // 스킬3 최종: 공허붕괴 — 장판 종료 시 거대한 폭발
    public bool hasVoidCollapse;
    public GameObject voidCollapseVfxPrefab;
    public float voidCollapseDamage;
    public float voidCollapseRadius;
}
