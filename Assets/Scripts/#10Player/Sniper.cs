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
        GameObject bullet = PoolManager.instance.MakeObj("sniperBullet");
        bullet.transform.position = bulletSpawnPos.position;
        bullet.transform.rotation = transform.localRotation;
        bullet.GetComponent<Bullet>().SetBulletInfo(damage, 10);
        if (nowBullet <= 0) StartCoroutine(Reloading());

        yield return null;
    }
}
