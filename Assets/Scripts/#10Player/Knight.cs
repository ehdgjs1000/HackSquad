using System.Collections;
using UnityEngine;

public class Knight : PlayerCtrl
{
    [SerializeField] float maxRandomDegree;
    [SerializeField] Transform bulletSpawnPos2;
    [SerializeField] Transform healVFX;
    public int penetrateCount;

    public bool isFinalSkill = false;
    float tempHealTime;
    float tempFinalHealTime;
    float healTime = 5.0f;
    float finalHealTime = 20.0f;
    float healAmount = 2;
    protected override void Awake()
    {
        base.Awake();
        penetrateCount = 0;
        maxRandomDegree = 25;
        isFinalSkill = false;
        tempHealTime = healTime;
        tempFinalHealTime = finalHealTime;
        healAmount *= Mathf.Pow(1.5f, BackEndGameData.Instance.UserHeroData.heroLevel[heroNum]);
    }
    protected override void Update()
    {
        base.Update();
        healTime -= Time.deltaTime;
        if(GameManager.instance != null)
        {
            if (healTime <= 0.0f) Heal();
            if (isFinalSkill)
            {
                finalHealTime -= Time.deltaTime;
                if (finalHealTime <= 0.0f) FinalHeal();
            }
        }
        
    }
    private void FinalHeal()
    {
        finalHealTime = tempFinalHealTime;
        if(GameManager.instance!=null)GameManager.instance.HealHP(healAmount * 3);
        Instantiate(healVFX, new Vector3(0,0.5f,0),Quaternion.identity);
        DamagePopUp.Create(new Vector3(0, 2.0f, 0), healAmount * 3, Color.green);
    }
    private void Heal()
    {
        healTime = tempHealTime;
        if (GameManager.instance != null) GameManager.instance.HealHP(healAmount);
        Instantiate(healVFX, new Vector3(0, 0.5f, 0), Quaternion.identity);
        DamagePopUp.Create(new Vector3(0,2.0f,0), healAmount, Color.green);
    }
    public void HealUpgrade()
    {
        healAmount++;
    }
    public void FixMaxRandomDegree(float _amount)
    {
        maxRandomDegree += _amount;
    }
    protected override void Attack(GameObject enemy)
    {
        fireRate = tempFireRate;
        _animator.SetBool("isAttacking", true);
        StartCoroutine(Shoot());
    }

    protected override IEnumerator Shoot()
    {
        nowBullet--;
        Instantiate(muzzleFlash, bulletSpawnPos.position, transform.localRotation);
        SoundManager.instance.PlaySound(weaponFireClip);

        float randomDegree = Random.Range(-maxRandomDegree, maxRandomDegree);
        Quaternion rotation = Quaternion.LookRotation(transform.right);
        GameObject bullet1 = PoolManager.instance.MakeObj("knightBullet");
        bullet1.transform.position = bulletSpawnPos.position;
        bullet1.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, randomDegree);
        bullet1.GetComponent<Bullet>().SetBulletInfo(damage, penetrateCount);

        nowBullet--;
        randomDegree = Random.Range(-maxRandomDegree, maxRandomDegree);
        GameObject bullet2 = PoolManager.instance.MakeObj("knightBullet");
        bullet2.transform.position = bulletSpawnPos.position;
        bullet2.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, randomDegree);
        bullet2.GetComponent<Bullet>().SetBulletInfo(damage, penetrateCount);

        if (nowBullet <= 0) StartCoroutine(Reloading());
        yield return null;
    }
}
