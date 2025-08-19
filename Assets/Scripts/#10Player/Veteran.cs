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
    
        //청두 evolving
        if(heroNum == 6)
        {
            if (BackEndGameData.Instance.UserEvolvingData.evolvingLevel[6] > 1) maxBullet += 5;
            if (BackEndGameData.Instance.UserEvolvingData.evolvingLevel[6] > 2) penetrateCount++;
        }else if (heroNum == 7)
        {
            if (BackEndGameData.Instance.UserEvolvingData.evolvingLevel[7] > 1) maxBullet += 10;
            if (BackEndGameData.Instance.UserEvolvingData.evolvingLevel[7] > 2) penetrateCount++;
        }
        else if (heroNum == 10)
        {
            if (BackEndGameData.Instance.UserEvolvingData.evolvingLevel[10] > 1) maxBullet += 30;
        }
    }
    public void FixMaxRandomDegree(float _amount)
    {
        maxRandomDegree += _amount;
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
        Instantiate(muzzleFlash, bulletSpawnPos.position, transform.localRotation);
        SoundManager.instance.PlaySound(weaponFireClip);

        float randomDegree = Random.Range(-maxRandomDegree, maxRandomDegree);
        Quaternion rotation = Quaternion.LookRotation(transform.right);
        GameObject bullet = PoolManager.instance.MakeObj("veteranBullet");
        bullet.transform.position = bulletSpawnPos.position;
        bullet.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, randomDegree);

        //람붜 진화적용
        if(BackEndGameData.Instance.UserEvolvingData.evolvingLevel[10] > 2 && heroNum == 10)
        {
            int randomPenNum = Random.Range(0,100);
            if(randomPenNum <=30) bullet.GetComponent<Bullet>().SetBulletInfo(damage, 1);
            else bullet.GetComponent<Bullet>().SetBulletInfo(damage, 0);
        }
        else bullet.GetComponent<Bullet>().SetBulletInfo(damage,penetrateCount);


        if (nowBullet <= 0) StartCoroutine(Reloading());
        yield return null;
    }
}
