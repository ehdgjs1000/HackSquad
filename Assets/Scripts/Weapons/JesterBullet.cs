using System.Collections;
using UnityEngine;

public class JesterBullet : MonoBehaviour
{
    [SerializeField] float attackRadius;
    [SerializeField] LayerMask monsterLayer;
    public float attackTerm;
    public float damage;
    Collider[] monsterColls;
    public float tempAttackTerm;

    private void Awake()
    {
        tempAttackTerm = attackTerm;
    }
    private void Update()
    {
        attackTerm -= Time.deltaTime;
        if (attackTerm <= 0.0f) StartCoroutine(JesterAttack());
    }
    IEnumerator JesterAttack()
    {
        attackTerm = tempAttackTerm;
        monsterColls = null;
        monsterColls = Physics.OverlapSphere(transform.position, attackRadius, monsterLayer);
        if(monsterColls.Length > 0)
        {
            foreach(Collider co in monsterColls)
            {
                co.GetComponent<MonsterCtrl>().GetAttack(damage);
                DamagePopUp.Create(new Vector3(co.transform.position.x,
                    co.transform.position.y + 2.0f, co.transform.position.z), damage);
            }
        }
        yield return null;

    }
}
