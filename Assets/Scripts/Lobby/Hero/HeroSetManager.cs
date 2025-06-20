using UnityEngine;

public class HeroSetManager : MonoBehaviour
{
    public static HeroSetManager instance;

    [SerializeField] GameObject[] squadHeros; //��� �����忡 ��ϵ� �����
    [SerializeField] GameObject heroDetailPanel;

    public int squadCount = 0;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        UpdateSquad();
    }
    public void OpenHeroDetail()
    {
        heroDetailPanel.SetActive(true);
    }
    public void UpdateSquad() //��� ������ ���� update (squadCount��ŭ�� ���)
    {
        for (int a = 0; a < squadCount; a++)
        {

        }
    }

}
