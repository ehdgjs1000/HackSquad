using System.Collections;
using UnityEngine;

public class Bazooka : PlayerCtrl
{
    public float exploseRadius;
    protected override void Attack(MonsterCtrl enemy)
    {
        fireRate = tempFireRate;

        _animator.SetBool("isAttacking", true);
        StartCoroutine(Shoot());
    }
    public void UpgradeExploseRadius(float _amount)
    {
        exploseRadius *= _amount;
    }
    protected override IEnumerator Shoot()
    {
        nowBullet--;
        Instantiate(muzzleFlash, bulletSpawnPos.position, transform.localRotation);
        GameObject bullet = Instantiate(bulletGo, bulletSpawnPos.position, transform.localRotation);
        bullet.GetComponent<BazookaBullet>().SetBulletInfo(damage,0);
        bullet.GetComponent<BazookaBullet>().exploseRadius = exploseRadius;

        if (nowBullet <= 0) StartCoroutine(Reloading());

        yield return null;
    }
}
