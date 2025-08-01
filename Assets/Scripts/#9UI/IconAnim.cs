using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class IconAnim : MonoBehaviour
{
    Transform iconTs;

    private void Awake()
    {
        iconTs = GetComponent<Transform>();
    }
    private void Start()
    {
        StartCoroutine(PlayIconAnim());
    }
    public IEnumerator PlayIconAnim()
    {
        iconTs.DOLocalMoveY(190.0f, 1.0f);
        yield return new WaitForSeconds(1.0f);
        iconTs.DOLocalMoveY(160.0f, 1.0f);
        yield return new WaitForSeconds(1.0f);
        if(this.gameObject.activeSelf) StartCoroutine(PlayIconAnim());
    }
    
}
