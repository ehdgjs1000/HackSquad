using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    private TextMeshPro damageMesh;
    private float dissapearTime = 0.7f;

    private void Awake()
    {
        damageMesh = GetComponent<TextMeshPro>();
    }
    private void Update()
    {
        dissapearTime -= Time.deltaTime;
        if (dissapearTime <= 0.0f) Destroy(this.gameObject);
    }
    public static DamagePopUp Create(Vector3 pos, float damage)
    {
        Transform damagePopUpTransform = Instantiate(GameAsset.Instance.pfDamagePopUp,
            pos, Quaternion.identity);
        DamagePopUp damagePopUp = damagePopUpTransform.GetComponent<DamagePopUp>();
        damagePopUp.SetUp(damage);

        return damagePopUp;
    }
    IEnumerator FadeDamage()
    {
        this.transform.DOScale(new Vector3(1.1f,1.1f,1.1f), 0.4f);
        yield return new WaitForSeconds(0.4f);
        this.transform.DOScale(Vector3.zero, 0.3f);
    }
    public void SetUp(float damage)
    {
        damageMesh.SetText(damage.ToString("F1"));
        StartCoroutine(FadeDamage());
    }
}
