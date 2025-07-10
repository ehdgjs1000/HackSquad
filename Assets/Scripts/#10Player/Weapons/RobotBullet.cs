using System.Collections;
using UnityEngine;

public class RobotBullet : MonoBehaviour
{
    [SerializeField] float attackDelay;
    public float damage;
    [SerializeField] float radius;
    [SerializeField] LayerMask monsterLayer;
    Collider[] monsterColls;

    private void Start()
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
                monsters.GetComponent<MonsterCtrl>().GetAttack(damage);
                DamagePopUp.Create(new Vector3(monsters.transform.position.x,
                   monsters.transform.position.y + 2.0f, monsters.transform.position.z), damage);
            }

        }
        StartCoroutine(PoolManager.instance.DeActive(0.8f, this.gameObject));
    }

}
