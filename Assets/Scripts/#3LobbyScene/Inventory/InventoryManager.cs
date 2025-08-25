using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public GameObject[] juiceItemPrefabs; // 인벤토리 프리팹
    public Transform gridParent;


    private void Awake()
    {
        if (instance == null) instance = this;

    }
    private void Start()
    {
        UpdateUI();
    }
    public void ClearGrid()
    {
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }
    }
    public void UpdateUI()
    {
        ClearGrid();
        //인벤토리 눌렀을 경우 backend-inventory 가져와 UI Update
        //아이템 갯수 가져오기
        int juiceItem1Count = BackEndGameData.Instance.UserInvenData.juiceLevel1ItemCount;
        int juiceItem2Count = BackEndGameData.Instance.UserInvenData.juiceLevel2ItemCount;
        int juiceItem3Count = BackEndGameData.Instance.UserInvenData.juiceLevel3ItemCount;
        int juiceItem4Count = BackEndGameData.Instance.UserInvenData.juiceLevel4ItemCount;

        if(juiceItem1Count > 0)
        {
            GameObject juice1Item = Instantiate(juiceItemPrefabs[0]);
            Inventory juiceInventory = juice1Item.GetComponent<Inventory>();
            juiceInventory.SetUp(juiceItem1Count);
            juiceInventory.transform.SetParent(gridParent, false);
        }
        if (juiceItem2Count > 0)
        {
            GameObject juice2Item = Instantiate(juiceItemPrefabs[1]);
            Inventory juiceInventory = juice2Item.GetComponent<Inventory>();
            juiceInventory.SetUp(juiceItem2Count);
            juiceInventory.transform.SetParent(gridParent, false);
        }
        if (juiceItem3Count > 0)
        {
            GameObject juice3Item = Instantiate(juiceItemPrefabs[2]);
            Inventory juiceInventory = juice3Item.GetComponent<Inventory>();
            juiceInventory.SetUp(juiceItem3Count);
            juiceInventory.transform.SetParent(gridParent, false);
        }
        if (juiceItem4Count > 0)
        {
            GameObject juice4Item = Instantiate(juiceItemPrefabs[3]);
            Inventory juiceInventory = juice4Item.GetComponent<Inventory>();
            juiceInventory.SetUp(juiceItem4Count);
            juiceInventory.transform.SetParent(gridParent, false);
        }


    }
}
