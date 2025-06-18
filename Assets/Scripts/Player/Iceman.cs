using System.Collections;
using UnityEngine;

public class Iceman : PlayerCtrl
{
    int slowSkillLevel = 0;
    float slowAmount = 20;
    MonsterCtrl monster;
    public void UpgradeSlowSkill()
    {
        slowSkillLevel++;
        slowAmount += 10;
    }
    protected override void Attack(MonsterCtrl enemy)
    {
        fireRate = tempFireRate;
        monster = enemy;
        _animator.SetBool("isAttacking", true);
        StartCoroutine(Shoot());
    }

    protected override IEnumerator Shoot()
    {
        nowBullet--;
        Instantiate(muzzleFlash, bulletSpawnPos.position, transform.localRotation);
        GameObject bullet = Instantiate(bulletGo, monster.transform.position, Quaternion.identity);
        bullet.GetComponent<IceLight>().damage = damage;
        bullet.GetComponent<IceLight>().slowAmount = slowAmount;
        if (nowBullet <= 0) StartCoroutine(Reloading());

        yield return new WaitForSeconds(0.5f);
        _animator.SetBool("isAttacking", false);
    }
}
