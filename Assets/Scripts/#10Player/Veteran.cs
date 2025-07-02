using System.Collections;
using UnityEngine;

public class Veteran : PlayerCtrl
{
    [SerializeField] float maxRandomDegree;
    public int penetrateCount;
    protected override void Awake()
    {
        base.Awake();
        penetrateCount = 0;
        maxRandomDegree = 20;
    }
    public void FixMaxRandomDegree(float _amount)
    {
        maxRandomDegree += _amount;
    }
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

        float randomDegree = Random.Range(-maxRandomDegree, maxRandomDegree);
        Quaternion rotation = Quaternion.LookRotation(transform.right);
        GameObject bullet = Instantiate(bulletGo, bulletSpawnPos.position, transform.localRotation);
        bullet.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, randomDegree);
        bullet.GetComponent<Bullet>().SetBulletInfo(damage,penetrateCount);

        if (nowBullet <= 0) StartCoroutine(Reloading());
        yield return null;
    }
}
