using UnityEngine;

public class MonsterCtrl : MonoBehaviour
{
    //Stats
    [SerializeField] float speed;
    [SerializeField] float hp;

    //BattleInfo
    Collider[] playerColl = new Collider[3];
    LayerMask playerLayer;
    Vector3 targetPos, dir;
    Quaternion lookTarget;
    bool isMoving = false;
    [SerializeField] float attackRange;
    

    private void Update()
    {
        MoveToTarget();
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
        Debug.Log("Die");

    }
    private void AttackPlayer()
    {
        //공격 구현

    }
    

}
