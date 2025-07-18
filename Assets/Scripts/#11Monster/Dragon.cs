using System.Collections;
using UnityEngine;

public class Dragon : MonsterCtrl
{
    [SerializeField] GameObject dragonFireVfx;
    [SerializeField] Transform fireSpawnPos;

    protected override IEnumerator Attack()
    {
        //StartCoroutine(base.Attack());
        GameObject bullet = Instantiate(dragonFireVfx, fireSpawnPos.position, Quaternion.identity);
        bullet.GetComponent<DragonFire>().SetUp(damage);
        attackTerm = tempAttackTerm;

        return null;
    }

}
