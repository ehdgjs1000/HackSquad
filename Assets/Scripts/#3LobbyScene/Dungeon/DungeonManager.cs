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


    [HideInInspector] public int remainGoldSweep;
    [HideInInspector] public int remainExpSweep;
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
    public void UpdateInfo()
    {
        if (PlayerPrefs.HasKey("remainGoldSweep")) remainGoldSweep = PlayerPrefs.GetInt("remainGoldSweep");
        else remainGoldSweep = 2;
        if (PlayerPrefs.HasKey("remainExpSweep")) remainExpSweep = PlayerPrefs.GetInt("remainExpSweep");
        else remainExpSweep = 2;
        highestGoldDungeon = PlayerPrefs.GetInt("highestGoldDungeon");
        highestExpDungeon = PlayerPrefs.GetInt("highestExpDungeon");
        
    }
    public void UpdateUI()
    {
        UpdateInfo();

        if (remainGoldSweep > 0) canGoldSweepImage.gameObject.SetActive(true);
        else canGoldSweepImage.gameObject.SetActive(false);
        if (remainExpSweep > 0) canExpSweepImage.gameObject.SetActive(true);
        else canExpSweepImage.gameObject.SetActive(false);

        remainGoldSweepText.text = $"¼ÒÅÁ °¡´É È½¼ö:{remainGoldSweep.ToString()}";
        remainExpSweepText.text = $"¼ÒÅÁ °¡´É È½¼ö:{remainExpSweep.ToString()}";



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
