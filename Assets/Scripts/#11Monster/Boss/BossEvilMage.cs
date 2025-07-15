using System.Collections;
using UnityEngine;

public class BossEvilMage : BossMonsterCtrl
{
    [SerializeField] Transform magicSpawnPos;
    [SerializeField] EvilMageProjectile magicVFX;
    protected override IEnumerator Attack()
    {
        _animator.SetTrigger("Attack");
        attackTerm = tempAttackTerm;
        yield return new WaitForSeconds(0.3f);
        Instantiate(magicVFX.gameObject, magicSpawnPos.position, Quaternion.identity);
        magicVFX.SetUp(damage);
    }

}
