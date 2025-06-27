using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PopUpMessageBase : MonoBehaviour
{
    public static PopUpMessageBase instance;
    private bool isPoping = false;

    [SerializeField] private GameObject textGO;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    protected void ResetUI()
    {
        textGO.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
    }
    public void SetMessage(string message)
    {
        textGO.GetComponentInChildren<TextMeshProUGUI>().text = message;
        if (!isPoping) StartCoroutine(ShowMessage());
    }
    IEnumerator ShowMessage()
    {
        
        isPoping = true;
        textGO.transform.DOScale(Vector3.one * 1, 0.3f);
        yield return new WaitForSeconds(1.3f);
        
        textGO.transform.DOScale(Vector3.zero, 0.3f);
        yield return new WaitForSeconds(0.1f);
        isPoping = false;
        ResetUI();
    }
}