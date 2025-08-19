using System.Collections;
using UnityEngine;

public class Iceman : PlayerCtrl
{
    [SerializeField] Transform finalSkillTs;
    [SerializeField] float finalSkillTime;
    float tempFinalSkillTime;
    
    bool isFinalSkill = false;
    int slowSkillLevel = 0;
    float slowAmount = 0.2f;
    GameObject monster;
    int attackCount = 1;

    protected override void Update()
    {
        base.Update();
        finalSkillTime -= Time.deltaTime;
        if(isFinalSkill && finalSkillTime <= 0.0f)
        {
            StartCoroutine(UseFinalSkill());
        }
    }
    protected override void Awake()
    {
        base.Awake();
        tempFinalSkillTime = finalSkillTime;
        if (BackEndGameData.Instance.UserEvolvingData.evolvingLevel[heroNum] > 1) attackCount++;
    }
    public void AttackCountUpgrade() => attackCount++;
    IEnumerator UseFinalSkill()
    {
        finalSkillTime = tempFinalSkillTime;
        yield return new WaitForSeconds(0.1f);
        Instantiate(finalSkillTs, new Vector3(0,0.1f,0), Quaternion.identity);
        finalSkillTs.GetComponent<IceGround>().iceDamage = damage * 0.5f;
        if(BackEndGameData.Instance.UserEvolvingData.evolvingLevel[heroNum] > 2) finalSkillTs.GetComponent<IceGround>().slowAmount = 0.6f;
        else finalSkillTs.GetComponent<IceGround>().slowAmount = 0.3f;

    }
    public void UpgradeFinalSkill()
    {
        isFinalSkill = true;
    }
    public void UpgradeSlowSkill()
    {
        slowSkillLevel++;
        slowAmount += 0.1f;
    }
    protected override void Attack(GameObject enemy)
    {
        if (enemy != null)
        {
            nowBullet--;
            fireRate = tempFireRate*3;

            //AttakcCount만큰 공격하기
            for(int i = 0; i<attackCount; i++)
            {
                monsterColls = null;
                monsterColls = Physics.OverlapSphere(this.transform.position, attackRange, monsterLayer);
                if (monsterColls.Length > 0)
                {
                    int randomMonster = Random.Range(0, monsterColls.Length);
                    monster = monsterColls[randomMonster].gameObject;
                    _animator.SetBool("isAttacking", true);
                    StartCoroutine(Shoot(monster));
                }
                else
                {
                    if (GameManager.instance != null) _animator.SetBool("isAttacking", false);
                }
            }
            
            //monster = enemy;
            //_animator.SetBool("isAttacking", true);
            //StartCoroutine(Shoot());
        }
    }
    protected override IEnumerator Shoot()
    {
        yield return null;
    }
    IEnumerator Shoot(GameObject target)
    {
        float iceDamage = 0;
        if (attackCount == 1) iceDamage = damage;
        else iceDamage = damage * 0.7f;
        if (weaponFireClip != null) SoundManager.instance.PlaySound(weaponFireClip);
        Instantiate(muzzleFlash, bulletSpawnPos.position, transform.localRotation);
        GameObject bullet = Instantiate(bulletGo, target.transform.position, Quaternion.identity);
        bullet.GetComponent<IceLight>().damage = iceDamage;
        bullet.GetComponent<IceLight>().slowAmount = slowAmount;

        fireRate = tempFireRate;
        if (nowBullet <= 0) StartCoroutine(Reloading());

        yield return new WaitForSeconds(0.5f);
        _animator.SetBool("isAttacking", false);
    }
}
