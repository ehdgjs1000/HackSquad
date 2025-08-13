using System.Collections;
using UnityEngine;

public class RobotBullet : MonoBehaviour
{
    [SerializeField] float attackDelay;
    public float damage;
    [SerializeField] float radius;
    [SerializeField] LayerMask monsterLayer;
    Collider[] monsterColls;

    public void StartAttack()
    {
        StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        monsterColls = null;
        yield return new WaitForSeconds(attackDelay);
        monsterColls = Physics.OverlapSphere(this.transform.position, radius, monsterLayer);
        if(monsterColls.Length >=0)
        {
            foreach (Collider monsters in monsterColls)
            {
                if(monsters.GetComponent<MonsterCtrl>() != null) monsters.GetComponent<MonsterCtrl>().GetAttack(damage);
                else if (monsters.GetComponent<BossMonsterCtrl>() != null) monsters.GetComponent<BossMonsterCtrl>().GetAttack(damage);
                else if (monsters.GetComponent<GoldMonsterCtrl>() != null) monsters.GetComponent<GoldMonsterCtrl>().GetAttack(damage);

                DamagePopUp.Create(new Vector3(monsters.transform.position.x,
                   monsters.transform.position.y + 2.0f, monsters.transform.position.z), damage, Color.blue);
            }

        }
        StartCoroutine(PoolManager.instance.DeActive(2.1f, this.gameObject));
    }

}
