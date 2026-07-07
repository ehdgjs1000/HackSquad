using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 8f;
    public float maxLifetime = 5f;

    float _damage;
    bool _isCrit;
    float _explosionRadius;
    int _clusterCount;
    bool _hasNapalm;
    float _napalmDamageRatio;
    float _napalmDuration;
    float _napalmTickInterval;
    GameObject _hitVFX;
    GameObject _napalmVFX;
    Transform _target;
    Vector3 _targetPos;
    int _monsterLayer;
    bool _exploded;
    float _vfxScale = 1f;

    public void Init(float damage, Transform target, float explosionRadius, GameObject hitVFX, int clusterCount = 0, bool hasNapalm = false, GameObject napalmVFX = null, float vfxScale = 1f, float napalmDamageRatio = 0.3f, float napalmDuration = 5f, float napalmTickInterval = -1f, bool isCrit = false)
    {
        _damage = damage;
        _isCrit = isCrit;
        _target = target;
        _targetPos = target.position;
        _explosionRadius = explosionRadius;
        _hitVFX = hitVFX;
        _clusterCount = clusterCount;
        _hasNapalm = hasNapalm;
        _napalmVFX = napalmVFX;
        _vfxScale = vfxScale;
        _napalmDamageRatio = napalmDamageRatio;
        _napalmDuration = napalmDuration;
        _napalmTickInterval = napalmTickInterval;
        _monsterLayer = LayerMask.GetMask("Monster");
        Destroy(gameObject, maxLifetime);
    }

    void Update()
    {
        if (_exploded) return;
        if (_target != null)
            _targetPos = _target.position;
        _targetPos.y = transform.position.y;

        transform.position = Vector3.MoveTowards(transform.position, _targetPos, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _targetPos) < 0.2f)
            Explode();
    }

    void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _monsterLayer) == 0) return;
        Explode();
    }

    void Explode()
    {
        if (_exploded) return;
        _exploded = true;

        SpawnVFX();

        var cols = Physics.OverlapSphere(transform.position, _explosionRadius, _monsterLayer);
        foreach (var col in cols)
            if (col.TryGetComponent(out Monster m))
                m.TakeDamage(_damage, _isCrit);

        if (_clusterCount > 0)
        {
            for (int i = 0; i < _clusterCount; i++)
            {
                Vector2 rand = Random.insideUnitCircle * _explosionRadius;
                Vector3 clusterPos = transform.position + new Vector3(rand.x, 0f, rand.y);
                var clusterCols = Physics.OverlapSphere(clusterPos, _explosionRadius * 0.4f, _monsterLayer);
                foreach (var col in clusterCols)
                    if (col.TryGetComponent(out Monster m))
                        m.TakeDamage(_damage * 0.5f, _isCrit);
            }
        }

        if (_hasNapalm)
            FireZone.Spawn(_napalmVFX, transform.position, _damage * _napalmDamageRatio, _explosionRadius, _napalmDuration, _vfxScale, _napalmTickInterval);

        Destroy(gameObject);
    }

    void SpawnVFX()
    {
        if (_hitVFX == null) return;
        var vfx = Instantiate(_hitVFX, transform.position, Quaternion.identity);
        vfx.transform.localScale *= _vfxScale;
        if (!vfx.TryGetComponent<ParticleSystem>(out var ps))
            Destroy(vfx, 3f);
        else if (ps.main.stopAction != ParticleSystemStopAction.Destroy)
            Destroy(vfx, ps.main.duration + ps.main.startLifetime.constantMax);
    }
}
