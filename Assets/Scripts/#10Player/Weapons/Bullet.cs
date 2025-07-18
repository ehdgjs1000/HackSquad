using UnityEngine;
using TMPro;

public abstract class Bullet : MonoBehaviour
{
    public float damage;
    public float criticalChange = 10.0f;
    public float bulletDestroyTime;
    float initBulletDestroyTime;
    [SerializeField] float bulletSpeed;
    protected int penetrateCount = 0;
    protected bool canPenetrate = false;

    [SerializeField] private GameObject damagePopUpTr;
    [SerializeField] private Transform onHitVFX;

    Color color;

    private void Awake()
    {
        initBulletDestroyTime = bulletDestroyTime;
        //StartCoroutine(PoolManager.instance.DeActive(bulletDestroyTime, this.gameObject));
    }
    private void FixedUpdate()
    {
        this.transform.position += transform.forward * bulletSpeed / 100;
    }

    public void SetBulletInfo(float dmg, int _penetrateCount)
    {
        bulletDestroyTime = initBulletDestroyTime;
        StartCoroutine(PoolManager.instance.DeActive(bulletDestroyTime, this.gameObject));
        damage = dmg;
        penetrateCount = _penetrateCount;
    }
    protected abstract void OnHit();
    private void OnCollisionEnter(Collision co)
    {
        try
        {
            if (co.gameObject.CompareTag("Monster"))
            {
                //크리티컬 적용
                float ranCriticalChance = Random.Range(0.0f, 100.0f);
                if (ranCriticalChance <= criticalChange)
                {
                    damage *= 1.5f;
                    damagePopUpTr.GetComponentInChildren<TextMeshPro>().color = Color.red;
                }
                else
                {
                    damagePopUpTr.GetComponentInChildren<TextMeshPro>().color = Color.blue;
                }

                //Enemy 데미지 적용
                if (co.gameObject.GetComponent<MonsterCtrl>() != null)
                {
                    co.gameObject.GetComponent<MonsterCtrl>().GetAttack(damage);
                }
                OnHit();
                OnHitVFX(this.transform.position);

                ColorUtility.TryParseHtmlString("#FF8600", out color);
                //Damage PopUp
                DamagePopUp.Create(new Vector3(co.transform.position.x,
                    co.transform.position.y + 2.0f, co.transform.position.z), damage, color);

                penetrateCount--;
                if (penetrateCount <= 0)
                {
                    StartCoroutine(PoolManager.instance.DeActive(0.0f, this.gameObject));
                    //Destroy(this.gameObject);
                }

            }
        }
        catch (System.ObjectDisposedException e)
        {
            System.Console.WriteLine("Caught: {0}", e.Message);
        }
    }
    private void OnHitVFX(Vector3 _pos)
    {
        if(onHitVFX != null ) Instantiate(onHitVFX, _pos, Quaternion.identity);
    }
}
