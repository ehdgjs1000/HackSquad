using UnityEngine;
using BackEnd;
using TMPro;
using UnityEngine.UI;

public class BackEndManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        BackendSetUp();
    }
    private void Update()
    {
        if (Backend.IsInitialized)
        {
            //Backend.AsyncPoll();
        }
    }
    private void BackendSetUp()
    {
        var bro = Backend.Initialize();

        if (bro.IsSuccess())
        {
            Debug.Log("�ʱ�ȭ ����");
        }
        else
        {
            Debug.Log("�ʱ�ȭ ����");
        }
    }

}
