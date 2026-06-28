using UnityEngine;

// 연사형 (람붜 미니건 / 중세기술자 SMG / 킴승기)
// 발사 간격마다 1발, 탄당 1 소모
public class AutoAttackBehavior : IAttackBehavior
{
    public float AmmoCostPerShot => 1f;

    public void Execute(Hero hero, Monster target)
    {
        if (target == null) return;
        float dmg = hero.CalcDamage();
        target.TakeDamage(dmg);
        hero.ConsumeAmmo(1);
        Debug.Log($"[AutoAttack] {hero.name} → {target.name} : {dmg:F1}dmg");
    }
}