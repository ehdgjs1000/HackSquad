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
        if (BackEndGameData.Instance.UserEvolvingData.evolvingLevel[8] > 1) exploseRadius *= 1.5f;
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
                if(co.GetComponent<MonsterCtrl>() != null) co.GetComponent<MonsterCtrl>().GetAttack(damage);
                else if (co.GetComponent<BossMonsterCtrl>() != null) co.GetComponent<BossMonsterCtrl>().GetAttack(damage);
                else if (co.GetComponent<GoldMonsterCtrl>() != null) co.GetComponent<GoldMonsterCtrl>().GetAttack(damage);

                DamagePopUp.Create(new Vector3(co.transform.position.x,
                   co.transform.position.y + 2.0f, co.transform.position.z), damage, Color.red);
                Instantiate(exploseTs, co.transform.position, Quaternion.identity);
            }
        }
    }
}
