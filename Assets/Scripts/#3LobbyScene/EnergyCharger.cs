using System.Collections;
using System;
using UnityEngine;
using TMPro;

public class EnergyCharger : MonoBehaviour
{
    public static EnergyCharger instance;
    [SerializeField] TextMeshProUGUI energyRefillTime; 

    DateTime saveExitTime;
    float remainChargingTime; //에너지 차징하다가 게임을 끄기 전 남은 time 
    float restTime = 2.0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if(restTime > 0.0f) restTime -= Time.deltaTime;
    }
    private void Start()
    {
        StartCoroutine(EnergyManagingStart());
    }
    IEnumerator EnergyManagingStart()
    {
        yield return new WaitForSeconds(1.5f);    
        //최대 에너지 보다 조금 있을 경우
        if (BackEndGameData.Instance.UserGameData.energy < (30 +
            BackEndGameData.Instance.UserAbilityData.abilityLevel[9]))
        {
            //에너지 차징 시작
            if (PlayerPrefs.HasKey("remainChargingTimeFloat"))
                remainChargingTime = PlayerPrefs.GetFloat("remainChargingTimeFloat");
            else PlayerPrefs.SetFloat("remainChargingTimeFloat", 0.0f);

            if (PlayerPrefs.HasKey("saveExitTime"))
            {
                var a = DateTime.Now - DateTime.ParseExact(PlayerPrefs.GetString("saveExitTime"), "yyyy-MM-dd HH:mm:ss", null);
                Debug.Log(a);
                remainChargingTime += a.Seconds;
                remainChargingTime += a.Minutes * 60;
                remainChargingTime += a.Hours * 3600;
            }
            StartCoroutine(EnergyCharging());
        }
        else
        {
            PlayerPrefs.SetFloat("remainChargingTimeFloat", 0.0f);
        }
    }
    IEnumerator EnergyCharging()
    {
        while (true)
        {
            //remainChargingTime += Time.deltaTime;
            remainChargingTime++;
            if(remainChargingTime >= 300.0f)
            {
                int addEnergy =  (int)(remainChargingTime / 300);
                float remainTime = remainChargingTime % 300;

                remainChargingTime -= 300.0f * addEnergy;
                remainChargingTime = remainTime;

                Debug.Log(addEnergy);
                BackEndGameData.Instance.UserGameData.energy += addEnergy;
                if (BackEndGameData.Instance.UserGameData.energy >= (30 +
            BackEndGameData.Instance.UserAbilityData.abilityLevel[9]))
                {
                    BackEndGameData.Instance.UserGameData.energy = 30 +
            BackEndGameData.Instance.UserAbilityData.abilityLevel[9];
                }
                BackEndGameData.Instance.GameDataUpdate();
                LobbyManager.instance.UpdateUIAll();
            }
            int remainMin = (int)((300.0f - remainChargingTime) / 60);
            float remainSec = (300.0f - remainChargingTime) % 60;
            energyRefillTime.text = remainMin + ":" + remainSec.ToString("F0");

            yield return new WaitForSeconds(1f);
        }
    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            PlayerPrefs.SetFloat("remainChargingTimeFloat", remainChargingTime);
            PlayerPrefs.SetString("saveExitTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }

}
