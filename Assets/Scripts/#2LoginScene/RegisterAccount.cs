using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
using BackEnd;
using System.Text.RegularExpressions;


public class RegisterAccount : LoginBase
{
    [SerializeField] private Image imageID;
    [SerializeField] private TMP_InputField inputFieldID;
    [SerializeField] private Image imagePW;
    [SerializeField] private TMP_InputField inputFieldPW;
    [SerializeField] private Image imageConfirmPW;
    [SerializeField] private TMP_InputField inputFieldConfirmPW;
    [SerializeField] private Image imageEmail;
    [SerializeField] private TMP_InputField inputFieldEmail;

    [SerializeField] private Button btnRegisterAccount;
    private void Awake()
    {
        inputFieldID.onValueChanged.AddListener(
     (word) => inputFieldID.text = Regex.Replace(word, @"[^0-9a-zA-Z]", ""));
        inputFieldPW.onValueChanged.AddListener(
     (word) => inputFieldPW.text = Regex.Replace(word, @"[^0-9a-zA-Z]", ""));

    }
    public void RegisterBtnOnClick()
    {
        ResetUI(imageID, imagePW, imageConfirmPW, imageEmail);
        string weaponInitFilePath = Application.persistentDataPath + "/WeaponInitData.json";
        File.WriteAllText(weaponInitFilePath, null);

        if (IsFieldDataEmpty(imageID, inputFieldID.text, "���̵�")) return;
        if (IsFieldDataEmpty(imagePW, inputFieldPW.text, "�н�����")) return;
        if (IsFieldDataEmpty(imageConfirmPW, inputFieldConfirmPW.text, "�н����� Ȯ��")) return;
        if (IsFieldDataEmpty(imageEmail, inputFieldEmail.text, "�����ּ�")) return;

        //��й�ȣ�� Ȯ���� ��ȣ�� �ٸ���
        if (!inputFieldPW.text.Equals(inputFieldConfirmPW.text))
        {
            GuidForIncorrectlyEnteredData(imageConfirmPW, "��й�ȣ�� ��ġ���� �ʽ��ϴ�.");
            return;
        }
        if (!inputFieldEmail.text.Contains("@"))
        {
            GuidForIncorrectlyEnteredData(imageEmail, "���� ������ �߸��Ǿ����ϴ�. (ex. temp@xx.xx)");
            return;
        }
        btnRegisterAccount.interactable = false;
        SetMessage("���� �������Դϴ�...");

        CustomSignUp();
    }
    private void CustomSignUp()
    {
        Backend.BMember.CustomSignUp(inputFieldID.text, inputFieldPW.text, callback =>
        {
            btnRegisterAccount.interactable = true;

            if (callback.IsSuccess())
            {
                Backend.BMember.UpdateCustomEmail(inputFieldEmail.text, callback =>
                {
                    SetMessage($"���� ���� ����. {inputFieldID.text}�� ȯ���մϴ�.");

                    //���� ������ �������� ?? �ش� ������ ���� ���� ����
                    BackEndGameData.Instance.GameDataInsert();
                    PlayerPrefs.SetString("ID", inputFieldID.text);
                    PlayerPrefs.SetString("PW", inputFieldPW.text);

                    /*PlayerPrefs.SetInt("QUTCDay", DateTime.UtcNow.Day);
                    PlayerPrefs.SetInt("QUTCMonth", DateTime.UtcNow.Month);
                    PlayerPrefs.SetInt("QUTCYear", DateTime.UtcNow.Year);
                    PlayerPrefs.SetInt("IsSkipTutorial", 0);*/
                    BackEndGameData.Instance.ResetPlayerPrefabData();
                    SceneManager.LoadScene("LobbyScene");
                });
            }
            else
            {
                string message = string.Empty;
                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 409:
                        message = "�̹� �����ϴ� ���̵��Դϴ�.";
                        break;
                    case 403:
                    case 401:
                    case 400:
                    default:
                        message = callback.GetMessage();
                        break;
                }
                if (message.Contains("���̵�"))
                {
                    GuidForIncorrectlyEnteredData(imageID, message);
                }
                else
                {
                    SetMessage(message);
                }
            }

        });
    }




}