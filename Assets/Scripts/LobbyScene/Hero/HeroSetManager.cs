using UnityEngine;
using TMPro;

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
       UpdateTopInfo();
    }
    public void UpdateTopInfo()
    {
        goldText.text = BackEndGameData.Instance.UserGameData.gold.ToString();
        gemText.text = BackEndGameData.Instance.UserGameData.gem.ToString();
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
