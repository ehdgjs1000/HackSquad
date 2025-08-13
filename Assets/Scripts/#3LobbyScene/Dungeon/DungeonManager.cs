using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager instance;

    [SerializeField] GameObject goldDungeonPanel;
    [SerializeField] GameObject expDungeonPanel;
    [SerializeField] Image canGoldSweepImage;
    [SerializeField] Image canExpSweepImage;
    [SerializeField] TextMeshProUGUI remainGoldSweepText;
    [SerializeField] TextMeshProUGUI remainExpSweepText;
    [SerializeField] TextMeshProUGUI[] remainTimeText;

    [HideInInspector] public int remainGoldDungeon;
    [HideInInspector] public int remainExpDungeon;
    [HideInInspector] public int highestGoldDungeon;
    [HideInInspector] public int highestExpDungeon;

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
        if (PlayerPrefs.HasKey("remainExpDungeonCount")) remainExpDungeon = PlayerPrefs.GetInt("remainExpDungeonCount");
        else remainExpDungeon = 2;
        highestGoldDungeon = PlayerPrefs.GetInt("highestGoldDungeon");
        highestExpDungeon = PlayerPrefs.GetInt("highestExpDungeon");
        
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
        if (remainGoldDungeon > 0) canExpSweepImage.gameObject.SetActive(true);
        else canExpSweepImage.gameObject.SetActive(false);

        remainGoldSweepText.text = $"플레이 가능 횟수:{remainGoldDungeon.ToString()}";
        remainExpSweepText.text = $"플레이 가능 횟수:{remainExpDungeon.ToString()}";
    }

    public void GoldDungeonOnClick()
    {
        goldDungeonPanel.SetActive(true);
        expDungeonPanel.SetActive(false);
        SoundManager.instance.BtnClickPlay();
    }
    public void ExpDungeonOnClick()
    {
        goldDungeonPanel.SetActive(false);
        expDungeonPanel.SetActive(true);
        SoundManager.instance.BtnClickPlay();
    }
    public void DungeonExitOnClick()
    {
        goldDungeonPanel.SetActive(false);
        expDungeonPanel.SetActive(false);
        SoundManager.instance.BtnClickPlay();
        this.gameObject.SetActive(false);

    }
     

}
