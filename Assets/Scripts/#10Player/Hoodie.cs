using System.Collections;
using UnityEngine;

public class Hoodie : PlayerCtrl
{
    [SerializeField] float maxRandomDegree;
    [SerializeField] float finalSkillTime = 8.0f;
    public int penetrateCount;
    public bool isFinalSkill = false;
    
    protected override void Awake()
    {
        base.Awake();
        penetrateCount = 0;
        maxRandomDegree = 10;
    }
    protected override void Update()
    {
        base.Update();
        finalSkillTime -= Time.deltaTime;
        if (finalSkillTime <= 0.0f && isFinalSkill) UseFinalSkill();
    }
    protected override void Attack(MonsterCtrl enemy)
    {
        fireRate = tempFireRate;
        _animator.SetBool("isAttacking", true);
        StartCoroutine(Shoot());
    }
    public void FixMaxRandomDegree(float degree)
    {
        maxRandomDegree += degree;
    }
    private void UseFinalSkill()
    {
        finalSkillTime = 8.0f;
        GameObject clone =  Instantiate(this.gameObject, new Vector3(0,0,0), Quaternion.identity);
        Destroy(clone, 10.0f);
    }
    protected override IEnumerator Shoot()
    {
        nowBullet--;
        Instantiate(muzzleFlash, bulletSpawnPos.position, transform.localRotation);

        float randomDegree = Random.Range(-maxRandomDegree, maxRandomDegree);
        Quaternion rotation = Quaternion.LookRotation(transform.right);
        GameObject bullet = PoolManager.instance.MakeObj("hoodieBullet");
        bullet.transform.position = bulletSpawnPos.position;
        bullet.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, randomDegree);
        bullet.GetComponent<Bullet>().SetBulletInfo(damage, penetrateCount);

        if (nowBullet <= 0) StartCoroutine(Reloading());
        yield return null;
    }
}
