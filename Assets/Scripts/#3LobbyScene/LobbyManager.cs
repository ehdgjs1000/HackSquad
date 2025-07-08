using System.Collections;
using UnityEngine;
using DG.Tweening;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance;
    int level;
    float exp;

    [SerializeField] GameObject[] chapterSet;

    private void Awake()
    {
        BackEndGameData.Instance.GameDataLoad();
        BackEndGameData.Instance.onGameDataLoadEvent.AddListener(UpdateGameData);
        if (instance == null) instance = this;
    }
    private void Start()
    {
        StartCoroutine(InitUI());
    }

    //서버 연결 시간 기다리기 위해서 0.1초 제공
    IEnumerator InitUI()
    {
        yield return new WaitForSeconds(0.1f);
        MainManager.instance.UpdateMainUI();
        DrawManager.instance.UpdateMainUI();
    }
    public void UpdateGameData()
    {
        level = BackEndGameData.Instance.UserGameData.level;
        exp = BackEndGameData.Instance.UserGameData.exp;
    }

    public void ChapterBtnClick(int _num)
    {
        for (int a = 0; a < chapterSet.Length; a++)
        {
            if (a == _num) chapterSet[a].transform.DOScale(new Vector3(1,1,1),0.0f);
            else chapterSet[a].transform.DOScale(new Vector3(0,0,0), 0.0f);
        }
        if(_num == 1)
        {
            //HeroCard Update / HeroInfo Update
            PreviewManager.instance.HeroInfoUpdate();
        }

        MainManager.instance.UpdateMainUI();
        HeroSetManager.instance.UpdateInfo();
        AbilityManager.instance.UpdateUI();
        StoreManager.instance.UpdateUI();
    }



}
