using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GoldDungeonOverManager : MonoBehaviour
{
    public static GoldDungeonOverManager instance;

    [SerializeField] GameObject gameWinImage;
    [SerializeField] GameObject gameDefeatImage;

    [SerializeField] TextMeshProUGUI gameOverText;
    bool canExit = false;
    public bool isWin = false;

    [SerializeField] TextMeshProUGUI goldRewardText;
    [SerializeField] AudioClip gameLoseClip, gameWinClip;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    public void UpdateRewardUI()
    {
        goldRewardText.text = GoldDungeonManager.instance.getGold.ToString();
    }
    public IEnumerator GameOver()
    {
        Debug.Log("GameOVer");
        canExit = false;
        UpdateRewardUI();
        gameWinImage.transform.localScale = Vector3.zero;
        gameDefeatImage.transform.localScale = Vector3.zero;
        //GoldDungeonManager min이 10분일떄 추가
        if (isWin)
        {
            Debug.Log("Win");
            SoundManager.instance.PlaySound(gameWinClip);
            gameWinImage.transform.DOScale(Vector3.one, 0.5f);

            BackEndGameData.Instance.UserGameData.highestChapter = (GoldDungeonManager.instance.gameLevel * 2) - 1;
            Debug.Log(BackEndGameData.Instance.UserGameData.highestChapter);
        }
        else
        {
            Debug.Log("Lose");
            gameDefeatImage.transform.DOScale(Vector3.one, 0.5f);
            SoundManager.instance.PlaySound(gameLoseClip);
            if (GoldDungeonManager.instance.min / 5 == 1)
            {
                BackEndGameData.Instance.UserGameData.highestChapter =
                    (GoldDungeonManager.instance.gameLevel * 2) - 2;
            }
        }
        Debug.Log(BackEndGameData.Instance.UserGameData.highestChapter);
        BackEndGameData.Instance.UserGameData.gold += GoldDungeonManager.instance.getGold;
        BackEndGameData.Instance.GameDataUpdate();
        yield return new WaitForSeconds(2.0f);
        canExit = true;
        gameOverText.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void ExitLobbyOnClick()
    {
        Time.timeScale = 1.0f;
        if (canExit)
        {
            SceneManager.LoadScene("LobbyScene");
        }
    }
}
