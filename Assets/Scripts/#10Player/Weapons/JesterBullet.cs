using System.Collections;
using UnityEngine;

public class JesterBullet : MonoBehaviour
{
    [SerializeField] float attackRadius;
    [SerializeField] LayerMask monsterLayer;
    public float attackTerm;
    float damage;
    Collider[] monsterColls;
    float tempAttackTerm;
    public float bulletSize = 1.0f;


    private void Awake()
    {
        tempAttackTerm = attackTerm;
    }
    private void Update()
    {
        attackTerm -= Time.deltaTime;
        if (attackTerm <= 0.0f) StartCoroutine(JesterAttack());
    }
    public void SetUp(float _damage, float _attackTerm)
    {
        damage = _damage;
        tempAttackTerm = _attackTerm;
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
                if(co.GetComponent<MonsterCtrl>() != null) co.GetComponent<MonsterCtrl>().GetAttack(damage);
                else if(co.GetComponent<BossMonsterCtrl>() != null) co.GetComponent<BossMonsterCtrl>().GetAttack(damage);
                DamagePopUp.Create(new Vector3(co.transform.position.x,
                    co.transform.position.y + 2.5f, co.transform.position.z), damage, Color.green);
            }
        }
        yield return null;

    }
}
