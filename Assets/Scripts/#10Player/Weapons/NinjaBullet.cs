using UnityEngine;

public class NinjaBullet : Bullet
{
    [SerializeField] LayerMask monsterLayer;
    Collider[] monsterColls;
    ParticleSystem mainParticle;

    public bool isFinalBullet;
    private void Start()
    {
        InitBullet();
        mainParticle = GetComponent<ParticleSystem>();
        mainParticle.Play(true);
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
