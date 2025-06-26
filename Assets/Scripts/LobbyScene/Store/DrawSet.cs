using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class DrawSet : MonoBehaviour
{
    [SerializeField] GameObject drawMonitor;
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject drawPanel;

    [SerializeField] GameObject[] normalHeros;
    [SerializeField] GameObject[] specialHeros;
    [SerializeField] GameObject[] epicHeros;
    [SerializeField] GameObject[] legendHeros;

    [SerializeField] DrawHeroGo draw1Hero;
    [SerializeField] DrawHeroGo[] draw10Heros;

    float cardMoveTime = 0.3f;
    bool canExit = false;
    [SerializeField] GameObject exitText;

    public void DrawCardOnClick(int drawNum)
    {
        if (drawNum == 1 && BackEndGameData.Instance.UserGameData.gem >= 100)
        {
            BackEndGameData.Instance.UserGameData.gem -= 100;
            StartCoroutine(DrawHeros(drawNum));
        }
        else if(drawNum == 10 && BackEndGameData.Instance.UserGameData.gem >= 900)
        {
            BackEndGameData.Instance.UserGameData.gem -= 900;
            StartCoroutine(DrawHeros(drawNum));
        }
        StoreManager.instance.UpdateMainUI();
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
        if (drawNum == 1)
        {
            draw1Hero.SetHero(RandomReturnHero());
            draw1Hero.InfoUpdate();
            draw1Hero.gameObject.transform.DOScale(Vector3.one, 1.0f);
            BackEndGameData.Instance.GameDataUpdate();
            PreviewManager.instance.HeroInfoUpdate();
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                draw10Heros[i].SetHero(RandomReturnHero());
                draw10Heros[i].InfoUpdate();
                draw10Heros[i].gameObject.transform.DOScale(new Vector3(0.4f, 0.4f, 0.4f), 0.0f);
            }
            BackEndGameData.Instance.GameDataUpdate();
            for (int i = 0; i < 10; i++)
            {
                if(i == 0 ) draw10Heros[i].gameObject.transform.DOLocalMove(new Vector3(0,710,0), cardMoveTime);
                else if(i == 1) draw10Heros[i].gameObject.transform.DOLocalMove(new Vector3(350, 600, 0), cardMoveTime);
                else if (i == 2) draw10Heros[i].gameObject.transform.DOLocalMove(new Vector3(350, 200, 0), cardMoveTime);
                else if (i == 3) draw10Heros[i].gameObject.transform.DOLocalMove(new Vector3(350, -200, 0), cardMoveTime);
                else if (i == 4) draw10Heros[i].gameObject.transform.DOLocalMove(new Vector3(350, -600, 0), cardMoveTime);
                else if (i == 5) draw10Heros[i].gameObject.transform.DOLocalMove(new Vector3(0, -710, 0), cardMoveTime);
                else if (i == 6) draw10Heros[i].gameObject.transform.DOLocalMove(new Vector3(-350, -600, 0), cardMoveTime);
                else if (i == 7) draw10Heros[i].gameObject.transform.DOLocalMove(new Vector3(-350, -200, 0), cardMoveTime);
                else if (i == 8) draw10Heros[i].gameObject.transform.DOLocalMove(new Vector3(-350, 200, 0), cardMoveTime);
                else if (i == 9) draw10Heros[i].gameObject.transform.DOLocalMove(new Vector3(-350, 600, 0), cardMoveTime);
                yield return new WaitForSeconds(0.4f);
            }
        }
        //BackEndGameData.Instance.GameDataUpdate();
        yield return new WaitForSeconds(1.0f);
        exitText.SetActive(true);
        canExit = true;
    }
    public void ExitDrawHeroCard()
    {
        if (canExit)
        {
            drawMonitor.transform.DOLocalRotate(new Vector3(90, 0, 0), 0);
            draw1Hero.gameObject.transform.localPosition = new Vector3(0, 0, 0);
            draw1Hero.gameObject.transform.DOScale(Vector3.zero, 0);
            for (int i = 0; i < 10; i++)
            {
                draw10Heros[i].gameObject.transform.localPosition = new Vector3(0, 0, 0);
                draw10Heros[i].gameObject.transform.DOScale(Vector3.zero, 0);
            }

            //카드 뽑기 뷰에서 나가기
            canExit = false;
            exitText.SetActive(false);
            drawPanel.SetActive(false);
        }

    }

    /// <summary>
    /// 뽑기확률 0 = 70% / 1 = 25% / 2 = 4% / 3 = 1%
    /// </summary>
    /// <returns></returns>
    private GameObject RandomReturnHero()
    {
        GameObject drawHeroGo = null ;
        float randomProb = Random.Range(0.0f, 100.0f);
        if(randomProb <= 70.0f)
        {
            int ranNum = Random.Range(0,normalHeros.Length);
            drawHeroGo = normalHeros[ranNum];
        }else if (randomProb > 70.0f && randomProb <= 95.0f)
        {
            int ranNum = Random.Range(0, specialHeros.Length);
            drawHeroGo = specialHeros[ranNum];
        }
        else if (randomProb > 95.0f && randomProb <= 99.0f)
        {
            int ranNum = Random.Range(0, epicHeros.Length);
            drawHeroGo = epicHeros[ranNum];
        }
        else if (randomProb > 99.0f)
        {
            int ranNum = Random.Range(0, legendHeros.Length);
            drawHeroGo = legendHeros[ranNum];
        }

        //서버에 뽑은 히어로 추가 및 Update
        BackEndGameData.Instance.UserHeroData.heroCount[drawHeroGo.GetComponent<HeroInfo>().ReturnHeroNum()]++;
        return drawHeroGo;
    }


}
