using UnityEngine;
using TMPro;

public class InGameChapterName : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI chapterText;
    string chapterName;

    private void Start()
    {
        InitChapterName();
    }
    private void InitChapterName()
    {
        chapterName = ChangeScene.instance.chapterName;
        chapterText.text = chapterName;
    }


}
