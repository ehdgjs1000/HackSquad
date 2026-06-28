using UnityEngine;

public class Monster : MonoBehaviour
{
    public float maxHp = 100f;
    public float moveSpeed = 1.5f;
    public int expReward = 10;

    float _hp;
    Transform _target;  // 중앙 Squad 위치

    void Start()
    {
        _hp = maxHp;
        // Squad 중앙(origin)을 향해 이동
        var squad = GameObject.FindWithTag("Squad");
        _target = squad != null ? squad.transform : null;
    }

    void Update()
    {
        if (_target == null) return;
        transform.position = Vector3.MoveTowards(
            transform.position, _target.position, moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(float dmg)
    {
        _hp -= dmg;
        if (_hp <= 0f) Die();
    }

    void Die()
    {
        SquadManager.Instance?.AddExp(expReward);
        Destroy(gameObject);
    }
}