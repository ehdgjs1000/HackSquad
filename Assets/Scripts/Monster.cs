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
        transform.position = Vector3.MoveTowards(
            transform.position, _target.position, moveSpeed * Time.deltaTime);
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
