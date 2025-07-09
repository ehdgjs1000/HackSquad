using UnityEngine;

public class SamuraiBullet : Bullet
{
    [SerializeField] LayerMask monsterLayer;
    [SerializeField] float exploseRadius;
    [SerializeField] Transform exploseTs;
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
        if (isFinalBullet)
        {
            monsterColls = null;
            monsterColls = Physics.OverlapSphere(transform.position, exploseRadius,monsterLayer);
            foreach (Collider co in monsterColls)
            {
                co.GetComponent<MonsterCtrl>().GetAttack(damage);
                DamagePopUp.Create(new Vector3(co.transform.position.x,
                   co.transform.position.y + 2.0f, co.transform.position.z), damage);
                Instantiate(exploseTs, co.transform.position, Quaternion.identity);
            }
        }
    }
}
