using UnityEngine;

public class NinjaBullet : Bullet
{
    [SerializeField] LayerMask monsterLayer;
    Collider[] monsterColls;

    public bool isFinalBullet;
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
