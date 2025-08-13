using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using BackEnd;
public class IntroManager : MonoBehaviour
{
    [SerializeField] private Image progressImage;
    [SerializeField] TextMeshProUGUI progressTimeText;
    [SerializeField] Button loginBtn;
    [SerializeField] GameObject progressGo;
    [SerializeField] Material sceneTransitionMat;
    float progressTime = 1;

    bool isLoadingEnd = false;

    private void Awake()
    {
        SystemSetUp();
    }
    private void Start()
    {
        sceneTransitionMat.SetFloat("_Progress",1);
    }
    private void SystemSetUp()
    {
        Application.runInBackground = true;
        int width = Screen.width;
        int height = (int)(Screen.width * 16.0f/9);

        Screen.SetResolution(width,height,true);

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Play();
    }
    public void Play(UnityAction action = null)
    {
        StartCoroutine(OnProgress(action));
    }
    IEnumerator OnProgress(UnityAction action)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / progressTime;

            if (percent * 100 >= 100) percent = 1;
            progressTimeText.text = $"Loading... {percent*100:F0}%";
            progressImage.fillAmount = Mathf.Lerp(0,1,percent);
            isLoadingEnd = true;
            yield return null;
        }
        action?.Invoke();
        LoginBtnActive();
    }
    public void NextSceneOnClick()
    {
        if (isLoadingEnd)
        {
            SceneManager.LoadScene("LoginScene");
        }
    }
    public void LoginBtnOnClick()
    {
        MainTainLogIn();
    }
    private void LoginBtnActive()
    {
        progressGo.SetActive(false);
        loginBtn.gameObject.SetActive(true);
    }
    public void MainTainLogIn()
    {
        string mainTainID = PlayerPrefs.GetString("ID");
        string mainTainPW = PlayerPrefs.GetString("PW");
        if (mainTainID != null && mainTainPW != null) ResponceToLogin(mainTainID, mainTainPW);
    }
    private void ResponceToLogin(string ID, string PW)
    {
        Backend.BMember.CustomLogin(ID, PW, callback =>
        {
            if (callback.IsSuccess())
            {
                PlayerPrefs.SetString("ID", ID);
                PlayerPrefs.SetString("PW", PW);

                SceneManager.LoadScene("LobbyScene");
            }
            else //로그인 실패
            {
                loginBtn.interactable = true;
                string message = string.Empty;
                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 401: //존재하지 않는 계정
                        message = callback.GetMessage().Contains("customId") ? "존재하지 않는 아이디 입니다." : "잘못된 비밀번호 입니다.";
                        break;
                    case 403: //유저or디바이스 차단
                        message = callback.GetMessage().Contains("user") ? "차단당한 유저입니다" : "차단당한 디바이스 입니다.";
                        break;
                    case 410: //탈퇴 진행중
                        message = "탈퇴가 진행중인 유저입니다.";
                        break;
                    default:
                        message = callback.GetMessage();
                        break;
                }
                SceneManager.LoadScene("LoginScene");

            }
        });
    }

}
