using System.Collections;
using UnityEngine;

public class Terrorist : PlayerCtrl
{
    Collider[] aMonsterColls;
    float radius = 20.0f;

    private GameObject monster;
    int stunSkillLevel = 0;
    float stunTime = 0.1f;
    public bool isFinalSkill = false;
    public int shootCount = 1;
    public void UpgradeFinalSkill()
    {
        isFinalSkill = true;
    }
    public void StunSkillUpgrade()
    {
        stunSkillLevel++;
        stunTime += 0.1f;
    }
    protected override void Attack(GameObject enemy)
    {
        if (!isFinalSkill)
        {
            fireRate = tempFireRate;
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
        fireRate = tempFireRate / 3;
        float finalDamage = damage * 0.7f;

        float ranx = Random.Range(-10.0f,10.0f);
        float ranz = Random.Range(-10.0f, 10.0f);
        Vector3 ranPos = new Vector3(ranx,0,ranz);
        GameObject bullet = PoolManager.instance.MakeObj("terroristBullet");
        SoundManager.instance.PlaySound(weaponFireClip);
        bullet.transform.position = ranPos;
        bullet.transform.rotation = Quaternion.identity;
        bullet.GetComponent<TerroristBullet>().SetBulletInfo(finalDamage, stunTime);
        StartCoroutine(PoolManager.instance.DeActive(3.0f, bullet));

        yield return null;
    }
    protected override IEnumerator Shoot()
    {
        nowBullet--;
        for (int i = 0; i < shootCount; i++)
        {
            if (monster.GetComponent<MonsterCtrl>() != null) monster.GetComponent<MonsterCtrl>().GetAttack(damage);
            else if (monster.GetComponent<BossMonsterCtrl>() != null) monster.GetComponent<BossMonsterCtrl>().GetAttack(damage);
            SoundManager.instance.PlaySound(weaponFireClip);

            GameObject bullet = PoolManager.instance.MakeObj("terroristBullet");
            
            bullet.transform.position = monster.transform.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.GetComponent<TerroristBullet>().SetBulletInfo(damage, stunTime);
            StartCoroutine(PoolManager.instance.DeActive(1.0f, bullet));

            Color color;
            ColorUtility.TryParseHtmlString("#E7E7E7", out color);
            DamagePopUp.Create(new Vector3(monster.transform.position.x,
                        monster.transform.position.y + 2.0f, monster.transform.position.z), damage, color);

            yield return new WaitForSeconds(0.3f);
        }
        if (nowBullet <= 0) StartCoroutine(Reloading());
        yield return null;
    }
}
