using System.Collections;
using UnityEngine;

public class MonsterCtrl : MonoBehaviour
{
    //Stats
    [SerializeField] float speed;
    [SerializeField] float hp;
    bool canMove = true;

    //BattleInfo
    Collider[] playerColl = new Collider[3];
    [SerializeField] LayerMask playerLayer;
    Vector3 targetPos, dir;
    Quaternion lookTarget;
    bool isMoving = false;
    [SerializeField] float attackRange;
    

    private void Update()
    {
        if (canMove)
        {
            MoveToTarget();
        }
    }
    public IEnumerator GetStun(float _stunTime)
    {
        canMove = false;
        yield return new WaitForSeconds(_stunTime);
        canMove = true;
    }
    public IEnumerator GetSlow(float _slowAmount)
    {
        float tempSpeed = speed;
        speed *= (1 - _slowAmount);
        yield return new WaitForSeconds(2.0f);
        speed = tempSpeed;
    }
    private void MoveToTarget()
    {
        targetPos = PlayerCtrl.instance.transform.position;
        dir = targetPos - transform.position;
        lookTarget = Quaternion.LookRotation(dir);

        if ((transform.position - targetPos).magnitude > attackRange)
        {
            transform.position += dir.normalized * Time.deltaTime * speed;
            transform.rotation = Quaternion.Lerp(transform.rotation, lookTarget, 0.25f);
            isMoving = true;
        }
        else
        {
            isMoving = false;
            AttackPlayer();
        }
    }
    public void GetAttack(float _dmg)
    {
        hp -= _dmg;
        if (hp <= 0.0f) Die();
    }
    private void Die()
    {
        

    }
    private void AttackPlayer()
    {
        //공격 구현

    }
    

}
