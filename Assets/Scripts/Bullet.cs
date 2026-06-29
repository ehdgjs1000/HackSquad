using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float maxLifetime = 3f;

    float _damage;
    int _pierceLeft;
    Vector3 _direction;
    int _monsterLayer;
    GameObject _hitVFX;

    public void Init(float damage, Vector3 direction, int pierceCount, GameObject hitVFX = null)
    {
        _damage = damage;
        _direction = direction.normalized;
        _pierceLeft = pierceCount;
        _hitVFX = hitVFX;
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
        SpawnVFX();

        if (_pierceLeft <= 0)
            Destroy(gameObject);
        else
            _pierceLeft--;
    }

    void SpawnVFX()
    {
        if (_hitVFX == null) return;
        var vfx = Instantiate(_hitVFX, transform.position, Quaternion.identity);
        if (!vfx.TryGetComponent<ParticleSystem>(out var ps))
            Destroy(vfx, 3f);
        else if (ps.main.stopAction != ParticleSystemStopAction.Destroy)
            Destroy(vfx, ps.main.duration + ps.main.startLifetime.constantMax);
    }
}
