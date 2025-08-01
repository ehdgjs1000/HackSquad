using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class InGameSetting : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldAmountText;
    [SerializeField] TextMeshProUGUI expAmountText;

    public void UpdateUI()
    {
        goldAmountText.text = GameManager.instance.getGold.ToString();
        expAmountText.text = GameManager.instance.userExp.ToString();
    }
    public void ExitOnClick()
    {
        Time.timeScale = 1.0f;
        SoundManager.instance.ErrorClipPlay();
        this.gameObject.SetActive(false);
        SceneManager.LoadScene("LobbyScene");
    }
    public void ResumeGameOnClick()
    {
        Time.timeScale = 1.0f;
        SoundManager.instance.BtnClickPlay();
        this.gameObject.SetActive(false);
    }

}
