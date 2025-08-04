using System.Collections;
using UnityEngine;

public class Jester : PlayerCtrl
{
    GameObject monster;
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
    protected override void Attack(GameObject enemy)
    {
        monster = enemy;
        _animator.SetBool("isAttacking", true);
        StartCoroutine(Shoot());
    }

    protected override IEnumerator Shoot()
    {
        GameObject bullet;
        nowBullet--;
        SoundManager.instance.PlaySound(weaponFireClip);
        if (!isFinalSkill)
        {
            fireRate = tempFireRate;
            bullet = PoolManager.instance.MakeObj("jesterBullet");
            bullet.transform.position = new Vector3(monster.transform.position.x,
            monster.transform.position.y + 0.3f,
            monster.transform.position.z);
            bullet.transform.rotation = Quaternion.identity;
            bullet.GetComponent<JesterBullet>().damage = damage;
            bullet.GetComponent<JesterBullet>().attackTerm = poisionTerm;
            StartCoroutine(PoolManager.instance.DeActive(6.0f, bullet));
        }
        else
        {
            fireRate = tempFireRate * 2f;
            bullet = Instantiate(finalBullet, new Vector3(0.0f,0.05f,0.0f), Quaternion.identity);
            bullet.GetComponent<JesterBullet>().damage = damage/3;
            bullet.GetComponent<JesterBullet>().attackTerm = poisionTerm /0.5f;
        }

        if (nowBullet <= 0) StartCoroutine(Reloading());
        yield return null;
    }
}
