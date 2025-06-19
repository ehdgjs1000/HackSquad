using System.Collections;
using UnityEngine;

public class Jester : PlayerCtrl
{
    MonsterCtrl monster;
    [SerializeField] GameObject finalBullet;
    bool isFinalSkill = false;
    float poisionTerm = 0.3f;
    int poisionLevel = 0;
    public void UpgradeFinalSkill()
    {
        isFinalSkill = true;    
    }
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
        GameObject bullet;
        nowBullet--;
        if (!isFinalSkill)
        {
            bullet = Instantiate(bulletGo, new Vector3(monster.transform.position.x,
            monster.transform.position.y + 0.05f,
            monster.transform.position.z), Quaternion.identity);
            bullet.GetComponent<JesterBullet>().damage = damage;
            bullet.GetComponent<JesterBullet>().attackTerm = poisionTerm;
        }
        else
        {
            bullet = Instantiate(finalBullet, new Vector3(monster.transform.position.x,
            monster.transform.position.y + 0.05f,
            monster.transform.position.z), Quaternion.identity);
            bullet.GetComponent<JesterBullet>().damage = damage;
            bullet.GetComponent<JesterBullet>().attackTerm = poisionTerm /0.5f;
        }
        
        

        if (nowBullet <= 0) StartCoroutine(Reloading());
        yield return null;
    }
}
