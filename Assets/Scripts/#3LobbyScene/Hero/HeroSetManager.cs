using UnityEngine;
using TMPro;
using System.Collections;

public class HeroSetManager : MonoBehaviour
{
    public static HeroSetManager instance;

    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI gemText;
    [SerializeField] GameObject[] squadHeros; //상단 스쿼드에 등록된 히어로
    [SerializeField] GameObject heroDetailPanel;

    public int squadCount = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        UpdateInfo();
        StartCoroutine(InitSquadInfo());
    }
    public void UpdateInfo()
    {
        for (int i =0; i < PreviewManager.instance.heroCards.Length; i++)
        {
            PreviewManager.instance.heroCards[i].UpdateHeroCard();
        }
        goldText.text = BackEndGameData.Instance.UserGameData.gold.ToString();
        gemText.text = BackEndGameData.Instance.UserGameData.gem.ToString();
        SquadInfoUpdate();
    }
    public void ChooseHero(int _pos, GameObject heroGO)
    {
        squadCount++;
        squadHeros[_pos].GetComponent<SquadHero>().hero = heroGO;
        BackEndGameData.Instance.GameDataUpdate();
        SquadInfoUpdate();
    }
    IEnumerator InitSquadInfo()
    {
        yield return new WaitForSeconds(0.2f);
        SquadInfoUpdate();
    }
    private void SquadInfoUpdate()
    {
        squadCount = 0;
        for (int a = 0; a < 4; a++)
        {
            if(BackEndGameData.Instance.UserHeroData.heroChooseNum[a] != 99)
            {
                squadHeros[a].GetComponent<SquadHero>().hero =
                PreviewManager.instance.heroInGameGos[BackEndGameData.Instance.UserHeroData.heroChooseNum[a]];
                squadCount++;
            }
            
        }
        //SquadHero에 hero가 등록되어 있을 경우 Info Update
        for (int a = 0; a < 4; a++)
        {
            if (squadHeros[a].GetComponent<SquadHero>().hero != null)
            {
                squadHeros[a].SetActive(true);
                squadHeros[a].GetComponent<SquadHero>().UpdateHeroInfo();
            }
            else
            {
                squadHeros[a].SetActive(false);
            }
            
        }
    }
    public GameObject ReturnHeroInfo(int _pos)
    {
        return squadHeros[_pos];
    }
    public void OpenHeroDetail()
    {
        SoundManager.instance.BtnClickPlay();
        heroDetailPanel.SetActive(true);
    }

}
