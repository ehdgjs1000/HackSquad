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
        iconTs.DOLocalMoveY(150.0f, 1.0f);
        yield return new WaitForSeconds(1.0f);
        iconTs.DOLocalMoveY(156.0f, 1.0f);
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(PlayIconAnim());
    }
    
}
