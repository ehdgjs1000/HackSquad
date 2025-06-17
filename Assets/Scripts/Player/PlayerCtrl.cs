using System.Collections;
using UnityEngine;

public abstract class PlayerCtrl : MonoBehaviour
{
    public static PlayerCtrl instance;
    
    protected Animator _animator;

    //About Fight Mode
    [SerializeField] float attackRange;
    [SerializeField] protected float fireRate;
    [SerializeField] protected Transform bulletSpawnPos;
    [SerializeField] protected GameObject bulletGo;
    [SerializeField] protected Transform muzzleFlash;
    [SerializeField] protected int maxBullet;
    [SerializeField] protected float reloadingTime;
    protected int nowBullet;
    protected float tempFireRate;
    protected bool isReloading = false;


    Vector3 targetPos;
    Vector3 dir;
    Quaternion lookTarget;
    Rigidbody rigid;
    public bool isMoving = false;
    
    //Monster
    [SerializeField]  LayerMask monsterLayer;
    private Collider[] monsterColls;


    private void Awake()
    {
        if (instance == null) instance = this;
        rigid = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        InitCharacterStats();
    }
    private void Update()
    {
        fireRate -= Time.deltaTime;
        CheckMonster();
    }
    private void InitCharacterStats()
    {
        nowBullet = maxBullet;
        tempFireRate = fireRate;
        fireRate = 0;
    }
    private void CheckMonster()
    {
        {
            monsterColls = null;
            monsterColls = Physics.OverlapSphere(this.transform.position, attackRange, monsterLayer);
            if (monsterColls.Length > 0)
            {
                try
                {
                    GameObject enemyGO = FindClosestTarget(monsterColls).gameObject;
                    this.transform.LookAt(enemyGO.transform.position);
                    MonsterCtrl enemy = enemyGO.GetComponent<MonsterCtrl>();
                    //공격
                    if (fireRate <= 0.0f && !isReloading) Attack(enemy);
                }

                catch (System.ObjectDisposedException e)
                {
                    System.Console.WriteLine("Caught: {0}", e.Message);
                }
            }
            else //근처에 몬스터가 없을경우
            {
                _animator.SetBool("isAttacking", false);
            }
        }
    }
    protected IEnumerator Reloading()
    {
        isReloading = true;
        _animator.SetBool("isAttacking", false);
        _animator.SetTrigger("Reloading");
        yield return new WaitForSeconds(reloadingTime);

        isReloading = false;
        nowBullet = maxBullet;
    }
    protected abstract void Attack(MonsterCtrl enemy);
    protected abstract IEnumerator Shoot();
    private Collider FindClosestTarget(Collider[] targets)
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
