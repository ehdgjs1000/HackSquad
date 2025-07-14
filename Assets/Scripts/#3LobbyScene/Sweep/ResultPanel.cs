using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] SweepReward[] rewards;

    public IEnumerator UpdateUI(float _goldAmount, float _expAmount)
    {
        rewards[0].Setting(_goldAmount);
        rewards[1].Setting(_expAmount);
        rewards[0].gameObject.SetActive(true);
        rewards[0].transform.DOScale(Vector3.zero, 0);
        rewards[0].transform.DOScale(new Vector3(1.3f,1.3f,1.3f), 0.2f);
        yield return new WaitForSeconds(0.2f);
        rewards[0].transform.DOScale(Vector3.one, 0.2f);
        yield return new WaitForSeconds(0.2f);

        rewards[1].gameObject.SetActive(true);
        rewards[1].transform.DOScale(Vector3.zero, 0);
        rewards[1].transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.2f);
        yield return new WaitForSeconds(0.2f);
        rewards[1].transform.DOScale(Vector3.one, 0.2f);
        yield return new WaitForSeconds(0.2f);
    }

    public void ExitOnClick()
    {
        rewards[0].transform.DOScale(Vector3.zero, 0);
        rewards[1].transform.DOScale(Vector3.zero, 0);
        this.gameObject.SetActive(false);
    }

}
