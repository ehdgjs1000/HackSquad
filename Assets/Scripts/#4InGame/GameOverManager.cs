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
    int killMonsterCount = 0;

    private void Awake()
    {
        if(instance == null) instance = this;
    }
    public IEnumerator GameOver()
    {
        Debug.Log("GameOVer");
        canExit = false;
        BackEndGameData.Instance.UserQuestData.questProgress[6] += GameManager.instance.killMonsterCount;
        gameWinImage.transform.localScale = Vector3.zero;
        gameDefeatImage.transform.localScale = Vector3.zero;
        //gamemanager min이 10분일떄 추가
        if (isWin)
        {
            gameWinImage.transform.DOScale(Vector3.one, 0.5f);

            Debug.Log(GameManager.instance.gameLevel);
            BackEndGameData.Instance.UserGameData.highestChapter = (GameManager.instance.gameLevel * 2) - 1;
            Debug.Log(BackEndGameData.Instance.UserGameData.highestChapter);
        }
        else
        {
            
            gameDefeatImage.transform.DOScale(Vector3.one, 0.5f);
            if(GameManager.instance.min/5 == 1)
            {
                Debug.Log(GameManager.instance.gameLevel);
                BackEndGameData.Instance.UserGameData.highestChapter = 
                    (GameManager.instance.gameLevel * 2) -2;
                Debug.Log(BackEndGameData.Instance.UserGameData.highestChapter);
            }
            
        }
        BackEndGameData.Instance.UserGameData.gold += GameManager.instance.getGold;
        BackEndGameData.Instance.UserGameData.exp += GameManager.instance.userExp;
        BackEndGameData.Instance.GameDataUpdate();
        yield return new WaitForSeconds(2.0f);
        canExit = true;
        gameOverText.gameObject.SetActive(true);
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
