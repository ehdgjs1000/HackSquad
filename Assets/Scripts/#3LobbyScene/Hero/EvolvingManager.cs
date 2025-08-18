using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EvolvingManager : MonoBehaviour
{
    [SerializeField] GameObject[] leftStars;
    [SerializeField] GameObject[] rightStars;
    [SerializeField] TextMeshProUGUI needItemText;
    [SerializeField] TextMeshProUGUI skillDesc;
    [SerializeField] Image skillImage;

    int nowEvolvingLevel;
    int nextEvolvingLevel;

    private void Start()
    {
        
    }
    public void UpdateUI()
    {
        //todo : 시작시 evolving level 연동

        //상단 별 ui관리
        foreach(GameObject starGo in leftStars) starGo.SetActive(false);
        foreach(GameObject starGo in rightStars) starGo.SetActive(false);
        for (int i = 0; i < nowEvolvingLevel; i++) leftStars[i].SetActive(true);
        for (int i = 0; i < nextEvolvingLevel; i++) rightStars[i].SetActive(true);



    }
}
