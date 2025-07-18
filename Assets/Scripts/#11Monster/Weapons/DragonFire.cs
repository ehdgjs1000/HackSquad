using System.Collections;
using UnityEngine;

public class DragonFire : MonoBehaviour
{
    [SerializeField] Transform hitVFX;
    [SerializeField] float speed;

    float damage;
    private void Awake()
    {
        Destroy(this.gameObject, 2.0f);
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, speed * Time.deltaTime);
        this.transform.LookAt(Vector3.zero);
    }
    public void SetUp(float _damage)
    {
        damage = _damage;
    }

    IEnumerator Attack()
    {
        GameManager.instance.GetDamage(damage);
        Instantiate(hitVFX, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        yield return null;
    }
    private void OnTriggerEnter(Collider co)
    {
        if (co.CompareTag("Player"))
        {
            StartCoroutine(Attack());
        }
    }
}
