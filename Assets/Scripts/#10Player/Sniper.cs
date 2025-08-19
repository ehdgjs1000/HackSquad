using System.Collections;
using UnityEngine;

public class Sniper : PlayerCtrl
{
    protected override void Awake()
    {
        if (BackEndGameData.Instance.UserEvolvingData.evolvingLevel[heroNum] > 1) attackRange = 50;
        if (BackEndGameData.Instance.UserEvolvingData.evolvingLevel[heroNum] > 2) tempFireRate = 0.7f;
    }
    protected override void Attack(GameObject enemy)
    {
        fireRate = tempFireRate;

        _animator.SetBool("isAttacking", true);
        StartCoroutine(Shoot());
    }

    protected override IEnumerator Shoot()
    {
        nowBullet--;
        SoundManager.instance.PlaySound(weaponFireClip);
        Instantiate(muzzleFlash, bulletSpawnPos.position, transform.localRotation);
        GameObject bullet = PoolManager.instance.MakeObj("sniperBullet");
        bullet.transform.position = bulletSpawnPos.position;
        bullet.transform.rotation = transform.localRotation;
        bullet.GetComponent<Bullet>().SetBulletInfo(damage, 10);
        if (nowBullet <= 0) StartCoroutine(Reloading());

        yield return null;
    }
}
