using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class IntroManager : MonoBehaviour
{
    [SerializeField] private Image progressImage;
    [SerializeField] TextMeshProUGUI progressTimeText;
    float progressTime = 1;

    bool isLoadingEnd = false;

    private void Awake()
    {
        SystemSetUp();
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
    }
    public void NextSceneOnClick()
    {
        if (isLoadingEnd)
        {
            SceneManager.LoadScene("LoginScene");
        }
    }

}
