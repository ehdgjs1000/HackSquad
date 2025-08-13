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
            else //�α��� ����
            {
                loginBtn.interactable = true;
                string message = string.Empty;
                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 401: //�������� �ʴ� ����
                        message = callback.GetMessage().Contains("customId") ? "�������� �ʴ� ���̵� �Դϴ�." : "�߸��� ��й�ȣ �Դϴ�.";
                        break;
                    case 403: //����or����̽� ����
                        message = callback.GetMessage().Contains("user") ? "���ܴ��� �����Դϴ�" : "���ܴ��� ����̽� �Դϴ�.";
                        break;
                    case 410: //Ż�� ������
                        message = "Ż�� �������� �����Դϴ�.";
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
