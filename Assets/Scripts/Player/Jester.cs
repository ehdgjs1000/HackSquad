using System.Collections;
using UnityEngine;

public class Jester : PlayerCtrl
{
    MonsterCtrl monster;
    float poisionTerm = 0.2f;
    int poisionLevel = 0;
    public void PoisionTermUpgrade()
    {
        poisionLevel++;
        poisionTerm *= 0.9f;
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
        GameObject bullet = Instantiate(bulletGo, new Vector3(monster.transform.position.x,
            monster.transform.position.y + 0.05f,
            monster.transform.position.z), Quaternion.identity);
        bullet.GetComponent<JesterBullet>().damage = damage;
        bullet.GetComponent<JesterBullet>().attackTerm = poisionTerm;

        if (nowBullet <= 0) StartCoroutine(Reloading());
        yield return null;
    }
}
