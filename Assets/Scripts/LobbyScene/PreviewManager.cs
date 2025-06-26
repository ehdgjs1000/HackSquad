using UnityEngine;
using DG.Tweening;

public class PreviewManager : MonoBehaviour
{
    public static PreviewManager instance;

    public GameObject[] heroGos;
    public HeroCard[] heroCards;

    private void Awake()
    {
        if(instance == null) instance = this;
    }
    public void HeroInfoUpdate()
    {
        for (int i = 0; i < heroGos.Length; i++)
        {
            heroGos[i].GetComponent<HeroInfo>().UpdateHeroInfo();
            heroCards[i].UpdateHeroCard();
        }
    }
    public void UpdateHeroGO(string heroName)
    {
        for (int a = 0; a < heroGos.Length; a++)
        {
            /*if(a == num) heroGos[a].SetActive(true);
            else heroGos[a].SetActive(false);*/
            if(heroGos[a].GetComponent<PlayerCtrl>().characterName == heroName)
            {
                heroGos[a].transform.DOScale(Vector3.one, 0.0f); 
            }
            else heroGos[a].transform.DOScale(Vector3.zero, 0.0f);
        }
    }
}
