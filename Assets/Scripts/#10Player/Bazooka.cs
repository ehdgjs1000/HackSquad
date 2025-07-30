using System.Collections;
using UnityEngine;

public class Bazooka : PlayerCtrl
{
    [SerializeField] GameObject finalSkillGO;
    [SerializeField] GameObject plane;
    [SerializeField] AudioClip finalSkillClip, zetClip;
    public float finalSkillTime;
    public float exploseRadius;
    Collider[] bMonsterColls;
    float finalSkillRadius = 20.0f;
    float tempFinalSkillTime;
    public bool canUseFinalSkill = false;
    private void Start()
    {
        tempFinalSkillTime = finalSkillTime;
        exploseRadius = 3;
    }
    protected override void Update()
    {
        base.Update();
        finalSkillTime -= Time.deltaTime;
        if (canUseFinalSkill && finalSkillTime <= 0.0f)
        {
            StartCoroutine(BazookaFinalSkill());
        }
    }
    protected override void Attack(GameObject enemy)
    {
        fireRate = tempFireRate;

        _animator.SetBool("isAttacking", true);
        StartCoroutine(Shoot());
    }
    public void UpgradeExploseRadius(float _amount)
    {
        exploseRadius *= _amount;
    }
    protected override IEnumerator Shoot()
    {
        nowBullet--;
        Instantiate(muzzleFlash, bulletSpawnPos.position, transform.localRotation);
        SoundManager.instance.PlaySound(weaponFireClip);

        GameObject bullet = PoolManager.instance.MakeObj("bazookaBullet");
        bullet.transform.position = bulletSpawnPos.transform.position;
        bullet.transform.rotation = transform.localRotation;
        bullet.GetComponent<BazookaBullet>().SetBulletInfo(damage,0);
        bullet.GetComponent<BazookaBullet>().exploseRadius = exploseRadius;
        StartCoroutine(PoolManager.instance.DeActive(1.0f, bullet));

        if (nowBullet <= 0) StartCoroutine(Reloading());

        yield return null;
    }
    IEnumerator BazookaFinalSkill()
    {
        Debug.Log("Bazookafinal");
        finalSkillTime = tempFinalSkillTime;
        //비행기 지나가기
        SoundManager.instance.PlaySound(zetClip);
        Instantiate(plane, new Vector3(22,9,0), Quaternion.Euler(0,-90,0));
        yield return new WaitForSeconds(0.8f);
        //랜덤 위치에 Effect 띄우기
        for (int a = 0; a<3; a++)
        {
            ShowFinalEffect();
        }
        SoundManager.instance.PlaySound(finalSkillClip);

        bMonsterColls = null;
        bMonsterColls = Physics.OverlapSphere(transform.position, finalSkillRadius, monsterLayer);
        foreach (Collider co in bMonsterColls)
        {
            co.GetComponent<MonsterCtrl>().GetAttack(damage * 2);
            DamagePopUp.Create(new Vector3(co.transform.position.x,
                    co.transform.position.y + 2.0f, co.transform.position.z), damage, Color.green);

        }

        yield return null;
    }
    private void ShowFinalEffect()
    {
        float ranX = Random.Range(-6.5f, 6.5f);
        float ranZ = Random.Range(-12f, 12f);
        Instantiate(finalSkillGO, new Vector3(ranX,0,ranZ), Quaternion.identity);
    }
    public void UseFinalSkill()
    {
        canUseFinalSkill = true;
    }
}
