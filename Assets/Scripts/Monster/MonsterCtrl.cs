using System.Collections;
using UnityEngine;

public class MonsterCtrl : MonoBehaviour
{
    //Stats
    [SerializeField] float attackDelay;
    [SerializeField] float damage;
    [SerializeField] float speed;
    [SerializeField] float hp;
    [SerializeField] float exp;
    bool canMove = true;
    Animator _animator;
    Collider monsterColl;

    //BattleInfo
    Collider[] playerColl = new Collider[4];
    [SerializeField] LayerMask playerLayer;
    Vector3 targetPos, dir;
    Quaternion lookTarget;
    bool isMoving = false;
    bool isSlow = false;
    public bool isDie = false;
    bool isAttacking = false;
    float slowAmount;
    [SerializeField] float attackRange;
    [SerializeField] float attackTerm;
    float tempAttackTerm;
     
 
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        monsterColl = GetComponent<Collider>();
        tempAttackTerm = attackTerm;
    }
    private void Update()
    {
        
        if(!isDie && !isAttacking)
        {
            if (canMove)
            {
                MoveToTarget();
            }
        }
        if(!isDie) AttackCheck();
        attackTerm -= Time.deltaTime;
    }
    public IEnumerator GetStun(float _stunTime)
    {
        canMove = false;
        yield return new WaitForSeconds(_stunTime);
        canMove = true;
    }
    public IEnumerator GetSlow(float _slowAmount)
    {
        isSlow = true;
        slowAmount = _slowAmount;
        yield return new WaitForSeconds(1.0f);
        isSlow = false;
    }
    private void MoveToTarget()
    {
        targetPos = new Vector3(0, 0, 0);
        dir = targetPos - transform.position;
        lookTarget = Quaternion.LookRotation(dir);

        if ((transform.position - targetPos).magnitude > attackRange - 0.5f)
        {
            if(!isSlow) transform.position += dir.normalized * Time.deltaTime * speed;
            else transform.position += dir.normalized * Time.deltaTime * speed * (1-slowAmount) ;

            transform.rotation = Quaternion.Lerp(transform.rotation, lookTarget, 0.25f);
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }
    public void GetAttack(float _dmg)
    {
        hp -= _dmg;
        if (hp <= 0.0f) StartCoroutine(Die());
    }
   IEnumerator Die()
    {
        isDie = true;
        monsterColl.enabled = false;
        GameManager.instance.GetExp(exp);
        //_animator.SetTrigger("Die");
        yield return new WaitForSeconds(4.0f);
        Destroy(this.gameObject);
    }
    private void AttackCheck()
    {
        //공격 구현
        playerColl = null;
        playerColl = Physics.OverlapSphere(transform.position, attackRange, playerLayer);
        if (playerColl.Length > 0)
        {
            GameObject playerGO = FindClosestTarget(playerColl).gameObject;
            this.transform.LookAt(playerGO.transform.position);
            if (attackTerm <= 0.0f) StartCoroutine(Attack());
        }
    }
    IEnumerator Attack()
    {
        isAttacking = true;
        attackTerm = tempAttackTerm;
        //_animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackDelay);
        GameManager.instance.GetDamage(damage);
    }
    Collider FindClosestTarget(Collider[] targets)
    {
        float closestDist = Mathf.Infinity;
        Collider target = null;

        foreach (var entity in targets)
        {
            //vector3.distance 보다 sqrMagnitude의 계산이 더 빠름
            float distance = (entity.transform.position - this.transform.position).sqrMagnitude;
            if (distance < closestDist)
            {
                closestDist = distance;
                target = entity;
            }
        }
        return target;
    }




}
