using System.Collections;
using UnityEngine;

public class BossMonsterCtrl : MonoBehaviour
{
    [SerializeField] protected float attackDelay;
    [SerializeField] protected float damage;
    [SerializeField] float speed;
    [SerializeField] float hp;
    [SerializeField] float exp;
    [SerializeField] protected int gold;
    bool canMove = true;
    protected Animator _animator;
    Collider monsterColl;
    float initHp;
    SpawnEffect spawnEffect;

    //BattleInfo
    Collider[] playerColl = new Collider[4];
    [SerializeField] LayerMask playerLayer;
    Vector3 targetPos, dir;
    Quaternion lookTarget;
    bool isSlow = false;
    public bool isDie = false;
    protected bool isAttacking = false;
    float slowAmount;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackTerm;
    protected float tempAttackTerm;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        monsterColl = GetComponent<Collider>();
        spawnEffect = GetComponent<SpawnEffect>();
        tempAttackTerm = attackTerm;
        initHp = hp;
    }
    private void Update()
    {
        if (!isDie && !isAttacking)
        {
            if (canMove)
            {
                MoveToTarget();
            }
        }
        if (!isDie) AttackCheck();
        attackTerm -= Time.deltaTime;
    }
    public void InitMonster()
    {
        isDie = false;
        canMove = true;
        isSlow = false;
        isAttacking = false;
        monsterColl.enabled = true;

        hp = initHp * Mathf.Pow(1.1f, GameManager.instance.min);
    }
    public void DoFade()
    {
        spawnEffect.StartFade();
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
            if (!isSlow) transform.position += dir.normalized * Time.deltaTime * speed;
            else transform.position += dir.normalized * Time.deltaTime * speed * (1 - slowAmount);

            transform.rotation = Quaternion.Lerp(transform.rotation, lookTarget, 0.25f);
            _animator.SetBool("isMoving", true);
        }
        else _animator.SetBool("isMoving", false);
    }
    public void GetAttack(float _dmg)
    {
        hp -= _dmg;
        if (hp <= 0.0f && !isDie)
        {
            StopAllCoroutines();
            StartCoroutine(Die());
        }

    }
    protected virtual IEnumerator Die()
    {
        isDie = true;
        
        _animator.SetTrigger("Die");
        monsterColl.enabled = false;
        GameManager.instance.IsBossDie();
        GameManager.instance.getGold += gold;
        GameManager.instance.BossLevelUp();
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
    protected virtual IEnumerator Attack()
    {
        isAttacking = true;
        attackTerm = tempAttackTerm;
        _animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackDelay);
        GameManager.instance.GetBossDamage(damage);
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
