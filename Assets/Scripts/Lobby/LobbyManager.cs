using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance;
    int gemAmount;
    int goldAmount;
    int eneryAmount;

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




}
