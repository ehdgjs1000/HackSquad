using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdsVideo : MonoBehaviour
{
#if UNITY_EDITOR
    private string _adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-9187119445756720/4755421000";
#else
  private string _adUnitId = "unused";
#endif
    private RewardedAd _rewardedAd;
    private int rewardType;//#0 stemina //#1 refreshSkill

    public static AdsVideo instance;

    public float buttonClickTime = 0.0f;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }
    public void Start()
    {
        MobileAds.Initialize(initStatus => { });
    }
    private void Update()
    {
        if(buttonClickTime >= -1.0f) buttonClickTime -= Time.deltaTime;
    }
    public void EnergyRewardOnClick()
    {
        if (buttonClickTime <= 0.0f)
        {
            rewardType = 0;
            buttonClickTime = 3.0f;
            LoadRewardedAd();
        }
        else
        {
            PopUpMessageBase.instance.SetMessage("��� �� �ٽ� �����ּ���.");
        }
    }
    public void ShowVideo()
    {
        if (buttonClickTime <= 0.0f)
        {
            rewardType = 99;
            buttonClickTime = 3.0f;
            LoadRewardedAd();
        }
        else
        {
            PopUpMessageBase.instance.SetMessage("��� �� �ٽ� �����ּ���.");
        }
    }
    public void RefreshSkillsOnClick()
    {
        if (buttonClickTime <= 0.0f)
        {
            rewardType = 1;
            buttonClickTime = 20.0f;
            LoadRewardedAd();
        }
        else
        {
            PopUpMessageBase.instance.SetMessage("��� �� �ٽ� �����ּ���.");
        }
    }
    //������ ���� �ε�
    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");
        var adRequest = new AdRequest();
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                _rewardedAd = ad;

                BackEndGameData.Instance.UserQuestData.questProgress[8]++;
                ShowRewardedAd();
            });
    }

    //������ �ݹ����� ������ ���� ǥ��
    public void ShowRewardedAd()
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.

                if (rewardType == 0)
                {
                    Debug.Log("���� ���� :" + string.Format(rewardMsg, reward.Type, reward.Amount));
                    BackEndGameData.Instance.UserGameData.energy += 5;
                    BackEndGameData.Instance.GameDataUpdate();
                }
                else if (rewardType == 1)
                {
                    GameManager.instance.SkillChoose();
                }
                else
                {
                    Debug.Log("rewardType = 99");
                }
            });
        }
    }

    //������ ���� �̸� �ε�
    private void RegisterReloadHandler(RewardedAd ad)
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded Ad full screen content closed.");

            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
    }

    //������ ���� �̺�Ʈ ����
    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(string.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
        };
    }
}
