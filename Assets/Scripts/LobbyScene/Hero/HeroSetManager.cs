using UnityEngine;
using TMPro;

public class HeroSetManager : MonoBehaviour
{
    public static HeroSetManager instance;

    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI gemText;
    [SerializeField] GameObject[] squadHeros; //��� �����忡 ��ϵ� �����
    [SerializeField] GameObject heroDetailPanel;

    public int squadCount = 0;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        UpdateInfo();
        SquadInfoUpdate();
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
        SquadInfoUpdate();
    }
    private void SquadInfoUpdate()
    {
        for (int a = 0; a < 4; a++)
        {
            if(BackEndGameData.Instance.UserHeroData.heroChooseNum[a] != 99)
            {
                squadHeros[a].GetComponent<SquadHero>().hero =
                PreviewManager.instance.heroGos[BackEndGameData.Instance.UserHeroData.heroChooseNum[a]];
            }
            
        }

        //SquadHero�� hero�� ��ϵǾ� ���� ��� Info Update
        for (int a = 0; a < 4; a++)
        {
            if (squadHeros[a].GetComponent<SquadHero>().hero != null)
            {
                squadHeros[a].GetComponent<SquadHero>().UpdateHeroInfo();
            }
            
        }
    }
    public GameObject ReturnHeroInfo(int _pos)
    {
        return squadHeros[_pos];
    }
    public void OpenHeroDetail()
    {
        heroDetailPanel.SetActive(true);
    }

}
