using System.Collections;
using UnityEngine;

public class Ninja : PlayerCtrl
{
    public int shootCount = 1;
    public bool isFinalSkill = false;

    protected override void Attack(MonsterCtrl enemy)
    {
        _animator.SetBool("isAttacking", true);
        if(!isFinalSkill) StartCoroutine(Shoot());
        else StartCoroutine(FinalSkill());
    }
    IEnumerator FinalSkill()
    {
        attackRandom = true;
        SoundManager.instance.PlaySound(weaponFireClip);
        GameObject bullet = Instantiate(bulletGo, bulletSpawnPos.position, transform.localRotation);
        bullet.GetComponent<Bullet>().SetBulletInfo(damage*0.7f, 10);
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
            yield return new WaitForSeconds(0.5f);
        }

        fireRate = tempFireRate;
        _animator.SetBool("isAttacking", false);
        if (nowBullet <= 0) StartCoroutine(Reloading());
        yield return null;
    }
}
