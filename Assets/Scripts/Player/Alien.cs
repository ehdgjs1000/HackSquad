using System.Collections;
using UnityEngine;

public class Alien : PlayerCtrl
{
    private MonsterCtrl monster;
    int stunSkillLevel = 0;
    float stunTime = 0.1f;
    public void StunSkillUpgrade()
    {
        stunSkillLevel++;
        stunTime += 0.1f;
    }
    protected override void Attack(MonsterCtrl enemy)
    {
        fireRate = tempFireRate;

        monster = enemy;
        _animator.SetBool("isAttacking", true);
        StartCoroutine(Shoot());
    }

    protected override IEnumerator Shoot()
    {
        nowBullet--;
        float damage = bulletGo.GetComponent<AilenBullet>().damage;
        monster.GetAttack(damage);
        GameObject bullet = Instantiate(bulletGo, monster.transform.position, Quaternion.identity);
        bullet.GetComponent<AilenBullet>().SetBulletInfo(damage, stunTime);
        DamagePopUp.Create(new Vector3(monster.transform.position.x,
                    monster.transform.position.y + 2.0f, monster.transform.position.z), damage);

        if (nowBullet <= 0) StartCoroutine(Reloading());
        yield return null;
    }
}
