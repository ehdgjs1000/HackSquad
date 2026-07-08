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
