using System.Collections;
using UnityEngine;

public abstract class PlayerCtrl : MonoBehaviour
{
    public static PlayerCtrl instance;
    protected Animator _animator;
    HeroInfo heroInfo;

    public string characterName;
    //About Fight Mode
    [SerializeField] protected Transform bulletSpawnPos;
    [SerializeField] protected GameObject bulletGo;
    [SerializeField] protected Transform muzzleFlash;
    [SerializeField] protected bool attackRandom;
    public SkillData[] characterSkills;
    public SkillData finalSkill;
    //init Data
    public float initAttackRange;
    public float initFireRate;
    public int initMaxBullet;
    public float initReloadingTime;
    public float initDamage;
    public float initHp;

    protected float attackRange;
    protected float fireRate;
    protected int maxBullet;
    protected float reloadingTime;
    protected float damage;
    protected int nowBullet;
    protected float tempFireRate;
    protected float hp;
    protected bool isReloading = false;

    public bool isMoving = false;
    protected int heroNum;
    
    //Monster
    [SerializeField] protected LayerMask monsterLayer;
    public Collider[] monsterColls;

    //Sound
    [SerializeField] protected AudioClip weaponFireClip;

    protected virtual void Awake()
    {
        if (instance == null) instance = this;
        _animator = GetComponent<Animator>();
        heroInfo = GetComponent<HeroInfo>();

        InitCharacterStats();
    }
    protected virtual void Update()
    {
        fireRate -= Time.deltaTime;
        CheckMonster();
    }
    public void UpgradeStats(int type, float amount)
    {
        if (type == 0) damage *= amount;
        else if (type == 1) tempFireRate *= amount;
        else if (type == 2) maxBullet += (int)amount;
        else if (type == 3) reloadingTime *= amount;
        else if (type == 4) attackRange *= amount;
    }
    private void InitCharacterStats()
    {
        heroNum = heroInfo.ReturnHeroNum();
        attackRange = initAttackRange;
        fireRate = initFireRate;
        maxBullet = initMaxBullet;
        reloadingTime = initReloadingTime;

        damage = initDamage * Mathf.Pow(1.3f, BackEndGameData.Instance.UserHeroData.heroLevel[heroNum]) *
            (1 + BackEndGameData.Instance.UserAbilityData.abilityLevel[0]*0.02f) *
            (1 + BackEndGameData.Instance.UserAbilityData.abilityLevel[3] * 0.05f) *
            (1 + BackEndGameData.Instance.UserAbilityData.abilityLevel[6] * 0.1f); 
        hp = initHp + 5* BackEndGameData.Instance.UserHeroData.heroLevel[heroNum];
         
        nowBullet = maxBullet;
        tempFireRate = fireRate;
        fireRate = 0;
    }
    public void AttackRandom()
    {
        attackRandom = true;
    }
    public float ReturnInitHp()
    {
        return initHp;
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
                    if (!attackRandom)
                    {
                        GameObject enemyGO = null;
                        if (monsterColls.Length > 1)
                        {
                            enemyGO = FindClosestTarget(monsterColls).gameObject;
                        }
                        else if(monsterColls.Length == 1)
                        {
                            enemyGO = monsterColls[0].gameObject;
                        }
                        
                        this.transform.LookAt(enemyGO.transform.position);
                        //MonsterCtrl enemy = enemyGO.GetComponent<MonsterCtrl>();
                        //공격
                        if (fireRate <= 0.0f && !isReloading) Attack(enemyGO);
                    }
                    else
                    {
                        int randomMonster = Random.Range(0,monsterColls.Length);
                        GameObject enemyGO = monsterColls[randomMonster].gameObject;
                        //MonsterCtrl enemy = enemyGO.GetComponent<MonsterCtrl>(); 
                        //공격
                        if (fireRate <= 0.0f && !isReloading) Attack(enemyGO);
                    }
                    
                }

                catch (System.ObjectDisposedException e)
                {
                    System.Console.WriteLine("Caught: {0}", e.Message);
                }
            }
            else //근처에 몬스터가 없을경우
            {
                //로비 Preview 캐릭터 warning대처
                if(GameManager.instance != null) _animator.SetBool("isAttacking", false);
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
    protected abstract void Attack(GameObject enemy);
    protected abstract IEnumerator Shoot();
    private Collider FindClosestTarget(Collider[] targets)
    {
        float closestDist = Mathf.Infinity;
        Collider target = null;

        foreach (var entity in targets)
        {
            //vector3.distance 보다 sqrMagnitude의 계산이 더 빠름
            float distance = (entity.transform.position - this.transform.position).sqrMagnitude;
            if (distance < closestDist && !entity.GetComponent<MonsterCtrl>().isDie)
            {
                closestDist = distance;
                target = entity;
            }
        }
        return target;
    }

}
