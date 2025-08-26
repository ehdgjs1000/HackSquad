using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager instance;

    [SerializeField] GameObject goldDungeonPanel;
    [SerializeField] GameObject evolvingDungeonPanel;
    [SerializeField] Image canGoldSweepImage;
    [SerializeField] Image canEvolvingSweepImage;
    [SerializeField] TextMeshProUGUI remainGoldSweepText;
    [SerializeField] TextMeshProUGUI remainEvolvingSweepText;
    [SerializeField] TextMeshProUGUI[] remainTimeText;

    [HideInInspector] public int remainGoldDungeon;
    [HideInInspector] public int remainEvolvingDungeon;
    [HideInInspector] public int highestGoldDungeon;
    [HideInInspector] public int highestEvolvingDungeon;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        UpdateUI();
    }
    private void Update()
    {
        UpdateUI();
    }
    public void UpdateInfo()
    {
        if (PlayerPrefs.HasKey("remainGoldDungeonCount")) remainGoldDungeon = PlayerPrefs.GetInt("remainGoldDungeonCount");
        else remainGoldDungeon = 2;
        if (PlayerPrefs.HasKey("remainEvolvingDungeonCount")) remainEvolvingDungeon = PlayerPrefs.GetInt("remainEvolvingDungeonCount");
        else remainEvolvingDungeon = 2;
        highestGoldDungeon = PlayerPrefs.GetInt("highestGoldDungeon");
        highestEvolvingDungeon = PlayerPrefs.GetInt("highestEvolvingDungeon");
        
    }
    public void UpdateUI()
    {
        UpdateInfo();
        foreach (TextMeshProUGUI textGO in remainTimeText)
        {
            textGO.text = TimeManager.instance.dailyStr;
        }

        if (remainGoldDungeon > 0) canGoldSweepImage.gameObject.SetActive(true);
        else canGoldSweepImage.gameObject.SetActive(false);
        if (remainEvolvingDungeon > 0) canEvolvingSweepImage.gameObject.SetActive(true);
        else canEvolvingSweepImage.gameObject.SetActive(false);

        remainGoldSweepText.text = $"플레이 가능 횟수:{remainGoldDungeon.ToString()}";
        remainEvolvingSweepText.text = $"플레이 가능 횟수:{remainEvolvingDungeon.ToString()}";
    }

    public void GoldDungeonOnClick()
    {
        goldDungeonPanel.SetActive(true);
        evolvingDungeonPanel.SetActive(false);
        SoundManager.instance.BtnClickPlay();
    }
    public void EvolvingDungeonOnClick()
    {
        goldDungeonPanel.SetActive(false);
        evolvingDungeonPanel.SetActive(true);
        SoundManager.instance.BtnClickPlay();
    }
    public void DungeonExitOnClick()
    {
        goldDungeonPanel.SetActive(false);
        evolvingDungeonPanel.SetActive(false);
        SoundManager.instance.BtnClickPlay();
        this.gameObject.SetActive(false);

    }
     

}
