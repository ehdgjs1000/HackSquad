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
    MonsterCtrl monster;

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
    }
    IEnumerator UseFinalSkill()
    {
        finalSkillTime = tempFinalSkillTime;
        yield return new WaitForSeconds(0.1f);
        Instantiate(finalSkillTs, transform.position, Quaternion.identity);
        finalSkillTs.GetComponent<IceGround>().iceDamage = damage * 0.7f;
        finalSkillTs.GetComponent<IceGround>().slowAmount = 1;
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
    protected override void Attack(MonsterCtrl enemy)
    {
        if (enemy != null)
        {
            fireRate = tempFireRate;
            monster = enemy;
            _animator.SetBool("isAttacking", true);
            StartCoroutine(Shoot());
        }
    }

    protected override IEnumerator Shoot()
    {
        nowBullet--;
        SoundManager.instance.PlaySound(weaponFireClip);
        Instantiate(muzzleFlash, bulletSpawnPos.position, transform.localRotation);
        GameObject bullet = Instantiate(bulletGo, monster.transform.position, Quaternion.identity);
        bullet.GetComponent<IceLight>().damage = damage;
        bullet.GetComponent<IceLight>().slowAmount = slowAmount;
        if (nowBullet <= 0) StartCoroutine(Reloading());

        yield return new WaitForSeconds(0.5f);
        _animator.SetBool("isAttacking", false);
    }
}
