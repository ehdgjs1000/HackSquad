using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Cowboy : PlayerCtrl
{
    [SerializeField] private float shotGunDegree;
    public int shotGunBulletSkillLevel = 0;
    public int penetrateCount;
    protected override void Awake()
    {
        base.Awake();
        penetrateCount = 0;
        shotGunBulletSkillLevel = 0;
    }
    protected override void Attack(MonsterCtrl enemy)
    {
        fireRate = tempFireRate;

        _animator.SetTrigger("isAttacking");
        StartCoroutine(Shoot());
    }
    protected override IEnumerator Shoot()
    {
        nowBullet--;
        Instantiate(muzzleFlash, bulletSpawnPos.position, transform.localRotation);

        Quaternion rotation = Quaternion.LookRotation(transform.right);

        GameObject bullet = PoolManager.instance.MakeObj("cowboyBullet");
        GameObject a = PoolManager.instance.MakeObj("cowboyBullet");
        GameObject b = PoolManager.instance.MakeObj("cowboyBullet");
        bullet.transform.position = bulletSpawnPos.transform.position;
        a.transform.position = bulletSpawnPos.transform.position;
        b.transform.position = bulletSpawnPos.transform.position;
        bullet.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 0.0f);
        a.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 20.0f);
        b.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, -20.0f);
        bullet.GetComponent<Bullet>().SetBulletInfo(damage, penetrateCount);
        a.GetComponent<Bullet>().SetBulletInfo(damage, penetrateCount);
        b.GetComponent<Bullet>().SetBulletInfo(damage, penetrateCount);
        if(shotGunBulletSkillLevel >= 1)
        {
            GameObject c = PoolManager.instance.MakeObj("cowboyBullet");
            GameObject d = PoolManager.instance.MakeObj("cowboyBullet");
            c.transform.position = bulletSpawnPos.transform.position;
            d.transform.position = bulletSpawnPos.transform.position;
            c.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 10.0f);
            d.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, -10.0f);
            c.GetComponent<Bullet>().SetBulletInfo(damage, penetrateCount);
            d.GetComponent<Bullet>().SetBulletInfo(damage, penetrateCount);
        }
        if (shotGunBulletSkillLevel >= 2)
        {
            GameObject e = PoolManager.instance.MakeObj("cowboyBullet");
            GameObject f = PoolManager.instance.MakeObj("cowboyBullet");
            e.transform.position = bulletSpawnPos.transform.position;
            f.transform.position = bulletSpawnPos.transform.position;
            e.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 30.0f);
            f.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, -30.0f);
            e.GetComponent<Bullet>().SetBulletInfo(damage, penetrateCount);
            f.GetComponent<Bullet>().SetBulletInfo(damage, penetrateCount);
        }
        if (shotGunBulletSkillLevel == 3)
        {
            GameObject g = PoolManager.instance.MakeObj("cowboyBullet");
            GameObject h = PoolManager.instance.MakeObj("cowboyBullet");
            g.transform.position=bulletSpawnPos.transform.position; 
            h.transform.position = bulletSpawnPos.transform.position;
            g.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 40.0f);
            h.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, -40.0f);
            g.GetComponent<Bullet>().SetBulletInfo(damage, penetrateCount);
            h.GetComponent<Bullet>().SetBulletInfo(damage, penetrateCount);
        }

        if (nowBullet <= 0) StartCoroutine(Reloading());
        
        yield return null;
    }
}
