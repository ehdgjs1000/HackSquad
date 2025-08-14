using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemCount;

    public void SetUp(int _itemConut)
    {
        itemCount.text = _itemConut.ToString();

    }
}
