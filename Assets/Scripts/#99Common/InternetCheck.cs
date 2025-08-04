using UnityEngine;

public class InternetCheck : MonoBehaviour
{
    public static InternetCheck instance;

    float internetCheckTerm = 5.0f;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        internetCheckTerm -= Time.deltaTime;
        if(internetCheckTerm <= 0.0f) CheckInternet();
    }
    private void CheckInternet()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            // 인터넷 연결 없음

            Application.Quit();
        }
    }
}
