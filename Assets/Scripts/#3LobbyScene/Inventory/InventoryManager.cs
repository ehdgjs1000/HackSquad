using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public GameObject juiceItemPrefab; // 인벤토리 프리팹
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
        int juiceItemCount = BackEndGameData.Instance.UserInvenData.juiceItemCount;


        if(juiceItemCount > 0)
        {
            GameObject juiceItem = Instantiate(juiceItemPrefab);
            Inventory juiceInventory = juiceItem.GetComponent<Inventory>();
            juiceInventory.SetUp(juiceItemCount);

            juiceInventory.transform.SetParent(gridParent, false);
        }
        

    }
}
