using System.Collections;
using UnityEngine;

public class Robot : PlayerCtrl
{
    MonsterCtrl monster;
    public int attackCount;
    bool isFinalSkill = false;
    protected override void Attack(MonsterCtrl enemy)
    {
        if (enemy != null)
        {
            if (!finalSkill)
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
                fireRate = 0.2f;
                monster = enemy;
                _animator.SetBool("isAttacking", true);
                StartCoroutine(Shoot());
            }
        }
    }

    protected override IEnumerator Shoot()
    {
        //Instantiate(muzzleFlash, bulletSpawnPos.position, transform.localRotation);
        Vector3 monsterPos = monster.transform.position;
        GameObject bullet = Instantiate(bulletGo, new Vector3(monsterPos.x, monsterPos.y+0.25f, monsterPos.z), 
            Quaternion.identity);
        bullet.GetComponent<RobotBullet>().damage = damage;
        yield return null;

    }
}
