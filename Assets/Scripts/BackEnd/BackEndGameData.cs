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
    
    /// <summary>
    /// 뒤끝 콘솔 테이블에 새로운 유저 정보 추가
    /// </summary>
    public void GameDataInsert()
    {
        GameUserDataInsert();
        GameHeroDataInsert();
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
            {"energy", userGameData.energy }
        };

        Backend.GameData.Insert("USER_DATA", param, callback =>
        {
            if (callback.IsSuccess())
            {
                gameUserDataRawInData = callback.GetInDate();
            }
            else
            {
                Debug.LogError("게임정보 삽입에 실패했습니다.");
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

            {"HeroChoose1", userHeroData.heroChooseNum[0]},
            {"HeroChoose2", userHeroData.heroChooseNum[1]},
            {"HeroChoose3", userHeroData.heroChooseNum[2]},
            {"HeroChoose4", userHeroData.heroChooseNum[3]}
        };

        Backend.GameData.Insert("HERO_DATA", param, callback =>
        {
            if (callback.IsSuccess())
            {
                gameHeroDataRawInData = callback.GetInDate();
            }
            else
            {
                Debug.LogError("게임정보 삽입에 실패했습니다.");
            }
        });

    }

    /// <summary>
    /// 뒤끝 콜솔 테이블에서 유저 정보를 불러올떄 호출
    /// </summary>
    public void GameDataLoad()
    {
        GameUserDataLoad();
        GameHeroDataLoad();
    }
    public void GameUserDataLoad()
    {
        Backend.GameData.GetMyData("USER_DATA", new Where(), callback =>
        {
            if (callback.IsSuccess())
            {
                //Debug.Log($"게임 정보 데이터 불러오기에 성공했습니다. : {callback}");

                try
                {
                    LitJson.JsonData gameDataJson = callback.FlattenRows();

                    if (gameDataJson.Count <= 0)
                    {
                        Debug.LogWarning("데이터가 존재하지 않습니다.");
                    }
                    else
                    {
                        //불러온 게임 정보의 고유 값
                        gameUserDataRawInData = gameDataJson[0]["inDate"].ToString();
                        //불러온 게임 정보를 userGameData 변수에 저장
                        userGameData.level = int.Parse(gameDataJson[0]["level"].ToString());
                        userGameData.exp = float.Parse(gameDataJson[0]["exp"].ToString());
                        userGameData.gold = int.Parse(gameDataJson[0]["gold"].ToString());
                        userGameData.gem = int.Parse(gameDataJson[0]["gem"].ToString());
                        userGameData.energy = int.Parse(gameDataJson[0]["energy"].ToString());

                        onGameDataLoadEvent?.Invoke();
                    }
                }
                catch (System.Exception e)
                {
                    userGameData.Reset();
                    Debug.LogError(e);
                }
            }
            else // 실패했을 때
            {
                Debug.LogError($"게임 정보 데이터 불러오기에 실패했습니다. : {callback}");
            }


        });
    }
    public void GameHeroDataLoad()
    {
        Backend.GameData.GetMyData("HERO_DATA", new Where(), callback =>
        {
            if (callback.IsSuccess())
            {
                //Debug.Log($"게임 정보 데이터 불러오기에 성공했습니다. : {callback}");

                try
                {
                    LitJson.JsonData gameHeroDataJson = callback.FlattenRows();

                    if (gameHeroDataJson.Count <= 0)
                    {
                        Debug.LogWarning("데이터가 존재하지 않습니다.");
                    }
                    else
                    {
                        //불러온 게임 정보의 고유 값
                        gameHeroDataRawInData = gameHeroDataJson[0]["inDate"].ToString();
                        //불러온 게임 정보를 userGameData 변수에 저장
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
            else // 실패했을 때
            {
                Debug.LogError($"게임 정보 데이터 불러오기에 실패했습니다. : {callback}");
            }


        });
    }

    public void GameDataUpdate(UnityAction action = null)
    {
        GameUserDataUpdate();
        GameHeroDataUpdate();
    }
    /// <summary>
    /// 뒤끝 콘솔 테이블에 있는 유저 데이터 갱신
    /// </summary>
    public void GameUserDataUpdate(UnityAction action = null)
    {
        if (userGameData == null)
        {
            Debug.LogError("서버에서 다운받거나 새로 삽입한 데이터가 존재하지 않습니다.");
            return;
        }

        Param param = new Param()
        {
            {"level", userGameData.level },
            {"exp", userGameData.exp },
            {"gold", userGameData.gold },
            {"gem", userGameData.gem },
            {"energy", userGameData.energy }
        };

        if (string.IsNullOrEmpty(gameUserDataRawInData))
        {
            Debug.LogError("유저의 inDate 정보가 없어 Update에 실패했습니다.");
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
                    Debug.LogError("게임 정보 데이터 수정에 실패했습니다 : " + callback);
                }
            });
        }
    }
    public void GameHeroDataUpdate(UnityAction action = null)
    {
        if (userHeroData == null)
        {
            Debug.LogError("서버에서 다운받거나 새로 삽입한 데이터가 존재하지 않습니다.");
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

            {"HeroChoose1", userHeroData.heroChooseNum[0] },
            {"HeroChoose2", userHeroData.heroChooseNum[1] },
            {"HeroChoose3", userHeroData.heroChooseNum[2] },
            {"HeroChoose4", userHeroData.heroChooseNum[3] }
        };

        if (string.IsNullOrEmpty(gameHeroDataRawInData))
        {
            Debug.LogError("유저의 inDate 정보가 없어 Update에 실패했습니다.");
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
                        Debug.LogError("게임 정보 데이터 수정에 실패했습니다 : " + callback);
                    }
                });
        }
    }

}
