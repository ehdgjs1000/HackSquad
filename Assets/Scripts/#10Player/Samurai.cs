using System.Collections;
using UnityEngine;

public class Samurai : PlayerCtrl
{
    [SerializeField] GameObject samuraiFinalBulletGO;
    public bool isFinalSkillUpgrade;
    protected override void Attack(GameObject enemy)
    {
        fireRate = tempFireRate;

        _animator.SetBool("isAttacking", true);
        StartCoroutine(Shoot());
    }

    protected override IEnumerator Shoot()
    {
        Instantiate(muzzleFlash, bulletSpawnPos.position, transform.localRotation);
        SoundManager.instance.PlaySound(weaponFireClip);
        if (isFinalSkillUpgrade)
        {
            GameObject bullet = Instantiate(samuraiFinalBulletGO, bulletSpawnPos.position, transform.localRotation);
            bullet.GetComponent<Bullet>().SetBulletInfo(damage*1.2f, 10);
            bullet.GetComponent<SamuraiBullet>().isFinalBullet = true;
        }
        else
        {
            nowBullet--;
            GameObject bullet = PoolManager.instance.MakeObj("samuraiBullet");
            bullet.transform.position = bulletSpawnPos.position;
            bullet.transform.rotation = transform.localRotation;

            bullet.GetComponent<Bullet>().SetBulletInfo(damage, 10);
        }

        if (nowBullet <= 0) StartCoroutine(Reloading());

        yield return null;
    }
}
