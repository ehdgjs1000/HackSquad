using System.Collections;
using UnityEngine;

public class Alien : PlayerCtrl
{
    Collider[] aMonsterColls;
    float radius = 20.0f;

    private MonsterCtrl monster;
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
    protected override void Attack(MonsterCtrl enemy)
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
        fireRate = tempFireRate;
        aMonsterColls = null;
        aMonsterColls = Physics.OverlapSphere(transform.position, radius, monsterLayer);
        foreach (Collider co in aMonsterColls)
        {
            MonsterCtrl monsterF = co.GetComponent<MonsterCtrl>();
            monsterF.GetAttack(damage);

            GameObject bullet = PoolManager.instance.MakeObj("alienBullet");
            bullet.GetComponent<AilenBullet>().SetBulletInfo(damage, stunTime);
            bullet.transform.position = monsterF.transform.position;
            bullet.transform.rotation = Quaternion.identity;
            StartCoroutine(PoolManager.instance.DeActive(1.0f, bullet));

            Color color;
            ColorUtility.TryParseHtmlString("#5B4A00", out color);
            DamagePopUp.Create(new Vector3(monsterF.transform.position.x,
                        monsterF.transform.position.y + 2.0f, monsterF.transform.position.z), damage, color);
        }
        yield return null;
    }
    protected override IEnumerator Shoot()
    {
        nowBullet--;
        monster.GetAttack(damage);
        
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
