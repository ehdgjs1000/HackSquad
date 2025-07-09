using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager instance;

    [SerializeField] GameObject gameWinImage;
    [SerializeField] GameObject gameDefeatImage;

    [SerializeField] TextMeshProUGUI gameOverText;
    bool canExit = false;
    public bool isWin = false;

    private void Awake()
    {
        if(instance == null) instance = this;
    }
    public IEnumerator GameOver()
    {
        canExit = false;
        gameWinImage.transform.localScale = Vector3.zero;
        gameDefeatImage.transform.localScale = Vector3.zero;
        RewardUpdate();
        //gamemanager min이 10분일떄 추가
        if (isWin)
        {
            gameWinImage.transform.DOScale(Vector3.one, 0.5f);

            BackEndGameData.Instance.UserGameData.highestChapter = (GameManager.instance.gameLevel * 2) - 1;
        }
        else
        {
            gameDefeatImage.transform.DOScale(Vector3.one, 0.5f);
            if(GameManager.instance.min/5 == 1)
            {
                BackEndGameData.Instance.UserGameData.highestChapter = (GameManager.instance.gameLevel * 2) - -2;
            }
        }

        yield return new WaitForSeconds(2.0f);
        canExit = true;
        gameOverText.gameObject.SetActive(true);
    }
    public void ExitLobbyOnClick()
    {
        Time.timeScale = 1.0f;
        if (canExit)
        {
            BackEndGameData.Instance.UserGameData.gold += GameManager.instance.getGold;
            BackEndGameData.Instance.GameDataUpdate();
            SceneManager.LoadScene("LobbyScene");
        }
        
        
    }
    private void RewardUpdate()
    {
        //보상 띄우기

    }
    public void SetReward()
    {

    }

}
