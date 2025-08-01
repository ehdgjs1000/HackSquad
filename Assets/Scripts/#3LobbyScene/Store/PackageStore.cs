using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PackageStore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI remainFreePackageText;
    [SerializeField] Image freePackageBG;

    private void Update()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        if (PlayerPrefs.GetInt("FreePackage") == 0)
        {
            remainFreePackageText.text = "1/1";
            freePackageBG.gameObject.SetActive(false);
        }else if (!PlayerPrefs.HasKey("FreePackage"))
        {
            remainFreePackageText.text = "1/1";
            freePackageBG.gameObject.SetActive(false);
        }
        else
        {
            remainFreePackageText.text = "0/1";
            freePackageBG.gameObject.SetActive(true);
        }
    }
    public void FreePackageOnClick()
    {
        if (PlayerPrefs.GetInt("FreePackage") == 1 || !PlayerPrefs.HasKey("FreePackage"))
        {
            BackEndGameData.Instance.UserGameData.gem += 50;
            PlayerPrefs.SetInt("FreePackage", 0);
            AdsVideo.instance.ShowVideo();
            BackEndGameData.Instance.GameDataUpdate();
        }
    }
}
