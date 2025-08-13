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
            StartCoroutine(PoolManager.instance.DeActive(0.0f, this.gameObject));
            // Destroy(this.gameObject);
        }
    }
    private void Explose()
    {
        monsterColls = null;
        monsterColls = Physics.OverlapSphere(transform.position, exploseRadius, monsterLayer);
        Color color;
        if (monsterColls.Length > 0)
        {
            foreach (Collider monster in monsterColls)
            {
                if(monster.GetComponent<MonsterCtrl>() != null) monster.GetComponent<MonsterCtrl>().GetAttack(damage);
                else if(monster.GetComponent<BossMonsterCtrl>() !=null) monster.GetComponent<BossMonsterCtrl>().GetAttack(damage);
                else if (monster.GetComponent<GoldMonsterCtrl>() != null) monster.GetComponent<GoldMonsterCtrl>().GetAttack(damage);

                ColorUtility.TryParseHtmlString("#E800FF", out color);
                DamagePopUp.Create(new Vector3(monster.transform.position.x,
                    monster.transform.position.y + 2.0f, monster.transform.position.z), damage, color);
            }

        }
    }

    protected override void OnHit()
    {
        Explose();
    }
}
