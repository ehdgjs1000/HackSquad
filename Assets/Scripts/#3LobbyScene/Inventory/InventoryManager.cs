using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public GameObject juiceItemPrefab; // �κ��丮 ������
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
        //�κ��丮 ������ ��� backend-inventory ������ UI Update
        //������ ���� ��������
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
