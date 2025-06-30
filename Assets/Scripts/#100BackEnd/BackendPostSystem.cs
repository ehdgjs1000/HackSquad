using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BackEnd;

public class BackendPostSystem : MonoBehaviour
{
    [System.Serializable]
    public class PostEvent : UnityEvent<List<PostData>> { }
    public PostEvent onGetPostListEvent = new PostEvent();
    private List<PostData> postList = new List<PostData>();
    public void PostListGet()
    {
        PostListGet(PostType.Admin);
    }

    private void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            PostListGet(PostType.Admin);
        }else if (Input.GetKeyDown("2"))
        {
            PostReceive(PostType.Admin, 0);
        }else if (Input.GetKeyDown("3"))
        {
            PostRecieveAll(PostType.Admin);
        }
    }

    public void PostReceive(PostType postType, string inDate)
    {
        PostReceive(postType, postList.FindIndex(item => item.inDate.Equals(inDate)));
    }
    public void PostRecieveAll()
    {
        PostRecieveAll(PostType.Admin);
    }
    public void PostListGet(PostType postType)
    {
        Backend.UPost.GetPostList(postType, callback =>
        {
            if (!callback.IsSuccess())
            {
                Debug.LogError("우편 불러오기 중 에러가 발생했습니다." + callback);
                return;
            }
            Debug.Log("우편 리스트 불러오기에 성공했습니다.");

            try
            {
                LitJson.JsonData jsonData = callback.GetFlattenJSON()["postList"];

                if (jsonData.Count <= 0)
                {
                    Debug.LogError("우편함이 비어있습니다.");
                    return;
                }
                postList.Clear();

                for (int i=0; i < jsonData.Count; i++)
                {
                    PostData post = new PostData();

                    post.title = jsonData[i]["title"].ToString();
                    post.content = jsonData[i]["content"].ToString();
                    post.inDate = jsonData[i]["inDate"].ToString();
                    post.expirationDate = jsonData[i]["expirationDate"].ToString();

                    //우편과 함께 발송된 모든 아이템 가져오기
                    foreach (LitJson.JsonData itemJson in jsonData[i]["items"])
                    {
                        //우편에 함꼐 발송된 차트의 이름이 "재화차트"일 떄
                        if (itemJson["chartName"].ToString() == Constants.GOODS_CHART_NAME)
                        {
                            string itemName = itemJson["item"]["itemName"].ToString();
                            int itemCount = int.Parse(itemJson["itemCount"].ToString());

                            //우편에 포함된 아이템이 여러개일 떄
                            //이미 postReward에 해당 아이템 정보가 있으면 갯수 추가
                            if (post.postReward.ContainsKey(itemName))
                            {
                                post.postReward[itemName] += itemCount;
                            }
                            else
                            {
                                post.postReward.Add(itemName, itemCount);
                            }
                            post.isCanReceive = true;
                        }
                        else
                        {
                            Debug.LogWarning("아직 지원하지 않는 차트 정보입니다. : " + 
                                itemJson["chartName"].ToString());

                            post.isCanReceive = false;
                        }
                    }
                    postList.Add(post);

                }
                //우편 리스트 불러오기가 완료되었을 때  이벤트 메소드 호출
                onGetPostListEvent?.Invoke(postList);

                for (int i = 0; i< postList.Count; i++)
                {
                    Debug.Log(i + "번째 우편 :" + postList[i].ToString());
                }

            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }
        });
    }
    
    public void SavePostToLocal(LitJson.JsonData  item)
    {
        try
        {
            foreach (LitJson.JsonData itemJson in item)
            {
                //차트 파일 이름과 backend console에 등록한 차트 이름
                string chartFileName = itemJson["item"]["chartFileName"].ToString();
                string chartName = itemJson["chartName"].ToString();

                int itemId = int.Parse(itemJson["item"]["itemId"].ToString());
                string itemName = itemJson["item"]["itemName"].ToString();
                string itemInfo = itemJson["item"]["itemInfo"].ToString();

                //우편 발송할 때 적는 아이템 수량
                int itemCount = int.Parse(itemJson["itemCount"].ToString());

                //우편으로 받은 아이템 재화를 게임 내 데이터에 적용
                if (chartName.Equals(Constants.GOODS_CHART_NAME))
                {
                    if (itemName.Equals("gold"))
                    {
                        BackEndGameData.Instance.UserGameData.gold += itemCount;
                    }else if (itemName.Equals("gem"))
                    {
                        BackEndGameData.Instance.UserGameData.gem += itemCount;
                    }else if (itemName.Equals("energy"))
                    {
                        BackEndGameData.Instance.UserGameData.energy += itemCount;
                    }
                }
                Debug.Log($"{chartName} - {chartFileName}");
                Debug.Log($"[{itemId}] {itemName} : {itemInfo}, 획득수량 : {itemCount}");
                Debug.Log($"아이템을 수령했습니다. : {itemName} - {itemCount}");

            }
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }
    public void PostReceive(PostType postType, int index)
    {
        if (postList.Count <= 0)
        {
            Debug.LogWarning("받을 수 있는 우편이 존재하지 않습니다. 혹은 우편 불러오기를 먼저 호출해주세요");
            return;
        }
        if (index >= postList.Count)
        {
            Debug.LogError("해당 우편은 존재하지 않습니다.");
            return;
        }

        Backend.UPost.ReceivePostItem(postType, postList[index].inDate, callback =>
        {
            if (!callback.IsSuccess())
            {
                Debug.LogError($"{postType.ToString()}의 {postList[index].inDate} 우편 수령 중 에러가 발생했습니다. {callback}");
                return;
            }

            postList.RemoveAt(index);

            //저장 가능한 아이템이 있을 때
            if (callback.GetFlattenJSON()["postItems"].Count > 0)
            {
                SavePostToLocal(callback.GetFlattenJSON()["postItems"]);
                BackEndGameData.Instance.GameDataUpdate();
                MainManager.instance.UpdateMainUI();
            }
            else
            {
                Debug.LogWarning("수령가능한 아이템이 존재하지 않습니다.");
            }

        });
    }

    public void PostRecieveAll(PostType postType)
    {
        if (postList.Count <= 0)
        {
            Debug.LogWarning("받을 수 있는 우편이 없습니다.");
            return;
        }
        Debug.Log("우편 전체 수령을 요청합니다.");

        Backend.UPost.ReceivePostItemAll(postType, callback =>
        {
            if (!callback.IsSuccess())
            {
                Debug.LogError("우편 전체 수령중 에러가 발생했습니다.");
                return;
            }
            Debug.Log("전체 우편 수령에 성공했습니다.");

            postList.Clear();

            foreach (LitJson.JsonData postItemsJson in callback.GetFlattenJSON()["postItems"])
            {
                SavePostToLocal(postItemsJson);
            }
            BackEndGameData.Instance.GameDataUpdate();
            MainManager.instance.UpdateMainUI();
        });

    }

}
