using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float maxLifetime = 3f;

    float _damage;
    bool _isCrit;
    int _pierceLeft;
    Vector3 _direction;
    int _monsterLayer;
    GameObject _hitVFX;

    // 복면 전용 옵션(기본값 0/null이면 기존 히어로 동작은 그대로 유지됨)
    float _damageIncreasePerPierce;
    float _splitChance;
    float _splitAngle;

    public void Init(float damage, Vector3 direction, int pierceCount, GameObject hitVFX = null, bool isCrit = false,
        float damageIncreasePerPierce = 0f, float splitChance = 0f, float splitAngle = 0f)
    {
        _damage = damage;
        _isCrit = isCrit;
        _direction = direction.normalized;
        _pierceLeft = pierceCount;
        _hitVFX = hitVFX;
        _monsterLayer = LayerMask.GetMask("Monster");
        _damageIncreasePerPierce = damageIncreasePerPierce;
        _splitChance = splitChance;
        _splitAngle = splitAngle;
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

        monster.TakeDamage(_damage, _isCrit);
        SpawnVFX();
        TrySplit();

        // 관통마다 데미지 누적 증가 (복면 스킬3)
        if (_damageIncreasePerPierce > 0f)
            _damage *= 1f + _damageIncreasePerPierce;

        // pierceCount == -1: 무한관통, 절대 파괴되지 않고 maxLifetime으로만 소멸
        if (_pierceLeft == 0)
            Destroy(gameObject);
        else if (_pierceLeft > 0)
            _pierceLeft--;
    }

    // 분열탄(복면 스킬2 최종): 명중 시 확률로 좌우 대칭 추가탄 발사 (기존 총알 프리팹 그대로 복제)
    void TrySplit()
    {
        if (_splitChance <= 0f) return;
        if (Random.value >= _splitChance) return;

        SpawnSplitBullet(Quaternion.Euler(0f, -_splitAngle, 0f) * _direction);
        SpawnSplitBullet(Quaternion.Euler(0f, _splitAngle, 0f) * _direction);
    }

    void SpawnSplitBullet(Vector3 dir)
    {
        var go = Instantiate(gameObject, transform.position, Quaternion.LookRotation(dir));
        if (go.TryGetComponent(out Bullet bullet))
            bullet.Init(_damage, dir, 0, _hitVFX, _isCrit); // 분열탄은 단발(추가 분열/관통 없음)
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
