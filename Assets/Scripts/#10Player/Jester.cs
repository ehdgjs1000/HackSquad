using System.Collections;
using UnityEngine;

public class Jester : PlayerCtrl
{
    GameObject monster;
    [SerializeField] GameObject finalBullet;
    bool isFinalSkill = false;
    float poisionTerm = 0.3f;
    int poisionLevel = 0;
    float jesterSkillSize = 1.0f;
    protected override void Awake()
    {
        base.Awake();
        if (BackEndGameData.Instance.UserEvolvingData.evolvingLevel[heroNum] > 1) jesterSkillSize *= 1.2f;
    }
    public void UpgradeFinalSkill()
    {
        isFinalSkill = true;    
    }
    public void PoisionTermUpgrade()
    {
        poisionLevel++;
        poisionTerm *= 0.9f;
    }
    public void UpgradeSkill1()
    {
        jesterSkillSize *= 1.15f;
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
            bullet.transform.localScale = new Vector3(jesterSkillSize, jesterSkillSize, jesterSkillSize);
            bullet.GetComponent<JesterBullet>().SetUp(damage, poisionTerm);
            StartCoroutine(PoolManager.instance.DeActive(6.0f, bullet));
        }
        else
        {
            fireRate = tempFireRate * 2f;
            bullet = Instantiate(finalBullet, new Vector3(0.0f,0.05f,0.0f), Quaternion.identity);
            bullet.GetComponent<JesterBullet>().SetUp(damage/3, poisionTerm/0.5f);
        }

        if (nowBullet <= 0) StartCoroutine(Reloading());
        yield return null;
    }
}
