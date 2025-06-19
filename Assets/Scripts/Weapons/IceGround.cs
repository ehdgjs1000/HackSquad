using UnityEngine;

public class IceGround : MonoBehaviour
{
    float attackTerm = 0.3f;
    float tempAttackTerm;
    [SerializeField] float iceRadius;
    [SerializeField] LayerMask monsterLayer;
    public float iceDamage;
    public float slowAmount;
    Collider[] monsterColls;

    private void Awake()
    {
        tempAttackTerm = attackTerm;
    }
    private void Update()
    {
        attackTerm -= Time.deltaTime;
        if (attackTerm <= 0.0) AttackMonster();
    }
    private void AttackMonster()
    {
        attackTerm = tempAttackTerm;
        // todo : �ֺ� ���� ã�Ƽ� �ʴ� ������ �ֱ�
        monsterColls = null;
        monsterColls = Physics.OverlapSphere(transform.position, iceRadius, monsterLayer);
        if(monsterColls.Length > 0)
        {
            foreach(Collider col in monsterColls)
            {
                col.GetComponent<MonsterCtrl>().GetAttack(iceDamage);
                StartCoroutine(col.GetComponent<MonsterCtrl>().GetSlow(slowAmount));
                DamagePopUp.Create(new Vector3(col.transform.position.x,
                    col.transform.position.y + 2.0f, col.transform.position.z), iceDamage);
            }
        }

    }
}
