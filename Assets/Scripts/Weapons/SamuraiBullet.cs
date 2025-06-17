using UnityEngine;

public class SamuraiBullet : Bullet
{
    private void Start()
    {
        InitBullet();
    }
    private void InitBullet()
    {
        canPenetrate = true;
        penetrateCount = 3;
    }

    protected override void OnHit()
    {
        
    }
}
