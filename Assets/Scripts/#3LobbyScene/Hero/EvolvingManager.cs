using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EvolvingManager : MonoBehaviour
{
    public static EvolvingManager instance;

    EvolvingInfo evolvingInfo;

    [SerializeField] GameObject[] leftStars;
    [SerializeField] GameObject[] rightStars;
    [SerializeField] TextMeshProUGUI needItemText;
    [SerializeField] TextMeshProUGUI skillDesc;
    [SerializeField] Image skillImage;
    [SerializeField] GameObject mainSet;
    [SerializeField] GameObject highestLevelSet;

    int nowEvolvingLevel;
    int nextEvolvingLevel;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateUI(int heroNum, EvolvingInfo _evolvingInfo)
    {
        evolvingInfo = _evolvingInfo;
        nowEvolvingLevel = BackEndGameData.Instance.UserEvolvingData.evolvingLevel[heroNum];
        nextEvolvingLevel = nowEvolvingLevel + 1;

        //상단 별 ui관리
        foreach (GameObject starGo in leftStars) starGo.SetActive(false);
        foreach (GameObject starGo in rightStars) starGo.SetActive(false);
        if (nowEvolvingLevel < 3)
        {
            highestLevelSet.SetActive(false);
            mainSet.SetActive(true);
            for (int i = 0; i < nowEvolvingLevel; i++) leftStars[i].SetActive(true);
            for (int i = 0; i < nextEvolvingLevel; i++) rightStars[i].SetActive(true);

            //Evolving Info Ui Update
            if (evolvingInfo.evolvingSprites[nextEvolvingLevel] != null)
            {
                skillImage.sprite = evolvingInfo.evolvingSprites[nowEvolvingLevel];
            }
            skillDesc.text = evolvingInfo.evolvingDesc[nowEvolvingLevel];
        }
        else
        {
            highestLevelSet.SetActive(true);
            mainSet.SetActive(false);
        }

        


    }
}
