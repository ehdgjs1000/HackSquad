using System.Collections;
using UnityEngine;

public class Alien : PlayerCtrl
{
    Collider[] aMonsterColls;
    float radius = 20.0f;

    private GameObject monster;
    int stunSkillLevel = 0;
    float stunTime = 0.1f;
    bool isFinalSkill = false;
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
        fireRate = tempFireRate * 2f;
        aMonsterColls = null;
        aMonsterColls = Physics.OverlapSphere(transform.position, radius, monsterLayer);
        float finalDamage = damage * 0.5f;
        int maxHitCount = 5;
        foreach (Collider co in aMonsterColls)
        {
            maxHitCount--;
               GameObject monsterF = null;
            if(co.GetComponent<MonsterCtrl>() != null)
            {
                monsterF = co.gameObject;
                MonsterCtrl monsterC = co.GetComponent<MonsterCtrl>();
                monsterC.GetAttack(finalDamage);
            }else if (co. GetComponent<BossMonsterCtrl>() != null)
            {
                monsterF = co.gameObject;
                BossMonsterCtrl monsterB = co.GetComponent<BossMonsterCtrl>();
                monsterB.GetAttack(finalDamage);
            }
            GameObject bullet = PoolManager.instance.MakeObj("alienBullet");
            bullet.GetComponent<AilenBullet>().SetBulletInfo(finalDamage, stunTime);
            bullet.transform.position = monsterF.transform.position;
            bullet.transform.rotation = Quaternion.identity;
            StartCoroutine(PoolManager.instance.DeActive(1.0f, bullet));

            Color color;
            ColorUtility.TryParseHtmlString("#5B4A00", out color);
            DamagePopUp.Create(new Vector3(monsterF.transform.position.x,
                        monsterF.transform.position.y + 2.0f, monsterF.transform.position.z), finalDamage, color);
            if (maxHitCount <= 0) break;
        }
        yield return null;
    }
    protected override IEnumerator Shoot()
    {
        nowBullet--;
        if(monster.GetComponent<MonsterCtrl>() != null) monster.GetComponent<MonsterCtrl>().GetAttack(damage);
        else if (monster.GetComponent<BossMonsterCtrl>() != null) monster.GetComponent<BossMonsterCtrl>().GetAttack(damage);
        else if (monster.GetComponent<GoldMonsterCtrl>() != null) monster.GetComponent<GoldMonsterCtrl>().GetAttack(damage);
        SoundManager.instance.PlaySound(weaponFireClip);
        
        GameObject bullet = PoolManager.instance.MakeObj("alienBullet");
        bullet.GetComponent<AilenBullet>().SetBulletInfo(damage, stunTime);
        bullet.transform.position = monster.transform.position;
        bullet.transform.rotation = Quaternion.identity;
        StartCoroutine(PoolManager.instance.DeActive(1.0f, bullet));

        Color color;
        ColorUtility.TryParseHtmlString("#5B4A00", out color);
        DamagePopUp.Create(new Vector3(monster.transform.position.x,
                    monster.transform.position.y + 2.0f, monster.transform.position.z), damage, color);

        if (nowBullet <= 0) StartCoroutine(Reloading());
        yield return null;
    }
}
