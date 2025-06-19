using System.Collections;
using UnityEngine;

public class Cowboy : PlayerCtrl
{
    [SerializeField] private float shotGunDegree;
    public int shotGunBulletSkillLevel = 0;
    public int penetrateCount;
    protected override void Awake()
    {
        base.Awake();
        penetrateCount = 0;
        shotGunBulletSkillLevel = 1;
    }
    protected override void Attack(MonsterCtrl enemy)
    {
        fireRate = tempFireRate;

        _animator.SetBool("isAttacking",true);
        StartCoroutine(Shoot());
    }
    protected override IEnumerator Shoot()
    {
        nowBullet--;
        Instantiate(muzzleFlash, bulletSpawnPos.position, transform.localRotation);

        Quaternion rotation = Quaternion.LookRotation(transform.right);
        GameObject bullet = Instantiate(bulletGo, bulletSpawnPos.position, transform.localRotation);
        GameObject a = Instantiate(bulletGo, bulletSpawnPos.position, transform.localRotation);
        GameObject b = Instantiate(bulletGo, bulletSpawnPos.position, transform.localRotation);
        a.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 20.0f);
        b.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, -20.0f);
        bullet.GetComponent<Bullet>().SetBulletInfo(damage, penetrateCount);
        a.GetComponent<Bullet>().SetBulletInfo(damage, penetrateCount);
        b.GetComponent<Bullet>().SetBulletInfo(damage, penetrateCount);
        if(shotGunBulletSkillLevel >= 1)
        {
            GameObject c = Instantiate(bulletGo, bulletSpawnPos.position, transform.localRotation);
            GameObject d = Instantiate(bulletGo, bulletSpawnPos.position, transform.localRotation);
            c.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 10.0f);
            d.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, -10.0f);
            c.GetComponent<Bullet>().SetBulletInfo(damage, penetrateCount);
            d.GetComponent<Bullet>().SetBulletInfo(damage, penetrateCount);
        }
        if (shotGunBulletSkillLevel >= 2)
        {
            GameObject e = Instantiate(bulletGo, bulletSpawnPos.position, transform.localRotation);
            GameObject f = Instantiate(bulletGo, bulletSpawnPos.position, transform.localRotation);
            e.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 30.0f);
            f.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, -30.0f);
            e.GetComponent<Bullet>().SetBulletInfo(damage, penetrateCount);
            f.GetComponent<Bullet>().SetBulletInfo(damage, penetrateCount);
        }
        if (shotGunBulletSkillLevel == 3)
        {
            GameObject g = Instantiate(bulletGo, bulletSpawnPos.position, transform.localRotation);
            GameObject h = Instantiate(bulletGo, bulletSpawnPos.position, transform.localRotation);
            g.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 40.0f);
            h.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, -40.0f);
            g.GetComponent<Bullet>().SetBulletInfo(damage, penetrateCount);
            h.GetComponent<Bullet>().SetBulletInfo(damage, penetrateCount);
        }

        if (nowBullet <= 0) StartCoroutine(Reloading());
        
        yield return null;
    }
}
