using UnityEngine;
using DG.Tweening;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance;
    int gemAmount;
    int goldAmount;
    int eneryAmount;

    [SerializeField] GameObject[] chapterSet;

    private void Awake()
    {
        if (instance == null) instance = this;

        InitData();
    }

    /// <summary>
    /// return 0 = gem  1 = gold  2 = energy
    /// </summary>
    /// <returns></returns>
    public int ReturnAccount(int _type)
    {
        if (_type == 0) return gemAmount;
        else if (_type == 1) return goldAmount;
        else if (_type == 2) return eneryAmount;
        else return 0;
    }
    private void InitData()
    {
        //추후 서버와 연동
        gemAmount = 10000;
        goldAmount = 10000;
        eneryAmount = 10000;
    }
    public void ChapterBtnClick(int _num)
    {
        for (int a = 0; a < chapterSet.Length; a++)
        {
            if (a == _num) chapterSet[a].transform.DOScale(new Vector3(1,1,1),0.0f);
            else chapterSet[a].transform.DOScale(new Vector3(0,0,0), 0.0f);
        }
    }



}
