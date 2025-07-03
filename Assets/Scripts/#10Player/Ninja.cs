using System.Collections;
using UnityEngine;

public class Ninja : PlayerCtrl
{

    protected override void Attack(MonsterCtrl enemy)
    {
        fireRate = tempFireRate;
        _animator.SetBool("isAttacking", true);
        StartCoroutine(Shoot());
    }
    protected override IEnumerator Shoot()
    {
        //Instantiate(muzzleFlash, bulletSpawnPos.position, transform.localRotation);
        GameObject bullet = Instantiate(bulletGo, bulletSpawnPos.position, transform.localRotation);
        bullet.GetComponent<Bullet>().SetBulletInfo(damage, 10);

        if (nowBullet <= 0) StartCoroutine(Reloading());
        yield return null;
    }
}
