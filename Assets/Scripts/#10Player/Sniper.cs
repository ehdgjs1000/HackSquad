using System.Collections;
using UnityEngine;

public class Sniper : PlayerCtrl
{
    protected override void Attack(MonsterCtrl enemy)
    {
        fireRate = tempFireRate;

        _animator.SetBool("isAttacking", true);
        StartCoroutine(Shoot());
    }

    protected override IEnumerator Shoot()
    {
        nowBullet--;

        Instantiate(muzzleFlash, bulletSpawnPos.position, transform.localRotation);
        GameObject bullet = Instantiate(bulletGo, bulletSpawnPos.position, transform.localRotation);
        bullet.GetComponent<SniperBullet>().SetBulletInfo(damage, 10);
        if (nowBullet <= 0) StartCoroutine(Reloading());

        yield return null;
    }
}
