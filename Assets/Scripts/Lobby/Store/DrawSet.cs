using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class DrawSet : MonoBehaviour
{
    [SerializeField] GameObject drawMonitor;
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject drawPanel;


    public void DrawCardOnClick(int drawNum)
    {
        if (drawNum == 1) StartCoroutine(DrawHeros(drawNum));

    }
    IEnumerator DrawHeros(int drawNum)
    {
        Vector3 initCameraPos = new Vector3(0, 1, -10);
        drawMonitor.transform.DOLocalRotate(new Vector3(-31,0,0), 1.0f);
        yield return new WaitForSeconds(1.2f);
        //카메라 이동
        mainCamera.transform.DOMove(new Vector3(0,2.6f,-2.5f),1.2f);
        yield return new WaitForSeconds(1.2f);
        drawPanel.SetActive(true);
        mainCamera.transform.position = initCameraPos;

        if(drawNum == 1)
        {

        }
        else
        {

        }

    }


}
