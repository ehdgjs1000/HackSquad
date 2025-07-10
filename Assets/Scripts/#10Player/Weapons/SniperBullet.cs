using UnityEngine;

public class SniperBullet : Bullet
{
    private void Start()
    {
        InitBullet();
    }
    private void InitBullet()
    {
        canPenetrate = true;
        penetrateCount = 10;
    }

    protected override void OnHit()
    {
        
    }
}
