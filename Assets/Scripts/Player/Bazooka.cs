using System.Collections;
using UnityEngine;

public class Bazooka : PlayerCtrl
{
    [SerializeField] GameObject finalSkillGO;
    [SerializeField] GameObject plane;
    public float finalSkillTime;
    public float exploseRadius;
    Collider[] bMonsterColls;
    float finalSkillRadius = 20.0f;
    float tempFinalSkillTime;
    public bool canUseFinalSkill = false;
    private void Start()
    {
        tempFinalSkillTime = finalSkillTime;
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
    protected override void Attack(MonsterCtrl enemy)
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
        GameObject bullet = Instantiate(bulletGo, bulletSpawnPos.position, transform.localRotation);
        bullet.GetComponent<BazookaBullet>().SetBulletInfo(damage,0);
        bullet.GetComponent<BazookaBullet>().exploseRadius = exploseRadius;

        if (nowBullet <= 0) StartCoroutine(Reloading());

        yield return null;
    }
    IEnumerator BazookaFinalSkill()
    {
        Debug.Log("¹ÙÁÖÄ« ±Ã±Ø±â");
        finalSkillTime = tempFinalSkillTime;
        //ºñÇà±â Áö³ª°¡±â
        Instantiate(plane, new Vector3(22,9,0), Quaternion.Euler(0,-90,0));
        yield return new WaitForSeconds(0.8f);
        //·£´ý À§Ä¡¿¡ Effect ¶ç¿ì±â
        for (int a = 0; a<3; a++)
        {
            ShowFinalEffect();
        }
        

        bMonsterColls = null;
        bMonsterColls = Physics.OverlapSphere(transform.position, finalSkillRadius, monsterLayer);
        foreach (Collider co in bMonsterColls)
        {
            co.GetComponent<MonsterCtrl>().GetAttack(damage * 3);
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
