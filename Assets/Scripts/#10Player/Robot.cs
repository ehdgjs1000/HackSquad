using System.Collections;
using UnityEngine;

public class Robot : PlayerCtrl
{
    GameObject monster;
    public int attackCount;
    public bool isFinalSkill = false;
    private void Start()
    {
        if (BackEndGameData.Instance.UserEvolvingData.evolvingLevel[11] > 2) attackCount++;
    }
    protected override void Attack(GameObject enemy)
    {
        if (enemy != null)
        {
            if (!isFinalSkill)
            {
                fireRate = tempFireRate;
                monster = enemy;
                _animator.SetBool("isAttacking", true);
                for (int i = 0; i < attackCount; i++)
                {
                    StartCoroutine(Shoot());
                }
            }
            else
            {
                fireRate = 0.8f;
                monster = enemy;
                _animator.SetBool("isAttacking", true);
                StartCoroutine(Shoot());
            }
        }
    }

    protected override IEnumerator Shoot()
    {
        
        Vector3 monsterPos = monster.transform.position;
        GameObject bullet = PoolManager.instance.MakeObj("robotBullet");
        bullet.transform.position = new Vector3(monsterPos.x, monsterPos.y + 0.25f, monsterPos.z);
        bullet.transform.rotation = Quaternion.identity;
        bullet.GetComponent<RobotBullet>().damage = damage;
        bullet.GetComponent<RobotBullet>().StartAttack();
        yield return new WaitForSeconds(0.4f);
        SoundManager.instance.PlaySound(weaponFireClip);

    }
}
