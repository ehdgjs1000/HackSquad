using System.Collections;
using UnityEngine;

public class Samurai : PlayerCtrl
{
    [SerializeField] GameObject samuraiFinalBulletGO;
    public bool isFinalSkillUpgrade;
    protected override void Attack(MonsterCtrl enemy)
    {
        fireRate = tempFireRate;

        _animator.SetBool("isAttacking", true);
        StartCoroutine(Shoot());
    }

    protected override IEnumerator Shoot()
    {
        Instantiate(muzzleFlash, bulletSpawnPos.position, transform.localRotation);
        if (isFinalSkillUpgrade)
        {
            GameObject bullet = Instantiate(samuraiFinalBulletGO, bulletSpawnPos.position, transform.localRotation);
            bullet.GetComponent<Bullet>().SetBulletInfo(damage, 10);
            bullet.GetComponent<SamuraiBullet>().isFinalBullet = true;
        }
        else
        {
            nowBullet--;
            GameObject bullet = Instantiate(bulletGo, bulletSpawnPos.position, transform.localRotation);
            bullet.GetComponent<Bullet>().SetBulletInfo(damage, 10);
        }

        if (nowBullet <= 0) StartCoroutine(Reloading());

        yield return null;
    }
}
