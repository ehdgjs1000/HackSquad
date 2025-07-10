using System.Collections;
using UnityEngine;

public class Robot : PlayerCtrl
{
    MonsterCtrl monster;
    public int attackCount;
    public bool isFinalSkill = false;
    protected override void Attack(MonsterCtrl enemy)
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
                fireRate = 0.2f;
                monster = enemy;
                _animator.SetBool("isAttacking", true);
                StartCoroutine(Shoot());
            }
        }
    }

    protected override IEnumerator Shoot()
    {
        Debug.Log("shoot");
        //Instantiate(muzzleFlash, bulletSpawnPos.position, transform.localRotation);
        Vector3 monsterPos = monster.transform.position;
        GameObject bullet = PoolManager.instance.MakeObj("robotBullet");
        bullet.transform.position = new Vector3(monsterPos.x, monsterPos.y + 0.25f, monsterPos.z);
        bullet.transform.rotation = Quaternion.identity;
        bullet.GetComponent<RobotBullet>().damage = damage;
        yield return null;

    }
}
