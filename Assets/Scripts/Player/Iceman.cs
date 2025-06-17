using System.Collections;
using UnityEngine;

public class Iceman : PlayerCtrl
{
    MonsterCtrl monster;
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
        Instantiate(bulletGo, monster.transform.position, Quaternion.identity);
        if (nowBullet <= 0) StartCoroutine(Reloading());

        yield return new WaitForSeconds(0.5f);
        _animator.SetBool("isAttacking", false);
    }
}
