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
            GameObject bullet = Instantiate(samuraiFinalBulletGO, new Vector3(bulletSpawnPos.position.x, bulletSpawnPos.position.y + 0.5f, bulletSpawnPos.position.z), transform.localRotation);
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
        //진화 레벨 2적용
        if(BackEndGameData.Instance.UserEvolvingData.evolvingLevel[heroNum] > 2)
        {
            GameObject bullet = PoolManager.instance.MakeObj("samuraiBullet");
            bullet.transform.position = bulletSpawnPos.position;
            bullet.transform.rotation = Quaternion.Inverse(transform.localRotation);

            bullet.GetComponent<Bullet>().SetBulletInfo(damage * 0.3f, 10);
        }

        if (nowBullet <= 0) StartCoroutine(Reloading());

        yield return null;
    }
}
