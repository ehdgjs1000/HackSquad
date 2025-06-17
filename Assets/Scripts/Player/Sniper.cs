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
        Instantiate(bulletGo, bulletSpawnPos.position, transform.localRotation);

        if (nowBullet <= 0) StartCoroutine(Reloading());

        yield return null;
    }
}
