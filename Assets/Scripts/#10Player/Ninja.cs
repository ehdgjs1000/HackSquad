using System.Collections;
using UnityEngine;

public class Ninja : PlayerCtrl
{
    public int shootCount = 1;
    public bool isFinalSkill = false;
    private void Start()
    {
        if (BackEndGameData.Instance.UserEvolvingData.evolvingLevel[heroNum] > 2) shootCount++;
    }
    protected override void Attack(GameObject enemy)
    {
        _animator.SetBool("isAttacking", true);
        if(!isFinalSkill) StartCoroutine(Shoot());
        else StartCoroutine(FinalSkill(enemy));
    }
    IEnumerator FinalSkill(GameObject enemy)
    {
        attackRandom = true;
        SoundManager.instance.PlaySound(weaponFireClip);
        this.transform.LookAt(enemy.transform.position);
        GameObject bullet = Instantiate(bulletGo, bulletSpawnPos.position, transform.localRotation);
        bullet.GetComponent<Bullet>().SetBulletInfo(damage*0.7f, 5);
        fireRate = 0.33f;
        yield return null;
    }
    protected override IEnumerator Shoot()
    {
        fireRate = 10.0f;
        for (int i = 0; i < shootCount; i++)
        {
            GameObject bullet = PoolManager.instance.MakeObj("ninjaBullet");
            SoundManager.instance.PlaySound(weaponFireClip);
            bullet.transform.position = bulletSpawnPos.position;
            bullet.transform.rotation = transform.localRotation;
            bullet.GetComponent<Bullet>().SetBulletInfo(damage, 10);
            if(BackEndGameData.Instance.UserEvolvingData.evolvingLevel[heroNum] > 1)
            {
                GameObject bulletP = PoolManager.instance.MakeObj("ninjaBullet");
                SoundManager.instance.PlaySound(weaponFireClip);
                bulletP.transform.position = bulletSpawnPos.position;
                bulletP.transform.rotation = Quaternion.Inverse(transform.localRotation);
                bulletP.GetComponent<Bullet>().SetBulletInfo(damage*0.3f, 10);
            }

            yield return new WaitForSeconds(0.5f);
        }

        fireRate = tempFireRate;
        _animator.SetBool("isAttacking", false);
        if (nowBullet <= 0) StartCoroutine(Reloading());
        yield return null;
    }
}
