using UnityEngine;

public class BazookaBullet : Bullet
{
    public float exploseRadius;
    [SerializeField] LayerMask monsterLayer;
    Collider[] monsterColls;

    private void Update()
    {
        bulletDestroyTime -= Time.deltaTime;
        if(bulletDestroyTime <= 0.2f)
        {
            Explose();
            Destroy(this.gameObject);
        }
    }
    private void Explose()
    {
        monsterColls = null;
        monsterColls = Physics.OverlapSphere(transform.position, exploseRadius, monsterLayer);
        if (monsterColls.Length > 0)
        {
            foreach (Collider monster in monsterColls)
            {
                monster.GetComponent<MonsterCtrl>().GetAttack(damage);
                DamagePopUp.Create(new Vector3(monster.transform.position.x,
                    monster.transform.position.y + 2.0f, monster.transform.position.z), damage);
            }

        }
    }

    protected override void OnHit()
    {
        Explose();
    }
}
