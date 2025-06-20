using UnityEngine;

public class HeroSetManager : MonoBehaviour
{
    public static HeroSetManager instance;

    [SerializeField] GameObject[] squadHeros; //상단 스쿼드에 등록된 히어로
    [SerializeField] GameObject heroDetailPanel;

    public int squadCount = 0;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        UpdateSquad();
    }
    public void OpenHeroDetail()
    {
        heroDetailPanel.SetActive(true);
    }
    public void UpdateSquad() //상단 스쿼드 정보 update (squadCount만큼만 등록)
    {
        for (int a = 0; a < squadCount; a++)
        {

        }
    }

}
