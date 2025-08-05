using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance;

    [SerializeField] GameObject[] chapterSet;
    [SerializeField] GameObject levelUpPanel;
    [SerializeField] Button[] bottomButtonSet;

    [SerializeField] AudioClip questClickClip;

    private void Awake()
    {
        BackEndGameData.Instance.GameDataLoad();
        if (instance == null) instance = this;
    }
    private void Start()
    {
        StartCoroutine(InitUI());
        SoundManager.instance.PlayBGM();
    }

    public void QuestClearSoundPlay()
    {
        SoundManager.instance.PlaySound(questClickClip);
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
        Color color;
        SoundManager.instance.BtnClickPlay();
        for (int a = 0; a < chapterSet.Length; a++)
        {
            if (a == _num)
            {
                chapterSet[a].transform.DOScale(new Vector3(1, 1, 1), 0.0f);
                ColorUtility.TryParseHtmlString("#FF45EA", out color);
                bottomButtonSet[a].image.color = color;
            }
            else
            {
                chapterSet[a].transform.DOScale(new Vector3(0, 0, 0), 0.0f);
                bottomButtonSet[a].image.color = Color.white;
            }
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
    public void BtnClickPlay()
    {
        SoundManager.instance.BtnClickPlay();
    }

}
