using System.Collections;
using UnityEngine;
using DG.Tweening;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance;

    [SerializeField] GameObject[] chapterSet;
    [SerializeField] GameObject levelUpPanel;

    private void Awake()
    {
        BackEndGameData.Instance.GameDataLoad();
        if (instance == null) instance = this;
    }
    private void Start()
    {
        StartCoroutine(InitUI());
    }

    public void LevelUp()
    {
        levelUpPanel.SetActive(true);
        levelUpPanel.GetComponent<LevelUp>().LevelUpUpdate();

        UpdateUIAll();
    }

    IEnumerator InitUI()
    {
        yield return new WaitForSeconds(0.1f);
        BackEndGameData.Instance.GameDataUpdate();
        MainManager.instance.UpdateMainUI();
        DrawManager.instance.UpdateMainUI();
        yield return null;
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

        UpdateUIAll();
    }
    public void UpdateUIAll()
    {
        MainManager.instance.UpdateMainUI();
        HeroSetManager.instance.UpdateInfo();
        AbilityManager.instance.UpdateUI();
        StoreManager.instance.UpdateUI();
        DrawManager.instance.UpdateMainUI();
    }



}
