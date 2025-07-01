using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BossImage : MonoBehaviour
{
    [SerializeField] Vector3 fadeAmount;
    float fadeDuration = 0.9f;
    Image bossImage;
    private void Awake()
    {
        bossImage = GetComponent<Image>();
    }
    // 5�� 10�� ���� ���� 5���� ����
    public IEnumerator BossImageFade()
    {
        for (int i = 0; i < 5; i++)
        {
            bossImage.transform.DOScale(fadeAmount, fadeDuration);
            yield return new WaitForSeconds(fadeDuration);
            bossImage.transform.DOScale(Vector3.one, fadeDuration);
            yield return new WaitForSeconds(fadeDuration);
        }
        this.gameObject.SetActive(false);
    }



}
