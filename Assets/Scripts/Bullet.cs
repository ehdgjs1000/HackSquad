using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float maxLifetime = 3f;

    float _damage;
    int _pierceLeft;
    Vector3 _direction;
    int _monsterLayer;

    public void Init(float damage, Vector3 direction, int pierceCount)
    {
        _damage = damage;
        _direction = direction.normalized;
        _pierceLeft = pierceCount;
        _monsterLayer = LayerMask.GetMask("Monster");
        Destroy(gameObject, maxLifetime);
    }

    void Update()
    {
        transform.position += _direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _monsterLayer) == 0) return;
        if (!other.TryGetComponent(out Monster monster)) return;

        monster.TakeDamage(_damage);

        if (_pierceLeft <= 0)
            Destroy(gameObject);
        else
            _pierceLeft--;
    }
}
