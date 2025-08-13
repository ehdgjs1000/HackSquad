using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoldSweepPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldAmonut;
    public void SetUp(int _Amount)
    {
        goldAmonut.text = _Amount.ToString();
    }
    public void ExitPanelOnClick()
    {
        this.gameObject.SetActive(false);
    }

}
