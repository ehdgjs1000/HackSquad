using System.Collections;
using UnityEngine;

public class ThunderBullet : MonoBehaviour
{
    public float damage;
    float thunderattackTerm = 0.2f;

    [SerializeField] float attackRadius;
    [SerializeField] LayerMask monsterLayer;
    public Collider[] monsterColls;
    private void Update()
    {
        thunderattackTerm -= Time.deltaTime;
        if (thunderattackTerm <= 0.0f) StartCoroutine(Attack());
        
    }
    public void SetBulletInfo(float _dmg)
    {
        damage = _dmg;
        StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        thunderattackTerm = 0.2f;
        float attackDamage = damage / 5;
        monsterColls = null;
        monsterColls = Physics.OverlapSphere(transform.position, attackRadius, monsterLayer);
        if (monsterColls.Length > 0)
        {
            foreach (Collider co in monsterColls)
            {
                if (co.GetComponent<MonsterCtrl>() != null) co.GetComponent<MonsterCtrl>().GetAttack(attackDamage);
                else if (co.GetComponent<BossMonsterCtrl>() != null) co.GetComponent<BossMonsterCtrl>().GetAttack(attackDamage);
                else if (co.GetComponent<GoldMonsterCtrl>() != null) co.GetComponent<GoldMonsterCtrl>().GetAttack(attackDamage);

                DamagePopUp.Create(new Vector3(co.transform.position.x,
                    co.transform.position.y + 2.5f, co.transform.position.z), attackDamage, Color.blue);
            }
        }
        yield return null;
    }

}
