using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
    public Button cafeBtn;
    public string url = "https://cafe.naver.com/hacksquad";

    void Start()
    {
        cafeBtn.onClick.AddListener(OpenLink);
    }

    void OpenLink()
    {
        Application.OpenURL(url);
    }

}
