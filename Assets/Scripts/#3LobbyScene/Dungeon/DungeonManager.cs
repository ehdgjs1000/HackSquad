using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] GameObject goldDungeonPanel;
    [SerializeField] GameObject expDungeonPanel;


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
