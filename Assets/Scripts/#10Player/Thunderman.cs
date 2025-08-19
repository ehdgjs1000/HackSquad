using System.Collections;
using UnityEngine;

public class Thunderman : PlayerCtrl
{
    Collider[] aMonsterColls;

    private GameObject monster;
    public bool isFinalSkill = false;
    float thunderSpeed = 5.0f;
    public int thunderCount = 1;
    private void Start()
    {
        if (BackEndGameData.Instance.UserEvolvingData.evolvingLevel[15] > 2) thunderCount++;
    }
    protected override void Attack(GameObject enemy)
    {
        if (!isFinalSkill)
        {
            monster = enemy;
            _animator.SetBool("isAttacking", true);
            StartCoroutine(Shoot());
        }
        else
        {
            StartCoroutine(UseFinalSkill());
        }
    }
    IEnumerator UseFinalSkill()
    {
        fireRate = 6.0f;
        for (int i = 0; i < 10; i++)
        {
            if (monster.GetComponent<MonsterCtrl>() != null) monster.GetComponent<MonsterCtrl>().GetAttack(damage);
            else if (monster.GetComponent<BossMonsterCtrl>() != null) monster.GetComponent<BossMonsterCtrl>().GetAttack(damage);
            SoundManager.instance.PlaySound(weaponFireClip);

            GameObject bullet = PoolManager.instance.MakeObj("thunderBullet");
            bullet.GetComponent<ObjectMove>().MoveSpeed = thunderSpeed;
            bullet.transform.position = bulletSpawnPos.transform.position;
            Quaternion rotation = Quaternion.LookRotation(transform.right);
            float ranDeg = Random.Range(-360,360);
            bullet.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, ranDeg);

            bullet.GetComponent<ThunderBullet>().SetBulletInfo(damage);
            StartCoroutine(PoolManager.instance.DeActive(6.0f, bullet));

            yield return new WaitForSeconds(0.2f);
        }
        fireRate = 3.0f;
        yield return null;
    }
    public void UpgradeFinalSkill()
    {
        isFinalSkill = true;
    }
    public void ThunderSpeedUp()
    {
        thunderSpeed += 1.0f;
    }
    protected override IEnumerator Shoot()
    {
        nowBullet--;
        fireRate = 10.0f;
        for (int i = 0; i <thunderCount; i++)
        {
            if (monster.GetComponent<MonsterCtrl>() != null) monster.GetComponent<MonsterCtrl>().GetAttack(damage);
            else if (monster.GetComponent<BossMonsterCtrl>() != null) monster.GetComponent<BossMonsterCtrl>().GetAttack(damage);
            SoundManager.instance.PlaySound(weaponFireClip);

            GameObject bullet = PoolManager.instance.MakeObj("thunderBullet");
            bullet.GetComponent<ObjectMove>().MoveSpeed = thunderSpeed;
            bullet.transform.position = bulletSpawnPos.transform.position;
            Quaternion rotation = Quaternion.LookRotation(transform.right);
            bullet.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 0);

            bullet.GetComponent<ThunderBullet>().SetBulletInfo(damage);
            StartCoroutine(PoolManager.instance.DeActive(6.0f, bullet));

            yield return new WaitForSeconds(0.3f);
        }

        fireRate = tempFireRate;
        if (nowBullet <= 0) StartCoroutine(Reloading());
        yield return null;
    }
}
