using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class EnergyBuy : MonoBehaviour
{
    [SerializeField] GameObject buyCheckGO;
    [SerializeField] GameObject buyGO;
    public void BuyEnergyOnClick(int energyAmount)
    {
        //���� ��û �� ���� ����
        if (energyAmount == 5)
        {

            BackEndGameData.Instance.UserGameData.energy += 5;
            BackEndGameData.Instance.GameDataUpdate();
            LobbyManager.instance.UpdateUIAll();
        }
        else if(energyAmount == 30)
        {
            if(BackEndGameData.Instance.UserGameData.gem >= 80)
            {
                BackEndGameData.Instance.UserGameData.gem -= 80;
                BackEndGameData.Instance.UserGameData.energy += 30;
                BackEndGameData.Instance.GameDataUpdate();
                LobbyManager.instance.UpdateUIAll();
                ConfirmBuy();
            }
            else
            {
                PopUpMessageBase.instance.SetMessage("���̾ư� �����մϴ�.");
                SoundManager.instance.ErrorClipPlay();
            }
        }
    }

    private void ConfirmBuy()
    {
        buyCheckGO.SetActive(true);
    }

}
