using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float maxHp = 100f;
    public float moveSpeed = 1.5f;
    public int expReward = 10;

    float _hp;
    Transform _target;
    Renderer _renderer;
    Color _baseColor;

    // 슬로우 배율 목록(1=정상, 0=완전정지). 여러 장판이 겹칠 수 있어 가장 강한(최소) 값을 적용
    readonly List<float> _slowMultipliers = new();
    public bool IsSlowed => _slowMultipliers.Count > 0;

    public void AddSlow(float multiplier) => _slowMultipliers.Add(multiplier);
    public void RemoveSlow(float multiplier) => _slowMultipliers.Remove(multiplier);

    // 화상(Burn): 1틱=1초. 재적용 시 남은 틱만 최대치로 갱신되고, 폭발까지의 경과 틱은 계속 누적된다
    const float BurnTickInterval = 1f;
    BurnApplication _burn;
    int _burnTicksRemaining;
    int _burnElapsedTicks;
    bool _burning;
    GameObject _burnVfxInstance;

    public void ApplyBurn(BurnApplication burn)
    {
        if (!burn.IsActive) return;

        _burn = burn;
        _burnTicksRemaining = burn.maxTicks;

        if (_burning) return;
        _burning = true;
        _burnVfxInstance = VFXManager.SpawnBurnVFX(transform);
        StartCoroutine(BurnLoop());
    }

    IEnumerator BurnLoop()
    {
        var wait = new WaitForSeconds(BurnTickInterval);
        while (_burnTicksRemaining > 0)
        {
            yield return wait;
            _burnTicksRemaining--;
            _burnElapsedTicks++;

            TakeDamage(_burn.damagePerTick);

            if (_burn.explodeEveryTicks > 0 && _burnElapsedTicks % _burn.explodeEveryTicks == 0)
                TriggerBurnExplosion();
        }

        _burning = false;
        _burnElapsedTicks = 0;
        if (_burnVfxInstance != null) Destroy(_burnVfxInstance);
    }

    // 연소폭발: explodeEveryTicks마다 발생하는 광역 폭발 (자신 포함 범위 내 전체 피해)
    void TriggerBurnExplosion()
    {
        if (_burn.explosionVfxPrefab != null)
        {
            var vfx = Instantiate(_burn.explosionVfxPrefab, transform.position, Quaternion.identity);
            if (!vfx.TryGetComponent<ParticleSystem>(out var ps))
                Destroy(vfx, 3f);
            else if (ps.main.stopAction != ParticleSystemStopAction.Destroy)
                Destroy(vfx, ps.main.duration + ps.main.startLifetime.constantMax);
        }

        int monsterLayer = LayerMask.GetMask("Monster");
        var cols = Physics.OverlapSphere(transform.position, _burn.explosionRadius, monsterLayer);
        foreach (var col in cols)
            if (col.TryGetComponent(out Monster m))
                m.TakeDamage(_burn.explosionDamage);
    }

    void Start()
    {
        _hp = maxHp;
        var squad = GameObject.FindWithTag("Squad");
        _target = squad != null ? squad.transform : null;

        _renderer = GetComponent<Renderer>();
        if (_renderer != null)
            _baseColor = _renderer.material.color;
    }

    void Update()
    {
        if (_target == null) return;

        float speedMultiplier = 1f;
        for (int i = 0; i < _slowMultipliers.Count; i++)
            speedMultiplier = Mathf.Min(speedMultiplier, _slowMultipliers[i]);

        transform.position = Vector3.MoveTowards(
            transform.position, _target.position, moveSpeed * speedMultiplier * Time.deltaTime);
    }

    public void TakeDamage(float dmg, bool isCrit = false)
    {
        _hp -= dmg;

        DamageTextManager.Show(transform.position, dmg, isCrit);

        if (_renderer != null)
        {
            float ratio = Mathf.Clamp01(_hp / maxHp);
            _renderer.material.color = Color.Lerp(Color.red, _baseColor, ratio);
        }
        if (_hp <= 0f) Die();
    }

    void Die()
    {
        SquadManager.Instance?.AddExp(expReward);
        Destroy(gameObject);
    }
}
