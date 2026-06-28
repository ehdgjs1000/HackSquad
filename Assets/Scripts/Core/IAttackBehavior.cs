public interface IAttackBehavior
{
    // MaxAmmo=1 타입은 장전속도가 공격 주기
    void Execute(Hero hero, Monster target);
    float AmmoCostPerShot { get; }  // 연사=1, 지속형=초당n, MaxAmmo=1타입=전량
}