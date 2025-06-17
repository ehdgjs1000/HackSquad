using System.Collections;
using UnityEngine;

public class Cowboy : PlayerCtrl
{
    [SerializeField] private float shotGunDegree;
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
        Instantiate(bulletGo, bulletSpawnPos.position, transform.localRotation);
        GameObject a = Instantiate(bulletGo, bulletSpawnPos.position, transform.localRotation);
        GameObject b = Instantiate(bulletGo, bulletSpawnPos.position, transform.localRotation);
        a.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 20.0f);
        b.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, -20.0f);

        if (nowBullet <= 0) StartCoroutine(Reloading());
        
        yield return null;
    }
}
