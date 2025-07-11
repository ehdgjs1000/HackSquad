using UnityEngine;

public class IceGround : MonoBehaviour
{
    float attackTerm = 0.5f;
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
        // todo : 주변 몬스터 찾아서 초당 데미지 주기
        monsterColls = null;
        monsterColls = Physics.OverlapSphere(transform.position, iceRadius, monsterLayer);
        Color color;
        if(monsterColls.Length > 0)
        {
            foreach(Collider col in monsterColls)
            {
                col.GetComponent<MonsterCtrl>().GetAttack(iceDamage);
                StartCoroutine(col.GetComponent<MonsterCtrl>().GetSlow(slowAmount));
                ColorUtility.TryParseHtmlString("#24E9F8", out color);
                DamagePopUp.Create(new Vector3(col.transform.position.x,
                    col.transform.position.y + 2.0f, col.transform.position.z), iceDamage, color);
            }
        }

    }
}
