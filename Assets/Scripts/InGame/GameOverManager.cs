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
        gameWinImage.transform.localScale = Vector3.zero;
        gameDefeatImage.transform.localScale = Vector3.zero;
        RewardUpdate();
        if (isWin)
        {
            gameWinImage.transform.DOScale(Vector3.one, 0.5f);
        }
        else
        {
            gameDefeatImage.transform.DOScale(Vector3.one, 0.5f);
        }

        yield return new WaitForSeconds(2.0f);
        canExit = true;
        gameOverText.gameObject.SetActive(true);
    }
    public void ExitLobbyOnClick()
    {
        SceneManager.LoadScene("LobbyScene");

    }
    private void RewardUpdate()
    {
        //º¸»ó ¶ç¿ì±â

    }
    public void SetReward()
    {

    }

}
