using System.Collections;
using UnityEngine;

public class TerroristBullet : MonoBehaviour
{
    public float damage;
    public float stunTime;

    [SerializeField] float attackRadius;
    [SerializeField] LayerMask monsterLayer;
    public Collider[] monsterColls;
    public void SetBulletInfo(float _dmg, float _stunTime)
    {
        damage = _dmg;
        stunTime = _stunTime;
        StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        monsterColls = null;
        monsterColls = Physics.OverlapSphere(transform.position, attackRadius, monsterLayer);
        if (monsterColls.Length > 0)
        {
            foreach (Collider co in monsterColls)
            {
                if (co.GetComponent<MonsterCtrl>() != null) co.GetComponent<MonsterCtrl>().GetAttack(damage);
                else if (co.GetComponent<BossMonsterCtrl>() != null) co.GetComponent<BossMonsterCtrl>().GetAttack(damage);
                DamagePopUp.Create(new Vector3(co.transform.position.x,
                    co.transform.position.y + 2.5f, co.transform.position.z), damage, Color.white);
            }
        }
        yield return null;
    }
}
