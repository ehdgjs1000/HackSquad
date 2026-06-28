using System;

[Serializable]
public struct HeroStats
{
    public float attackSpeed;   // 발사 간격(초)
    public float attackDamage;
    public float critChance;    // 0~1
    public float critDamage;    // 배율 (2.0 = 200%)
    public float range;
    public float maxHp;
    public int   maxAmmo;
    public float reloadSpeed;   // 장전 시간(초)

    public static HeroStats Default => new HeroStats
    {
        attackSpeed  = 0.5f,
        attackDamage = 10f,
        critChance   = 0.1f,
        critDamage   = 2.0f,
        range        = 8f,
        maxHp        = 100f,
        maxAmmo      = 30,
        reloadSpeed  = 1.5f
    };
}