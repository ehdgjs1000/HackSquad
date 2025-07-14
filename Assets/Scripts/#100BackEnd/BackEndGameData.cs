using UnityEngine;
using BackEnd;
using UnityEngine.Events;

public class BackEndGameData
{
    [System.Serializable]
    public class GameDataLoadEvent : UnityEngine.Events.UnityEvent { }
    public GameDataLoadEvent onGameDataLoadEvent = new GameDataLoadEvent();

    private static BackEndGameData instance = null;
    public static BackEndGameData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BackEndGameData();
            }
            return instance;
        }
    }
    
    private UserGameData userGameData = new UserGameData();
    public UserGameData UserGameData => userGameData;
    private string gameUserDataRawInData = string.Empty;

    private UserHeroData userHeroData = new UserHeroData();
    public UserHeroData UserHeroData => userHeroData;
    private string gameHeroDataRawInData = string.Empty;

    private UserQuestData userQuestData = new UserQuestData();
    public UserQuestData UserQuestData => userQuestData;
    private string gameQuestDataRawInData = string.Empty;

    private UserAbilityData userAbilityData = new UserAbilityData();
    public UserAbilityData UserAbilityData => userAbilityData;
    private string gameAbilityDataRawInData = string.Empty;
    
    /// <summary>
    /// �ڳ� �ܼ� ���̺� ���ο� ���� ���� �߰�
    /// </summary>
    public void GameDataInsert()
    {
        GameUserDataInsert();
        GameHeroDataInsert();
        GameQuestDataInsert();
        GameAbilityDataInsert();
    }
    public void GameUserDataInsert()
    {
        userGameData.Reset();

        Param param = new Param()
        {
            {"level", userGameData.level },
            {"exp", userGameData.exp },
            {"gold", userGameData.gold },
            {"gem", userGameData.gem },
            {"energy", userGameData.energy },
            {"highestChapter", userGameData.highestChapter },
            {"highestRewardChapter", userGameData.highestRewardChapter },
        };

        Backend.GameData.Insert("USER_DATA", param, callback =>
        {
            if (callback.IsSuccess())
            {
                gameUserDataRawInData = callback.GetInDate();
            }
            else
            {
                Debug.LogError("�������� ���Կ� �����߽��ϴ�.");
            }
        });
    }
    public void GameHeroDataInsert()
    {
        userHeroData.Reset();

        Param param = new Param()
        {
            {"Hero1Level", userHeroData.heroLevel[0] },
            {"Hero2Level", userHeroData.heroLevel[1] },
            {"Hero3Level", userHeroData.heroLevel[2] },
            {"Hero4Level", userHeroData.heroLevel[3] },
            {"Hero5Level", userHeroData.heroLevel[4] },
            {"Hero6Level", userHeroData.heroLevel[5] },
            {"Hero7Level", userHeroData.heroLevel[6] },
            {"Hero8Level", userHeroData.heroLevel[7] },
            {"Hero9Level", userHeroData.heroLevel[8] },
            {"Hero10Level", userHeroData.heroLevel[9] },
            {"Hero11Level", userHeroData.heroLevel[10] },
            {"Hero12Level", userHeroData.heroLevel[11] },

            {"Hero1Count", userHeroData.heroCount[0] },
            {"Hero2Count", userHeroData.heroCount[1] },
            {"Hero3Count", userHeroData.heroCount[2] },
            {"Hero4Count", userHeroData.heroCount[3] },
            {"Hero5Count", userHeroData.heroCount[4] },
            {"Hero6Count", userHeroData.heroCount[5] },
            {"Hero7Count", userHeroData.heroCount[6] },
            {"Hero8Count", userHeroData.heroCount[7] },
            {"Hero9Count", userHeroData.heroCount[8] },
            {"Hero10Count", userHeroData.heroCount[9] },
            {"Hero11Count", userHeroData.heroCount[10] },
            {"Hero12Count", userHeroData.heroCount[11] },

            {"HeroChoose1", userHeroData.heroChooseNum[0]},
            {"HeroChoose2", userHeroData.heroChooseNum[1]},
            {"HeroChoose3", userHeroData.heroChooseNum[2]},
            {"HeroChoose4", userHeroData.heroChooseNum[3]},
        };

        Backend.GameData.Insert("HERO_DATA", param, callback =>
        {
            if (callback.IsSuccess())
            {
                gameHeroDataRawInData = callback.GetInDate();
            }
            else
            {
                Debug.LogError("�������� ���Կ� �����߽��ϴ�.");
            }
        });

    }
    public void GameQuestDataInsert()
    {
        userQuestData.Reset();

        Param param = new Param()
        {
            {"dailyClearAmount", userQuestData.dailyClearAmount },
            {"weeklyClearAmount", userQuestData.weeklyClearAmount },

            {"quest1Progress", userQuestData.questProgress[0] },
            {"quest2Progress", userQuestData.questProgress[1] },
            {"quest3Progress", userQuestData.questProgress[2] },
            {"quest4Progress", userQuestData.questProgress[3] },
            {"quest5Progress", userQuestData.questProgress[4] },
            {"quest6Progress", userQuestData.questProgress[5] },
            {"quest7Progress", userQuestData.questProgress[6] },
            {"quest8Progress", userQuestData.questProgress[7] },
            {"quest9Progress", userQuestData.questProgress[8] },
            {"quest10Progress", userQuestData.questProgress[9] },

            {"daily1Cleared", userQuestData.dailyCleared[0] },
            {"daily2Cleared", userQuestData.dailyCleared[1] },
            {"daily3Cleared", userQuestData.dailyCleared[2] },
            {"daily4Cleared", userQuestData.dailyCleared[3] },
            {"daily5Cleared", userQuestData.dailyCleared[4] },
            {"daily6Cleared", userQuestData.dailyCleared[5] },
            {"daily7Cleared", userQuestData.dailyCleared[6] },
            {"daily8Cleared", userQuestData.dailyCleared[7] },
            {"daily9Cleared", userQuestData.dailyCleared[8] },
            {"daily10Cleared", userQuestData.dailyCleared[9] },

            {"dailyReward1Recieved", userQuestData.dailyRewardRecieved[0] },
            {"dailyReward2Recieved", userQuestData.dailyRewardRecieved[1] },
            {"dailyReward3Recieved", userQuestData.dailyRewardRecieved[2] },
            {"dailyReward4Recieved", userQuestData.dailyRewardRecieved[3] },
            {"dailyReward5Recieved", userQuestData.dailyRewardRecieved[4] },

            {"weeklyReward1Recieved", userQuestData.weeklyRewardRecieved[0] },
            {"weeklyReward2Recieved", userQuestData.weeklyRewardRecieved[1] },
            {"weeklyReward3Recieved", userQuestData.weeklyRewardRecieved[2] },
            {"weeklyReward4Recieved", userQuestData.weeklyRewardRecieved[3] },
            {"weeklyReward5Recieved", userQuestData.weeklyRewardRecieved[4] },
        };

        Backend.GameData.Insert("QUEST_DATA", param, callback =>
        {
            if (callback.IsSuccess())
            {
                gameQuestDataRawInData = callback.GetInDate();
            }
            else
            {
                Debug.LogError("����Ʈ���� ���Կ� �����߽��ϴ�.");
            }
        });
    }
    public void GameAbilityDataInsert()
    {
        userAbilityData.Reset();

        Param param = new Param()
        {
            {"ability0Level", userAbilityData.abilityLevel[0]},
            {"ability1Level", userAbilityData.abilityLevel[1]},
            {"ability2Level", userAbilityData.abilityLevel[2]},
            {"ability3Level", userAbilityData.abilityLevel[3]},
            {"ability4Level", userAbilityData.abilityLevel[4]},
            {"ability5Level", userAbilityData.abilityLevel[5]},
            {"ability6Level", userAbilityData.abilityLevel[6]},
            {"ability7Level", userAbilityData.abilityLevel[7]},
            {"ability8Level", userAbilityData.abilityLevel[8]},
            {"ability9Level", userAbilityData.abilityLevel[9]},
            {"ability10Level", userAbilityData.abilityLevel[10]},
            {"ability11Level", userAbilityData.abilityLevel[11]},

        };

        Backend.GameData.Insert("ABILITY_DATA", param, callback =>
        {
            if (callback.IsSuccess())
            {
                gameAbilityDataRawInData = callback.GetInDate();
            }
            else
            {
                Debug.LogError("�����Ƽ ���� ���Կ� �����߽��ϴ�.");
            }
        });
    }

    /// <summary>
    /// �ڳ� �ݼ� ���̺��� ���� ������ �ҷ��Ë� ȣ��
    /// </summary>
    public void GameDataLoad()
    {
        GameUserDataLoad();
        GameHeroDataLoad();
        GameQuestDataLoad();
        GameAbilityDataLoad();
        Debug.Log("GameDataLoad EXP : " + UserGameData.exp);
    }
    public void GameUserDataLoad()
    {
        Backend.GameData.GetMyData("USER_DATA", new Where(), callback =>
        {
            if (callback.IsSuccess())
            {
                //Debug.Log($"���� ���� ������ �ҷ����⿡ �����߽��ϴ�. : {callback}");

                try
                {
                    LitJson.JsonData gameDataJson = callback.FlattenRows();

                    if (gameDataJson.Count <= 0)
                    {
                        Debug.LogWarning("�����Ͱ� �������� �ʽ��ϴ�.");
                    }
                    else
                    {
                        //�ҷ��� ���� ������ ���� ��
                        gameUserDataRawInData = gameDataJson[0]["inDate"].ToString();
                        //�ҷ��� ���� ������ userGameData ������ ����
                        userGameData.level = int.Parse(gameDataJson[0]["level"].ToString());
                        userGameData.exp = float.Parse(gameDataJson[0]["exp"].ToString());
                        userGameData.gold = int.Parse(gameDataJson[0]["gold"].ToString());
                        userGameData.gem = int.Parse(gameDataJson[0]["gem"].ToString());
                        userGameData.energy = int.Parse(gameDataJson[0]["energy"].ToString());
                        userGameData.highestChapter = float.Parse(gameDataJson[0]["highestChapter"].ToString());
                        userGameData.highestRewardChapter = float.Parse(gameDataJson[0]["highestRewardChapter"].ToString());

                        onGameDataLoadEvent?.Invoke();
                    }
                }
                catch (System.Exception e)
                {
                    userGameData.Reset();
                    Debug.LogError(e);
                }
            }
            else // �������� ��
            {
                Debug.LogError($"���� ���� ������ �ҷ����⿡ �����߽��ϴ�. : {callback}");
            }


        });
    }
    public void GameHeroDataLoad()
    {
        Backend.GameData.GetMyData("HERO_DATA", new Where(), callback =>
        {
            if (callback.IsSuccess())
            {
                //Debug.Log($"���� ���� ������ �ҷ����⿡ �����߽��ϴ�. : {callback}");

                try
                {
                    LitJson.JsonData gameHeroDataJson = callback.FlattenRows();

                    if (gameHeroDataJson.Count <= 0)
                    {
                        Debug.LogWarning("�����Ͱ� �������� �ʽ��ϴ�.");
                    }
                    else
                    {
                        //�ҷ��� ���� ������ ���� ��
                        gameHeroDataRawInData = gameHeroDataJson[0]["inDate"].ToString();
                        //�ҷ��� ���� ������ userGameData ������ ����
                        userHeroData.heroLevel[0] = int.Parse(gameHeroDataJson[0]["Hero1Level"].ToString());
                        userHeroData.heroLevel[1] = int.Parse(gameHeroDataJson[0]["Hero2Level"].ToString());
                        userHeroData.heroLevel[2] = int.Parse(gameHeroDataJson[0]["Hero3Level"].ToString());
                        userHeroData.heroLevel[3] = int.Parse(gameHeroDataJson[0]["Hero4Level"].ToString());
                        userHeroData.heroLevel[4] = int.Parse(gameHeroDataJson[0]["Hero5Level"].ToString());
                        userHeroData.heroLevel[5] = int.Parse(gameHeroDataJson[0]["Hero6Level"].ToString());
                        userHeroData.heroLevel[6] = int.Parse(gameHeroDataJson[0]["Hero7Level"].ToString());
                        userHeroData.heroLevel[7] = int.Parse(gameHeroDataJson[0]["Hero8Level"].ToString());
                        userHeroData.heroLevel[8] = int.Parse(gameHeroDataJson[0]["Hero9Level"].ToString());
                        userHeroData.heroLevel[9] = int.Parse(gameHeroDataJson[0]["Hero10Level"].ToString());
                        userHeroData.heroLevel[10] = int.Parse(gameHeroDataJson[0]["Hero11Level"].ToString());
                        userHeroData.heroLevel[11] = int.Parse(gameHeroDataJson[0]["Hero12Level"].ToString());

                        userHeroData.heroCount[0] = int.Parse(gameHeroDataJson[0]["Hero1Count"].ToString());
                        userHeroData.heroCount[1] = int.Parse(gameHeroDataJson[0]["Hero2Count"].ToString());
                        userHeroData.heroCount[2] = int.Parse(gameHeroDataJson[0]["Hero3Count"].ToString());
                        userHeroData.heroCount[3] = int.Parse(gameHeroDataJson[0]["Hero4Count"].ToString());
                        userHeroData.heroCount[4] = int.Parse(gameHeroDataJson[0]["Hero5Count"].ToString());
                        userHeroData.heroCount[5] = int.Parse(gameHeroDataJson[0]["Hero6Count"].ToString());
                        userHeroData.heroCount[6] = int.Parse(gameHeroDataJson[0]["Hero7Count"].ToString());
                        userHeroData.heroCount[7] = int.Parse(gameHeroDataJson[0]["Hero8Count"].ToString());
                        userHeroData.heroCount[8] = int.Parse(gameHeroDataJson[0]["Hero9Count"].ToString());
                        userHeroData.heroCount[9] = int.Parse(gameHeroDataJson[0]["Hero10Count"].ToString());
                        userHeroData.heroCount[10] = int.Parse(gameHeroDataJson[0]["Hero11Count"].ToString());
                        userHeroData.heroCount[11] = int.Parse(gameHeroDataJson[0]["Hero12Count"].ToString());

                        userHeroData.heroChooseNum[0] = int.Parse(gameHeroDataJson[0]["HeroChoose1"].ToString());
                        userHeroData.heroChooseNum[1] = int.Parse(gameHeroDataJson[0]["HeroChoose2"].ToString());
                        userHeroData.heroChooseNum[2] = int.Parse(gameHeroDataJson[0]["HeroChoose3"].ToString());
                        userHeroData.heroChooseNum[3] = int.Parse(gameHeroDataJson[0]["HeroChoose4"].ToString());
                    }
                }
                catch (System.Exception e)
                {
                    userHeroData.Reset();
                    Debug.LogError(e);
                }
            }
            else // �������� ��
            {
                Debug.LogError($"���� ���� ������ �ҷ����⿡ �����߽��ϴ�. : {callback}");
            }


        });
    }
    public void GameQuestDataLoad()
    {
        Backend.GameData.GetMyData("QUEST_DATA", new Where(), callback =>
        {
            if (callback.IsSuccess())
            {
                //Debug.Log($"���� ���� ������ �ҷ����⿡ �����߽��ϴ�. : {callback}");

                try
                {
                    LitJson.JsonData gameQuestDataJson = callback.FlattenRows();

                    if (gameQuestDataJson.Count <= 0)
                    {
                        Debug.LogWarning("�����Ͱ� �������� �ʽ��ϴ�.");
                    }
                    else
                    {
                        //�ҷ��� ���� ������ ���� ��
                        gameQuestDataRawInData = gameQuestDataJson[0]["inDate"].ToString();

                        //�ҷ��� ���� ������ userGameData ������ ����
                        userQuestData.dailyClearAmount = float.Parse(gameQuestDataJson[0]["dailyClearAmount"].ToString());
                        userQuestData.weeklyClearAmount = float.Parse(gameQuestDataJson[0]["weeklyClearAmount"].ToString());

                        userQuestData.questProgress[0] = float.Parse(gameQuestDataJson[0]["quest1Progress"].ToString());
                        userQuestData.questProgress[1] = float.Parse(gameQuestDataJson[0]["quest2Progress"].ToString());
                        userQuestData.questProgress[2] = float.Parse(gameQuestDataJson[0]["quest3Progress"].ToString());
                        userQuestData.questProgress[3] = float.Parse(gameQuestDataJson[0]["quest4Progress"].ToString());
                        userQuestData.questProgress[4] = float.Parse(gameQuestDataJson[0]["quest5Progress"].ToString());
                        userQuestData.questProgress[5] = float.Parse(gameQuestDataJson[0]["quest6Progress"].ToString());
                        userQuestData.questProgress[6] = float.Parse(gameQuestDataJson[0]["quest7Progress"].ToString());
                        userQuestData.questProgress[7] = float.Parse(gameQuestDataJson[0]["quest8Progress"].ToString());
                        userQuestData.questProgress[8] = float.Parse(gameQuestDataJson[0]["quest9Progress"].ToString());
                        userQuestData.questProgress[9] = float.Parse(gameQuestDataJson[0]["quest10Progress"].ToString());

                        userQuestData.dailyCleared[0] = bool.Parse(gameQuestDataJson[0]["daily1Cleared"].ToString());
                        userQuestData.dailyCleared[1] = bool.Parse(gameQuestDataJson[0]["daily2Cleared"].ToString());
                        userQuestData.dailyCleared[2] = bool.Parse(gameQuestDataJson[0]["daily3Cleared"].ToString());
                        userQuestData.dailyCleared[3] = bool.Parse(gameQuestDataJson[0]["daily4Cleared"].ToString());
                        userQuestData.dailyCleared[4] = bool.Parse(gameQuestDataJson[0]["daily5Cleared"].ToString());
                        userQuestData.dailyCleared[5] = bool.Parse(gameQuestDataJson[0]["daily6Cleared"].ToString());
                        userQuestData.dailyCleared[6] = bool.Parse(gameQuestDataJson[0]["daily7Cleared"].ToString());
                        userQuestData.dailyCleared[7] = bool.Parse(gameQuestDataJson[0]["daily8Cleared"].ToString());
                        userQuestData.dailyCleared[8] = bool.Parse(gameQuestDataJson[0]["daily9Cleared"].ToString());
                        userQuestData.dailyCleared[9] = bool.Parse(gameQuestDataJson[0]["daily10Cleared"].ToString());

                        userQuestData.dailyRewardRecieved[0] = bool.Parse(gameQuestDataJson[0]["dailyReward1Recieved"].ToString());
                        userQuestData.dailyRewardRecieved[1] = bool.Parse(gameQuestDataJson[0]["dailyReward2Recieved"].ToString());
                        userQuestData.dailyRewardRecieved[2] = bool.Parse(gameQuestDataJson[0]["dailyReward3Recieved"].ToString());
                        userQuestData.dailyRewardRecieved[3] = bool.Parse(gameQuestDataJson[0]["dailyReward4Recieved"].ToString());
                        userQuestData.dailyRewardRecieved[4] = bool.Parse(gameQuestDataJson[0]["dailyReward5Recieved"].ToString());

                        userQuestData.weeklyRewardRecieved[0] = bool.Parse(gameQuestDataJson[0]["weeklyReward1Recieved"].ToString());
                        userQuestData.weeklyRewardRecieved[1] = bool.Parse(gameQuestDataJson[0]["weeklyReward2Recieved"].ToString());
                        userQuestData.weeklyRewardRecieved[2] = bool.Parse(gameQuestDataJson[0]["weeklyReward3Recieved"].ToString());
                        userQuestData.weeklyRewardRecieved[3] = bool.Parse(gameQuestDataJson[0]["weeklyReward4Recieved"].ToString());
                        userQuestData.weeklyRewardRecieved[4] = bool.Parse(gameQuestDataJson[0]["weeklyReward5Recieved"].ToString());
                    }
                }
                catch (System.Exception e)
                {
                    userQuestData.Reset();
                    Debug.LogError(e);
                }
            }
            else // �������� ��
            {
                Debug.LogError($"����Ʈ ���� ������ �ҷ����⿡ �����߽��ϴ�. : {callback}");
            }


        });
    }
    public void GameAbilityDataLoad()
    {
        Backend.GameData.GetMyData("ABILITY_DATA", new Where(), callback =>
        {
            if (callback.IsSuccess())
            {
                //Debug.Log($"���� ���� ������ �ҷ����⿡ �����߽��ϴ�. : {callback}");

                try
                {
                    LitJson.JsonData gameAbilityDataJson = callback.FlattenRows();

                    if (gameAbilityDataJson.Count <= 0)
                    {
                        Debug.LogWarning("�����Ͱ� �������� �ʽ��ϴ�.");
                    }
                    else
                    {
                        //�ҷ��� ���� ������ ���� ��
                        gameAbilityDataRawInData = gameAbilityDataJson[0]["inDate"].ToString();

                        userAbilityData.abilityLevel[0] = int.Parse(gameAbilityDataJson[0]["ability0Level"].ToString());
                        userAbilityData.abilityLevel[1] = int.Parse(gameAbilityDataJson[0]["ability1Level"].ToString());
                        userAbilityData.abilityLevel[2] = int.Parse(gameAbilityDataJson[0]["ability2Level"].ToString());
                        userAbilityData.abilityLevel[3] = int.Parse(gameAbilityDataJson[0]["ability3Level"].ToString());
                        userAbilityData.abilityLevel[4] = int.Parse(gameAbilityDataJson[0]["ability4Level"].ToString());
                        userAbilityData.abilityLevel[5] = int.Parse(gameAbilityDataJson[0]["ability5Level"].ToString());
                        userAbilityData.abilityLevel[6] = int.Parse(gameAbilityDataJson[0]["ability6Level"].ToString());
                        userAbilityData.abilityLevel[7] = int.Parse(gameAbilityDataJson[0]["ability7Level"].ToString());
                        userAbilityData.abilityLevel[8] = int.Parse(gameAbilityDataJson[0]["ability8Level"].ToString());
                        userAbilityData.abilityLevel[9] = int.Parse(gameAbilityDataJson[0]["ability9Level"].ToString());
                        userAbilityData.abilityLevel[10] = int.Parse(gameAbilityDataJson[0]["ability10Level"].ToString());
                        userAbilityData.abilityLevel[11] = int.Parse(gameAbilityDataJson[0]["ability11Level"].ToString());
                    }
                }
                catch (System.Exception e)
                {
                    userAbilityData.Reset();
                    Debug.LogError(e);
                }
            }
            else // �������� ��
            {
                Debug.LogError($"�����Ƽ ���� ������ �ҷ����⿡ �����߽��ϴ�. : {callback}");
            }
        });
    }

    public void GameDataUpdate(UnityAction action = null)
    {
        GameUserDataUpdate();
        GameHeroDataUpdate();
        GameQuestDataUpdate();
        GameAbilityDataUpdate();
        if(LobbyManager.instance != null) CheckLevelUp();
        Debug.Log("GameDataUpdate EXP : " + userGameData.exp);
    }
    /// <summary>
    /// �ڳ� �ܼ� ���̺� �ִ� ���� ������ ����
    /// </summary>
    public void GameUserDataUpdate(UnityAction action = null)
    {
        if (userGameData == null)
        {
            Debug.LogError("�������� �ٿ�ްų� ���� ������ �����Ͱ� �������� �ʽ��ϴ�.");
            return;
        }

        Param param = new Param()
        {
            {"level", userGameData.level },
            {"exp", userGameData.exp },
            {"gold", userGameData.gold },
            {"gem", userGameData.gem },
            {"energy", userGameData.energy },
            {"highestChapter", userGameData.highestChapter },
            {"highestRewardChapter", userGameData.highestRewardChapter },
        };

        if (string.IsNullOrEmpty(gameUserDataRawInData))
        {
            Debug.LogError("������ inDate ������ ���� Update�� �����߽��ϴ�.");
        }
        else
        {
            Backend.GameData.UpdateV2("USER_DATA", gameUserDataRawInData, Backend.UserInDate, param,
                callback =>
            {
                if (callback.IsSuccess())
                {
                    action?.Invoke();
                }
                else
                {
                    Debug.LogError("���� ���� ������ ������ �����߽��ϴ� : " + callback);
                }
            });
        }
    }
    public void GameHeroDataUpdate(UnityAction action = null)
    {
        if (userHeroData == null)
        {
            Debug.LogError("�������� �ٿ�ްų� ���� ������ �����Ͱ� �������� �ʽ��ϴ�.");
            return;
        }

        Param param = new Param()
        {
            {"Hero1Level", userHeroData.heroLevel[0] },
            {"Hero2Level", userHeroData.heroLevel[1] },
            {"Hero3Level", userHeroData.heroLevel[2] },
            {"Hero4Level", userHeroData.heroLevel[3] },
            {"Hero5Level", userHeroData.heroLevel[4] },
            {"Hero6Level", userHeroData.heroLevel[5] },
            {"Hero7Level", userHeroData.heroLevel[6] },
            {"Hero8Level", userHeroData.heroLevel[7] },
            {"Hero9Level", userHeroData.heroLevel[8] },
            {"Hero10Level", userHeroData.heroLevel[9] },
            {"Hero11Level", userHeroData.heroLevel[10] },
            {"Hero12Level", userHeroData.heroLevel[11] },

            {"Hero1Count", userHeroData.heroCount[0] },
            {"Hero2Count", userHeroData.heroCount[1] },
            {"Hero3Count", userHeroData.heroCount[2] },
            {"Hero4Count", userHeroData.heroCount[3] },
            {"Hero5Count", userHeroData.heroCount[4] },
            {"Hero6Count", userHeroData.heroCount[5] },
            {"Hero7Count", userHeroData.heroCount[6] },
            {"Hero8Count", userHeroData.heroCount[7] },
            {"Hero9Count", userHeroData.heroCount[8] },
            {"Hero10Count", userHeroData.heroCount[9] },
            {"Hero11Count", userHeroData.heroCount[10] },
            {"Hero12Count", userHeroData.heroCount[11] },

            {"HeroChoose1", userHeroData.heroChooseNum[0] },
            {"HeroChoose2", userHeroData.heroChooseNum[1] },
            {"HeroChoose3", userHeroData.heroChooseNum[2] },
            {"HeroChoose4", userHeroData.heroChooseNum[3] },
        };

        if (string.IsNullOrEmpty(gameHeroDataRawInData))
        {
            Debug.LogError("������ inDate ������ ���� Update�� �����߽��ϴ�.");
        }
        else
        {
            Backend.GameData.UpdateV2("HERO_DATA", gameHeroDataRawInData, Backend.UserInDate, param,
                callback =>
                {
                    if (callback.IsSuccess())
                    {
                        //action?.Invoke();
                    }
                    else
                    {
                        Debug.LogError("���� ���� ������ ������ �����߽��ϴ� : " + callback);
                    }
                });
        }
    }
    public void GameQuestDataUpdate(UnityAction action = null)
    {
        if (userQuestData == null)
        {
            Debug.LogError("�������� �ٿ�ްų� ���� ������ �����Ͱ� �������� �ʽ��ϴ�.");
            return;
        }

        Param param = new Param()
        {
            {"dailyClearAmount", userQuestData.dailyClearAmount },
            {"weeklyClearAmount", userQuestData.weeklyClearAmount },

            {"quest1Progress", userQuestData.questProgress[0] },
            {"quest2Progress", userQuestData.questProgress[1] },
            {"quest3Progress", userQuestData.questProgress[2] },
            {"quest4Progress", userQuestData.questProgress[3] },
            {"quest5Progress", userQuestData.questProgress[4] },
            {"quest6Progress", userQuestData.questProgress[5] },
            {"quest7Progress", userQuestData.questProgress[6] },
            {"quest8Progress", userQuestData.questProgress[7] },
            {"quest9Progress", userQuestData.questProgress[8] },
            {"quest10Progress", userQuestData.questProgress[9] },

            {"daily1Cleared", userQuestData.dailyCleared[0] },
            {"daily2Cleared", userQuestData.dailyCleared[1] },
            {"daily3Cleared", userQuestData.dailyCleared[2] },
            {"daily4Cleared", userQuestData.dailyCleared[3] },
            {"daily5Cleared", userQuestData.dailyCleared[4] },
            {"daily6Cleared", userQuestData.dailyCleared[5] },
            {"daily7Cleared", userQuestData.dailyCleared[6] },
            {"daily8Cleared", userQuestData.dailyCleared[7] },
            {"daily9Cleared", userQuestData.dailyCleared[8] },
            {"daily10Cleared", userQuestData.dailyCleared[9] },

            {"dailyReward1Recieved", userQuestData.dailyRewardRecieved[0] },
            {"dailyReward2Recieved", userQuestData.dailyRewardRecieved[1] },
            {"dailyReward3Recieved", userQuestData.dailyRewardRecieved[2] },
            {"dailyReward4Recieved", userQuestData.dailyRewardRecieved[3] },
            {"dailyReward5Recieved", userQuestData.dailyRewardRecieved[4] },

            {"weeklyReward1Recieved", userQuestData.weeklyRewardRecieved[0] },
            {"weeklyReward2Recieved", userQuestData.weeklyRewardRecieved[1] },
            {"weeklyReward3Recieved", userQuestData.weeklyRewardRecieved[2] },
            {"weeklyReward4Recieved", userQuestData.weeklyRewardRecieved[3] },
            {"weeklyReward5Recieved", userQuestData.weeklyRewardRecieved[4] },
        };

        if (string.IsNullOrEmpty(gameQuestDataRawInData))
        {
            Debug.LogError("������ inDate ������ ���� Update�� �����߽��ϴ�.");
        }
        else
        {
            Backend.GameData.UpdateV2("QUEST_DATA", gameQuestDataRawInData, Backend.UserInDate, param,
                callback =>
                {
                    if (callback.IsSuccess())
                    {
                        //action?.Invoke();
                    }
                    else
                    {
                        Debug.LogError("���� ���� ������ ������ �����߽��ϴ� : " + callback);
                    }
                });
        }

    }
    public void GameAbilityDataUpdate(UnityAction action = null)
    {
        if (userAbilityData == null)
        {
            Debug.LogError("�������� �ٿ�ްų� ���� ������ �����Ͱ� �������� �ʽ��ϴ�.");
            return;
        }

        Param param = new Param()
        {
            {"ability0Level", userAbilityData.abilityLevel[0] },
            {"ability1Level", userAbilityData.abilityLevel[1] },
            {"ability2Level", userAbilityData.abilityLevel[2] },
            {"ability3Level", userAbilityData.abilityLevel[3] },
            {"ability4Level", userAbilityData.abilityLevel[4] },
            {"ability5Level", userAbilityData.abilityLevel[5] },
            {"ability6Level", userAbilityData.abilityLevel[6] },
            {"ability7Level", userAbilityData.abilityLevel[7] },
            {"ability8Level", userAbilityData.abilityLevel[8] },
            {"ability9Level", userAbilityData.abilityLevel[9] },
            {"ability10Level", userAbilityData.abilityLevel[10] },
            {"ability11Level", userAbilityData.abilityLevel[11] },

        };

        if (string.IsNullOrEmpty(gameAbilityDataRawInData))
        {
            Debug.LogError("������ inDate ������ ���� Update�� �����߽��ϴ�.");
        }
        else
        {
            Backend.GameData.UpdateV2("ABILITY_DATA", gameAbilityDataRawInData, Backend.UserInDate, param,
                callback =>
                {
                    if (callback.IsSuccess())
                    {
                        //action?.Invoke();
                    }
                    else
                    {
                        Debug.LogError("���� ���� ������ ������ �����߽��ϴ� : " + callback);
                    }
                });
        }

    }

    private void CheckLevelUp()
    {
        if(UserGameData.exp >= 1000 * Mathf.Pow(1.2f, UserGameData.level + 1))
        {
            Debug.Log("LevelUp");
            UserGameData.exp -= 1000 * Mathf.Pow(1.2f, UserGameData.level + 1);
            UserGameData.level++;
            GameDataUpdate();
            //Todo : ���� UI ����
            LobbyManager.instance.LevelUp();
        }
    }
}
