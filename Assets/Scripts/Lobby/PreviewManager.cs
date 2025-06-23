using UnityEngine;

public class PreviewManager : MonoBehaviour
{
    [SerializeField] GameObject[] heroGos;


    public void UpdateHeroGO(string heroName)
    {
        for (int a = 0; a < heroGos.Length; a++)
        {
            /*if(a == num) heroGos[a].SetActive(true);
            else heroGos[a].SetActive(false);*/
            if(heroGos[a].GetComponent<PlayerCtrl>().characterName == heroName)
            {
                heroGos[a].SetActive(true);
            }
            else heroGos[a].SetActive(false);
        }
    }
}
