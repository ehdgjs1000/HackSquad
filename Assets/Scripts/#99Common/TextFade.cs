using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class TextFade : MonoBehaviour
{
    TextMeshProUGUI text;
    Transform textTs;
    private void OnEnable()
    {
        StartCoroutine(FadeText());
    }
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        textTs = text.transform;
    }
    
    IEnumerator FadeText()
    {
        while (true)
        {
            textTs.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1.0f);
            yield return new WaitForSeconds(1.1f);
            textTs.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 1.0f);
            yield return new WaitForSeconds(1.1f);
        }
    }


}
