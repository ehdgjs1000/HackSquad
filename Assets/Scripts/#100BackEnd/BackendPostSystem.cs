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
                Debug.LogError("���� �ҷ����� �� ������ �߻��߽��ϴ�." + callback);
                return;
            }
            Debug.Log("���� ����Ʈ �ҷ����⿡ �����߽��ϴ�.");

            try
            {
                LitJson.JsonData jsonData = callback.GetFlattenJSON()["postList"];

                if (jsonData.Count <= 0)
                {
                    Debug.LogError("�������� ����ֽ��ϴ�.");
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

                    //����� �Բ� �߼۵� ��� ������ ��������
                    foreach (LitJson.JsonData itemJson in jsonData[i]["items"])
                    {
                        //���� �Բ� �߼۵� ��Ʈ�� �̸��� "��ȭ��Ʈ"�� ��
                        if (itemJson["chartName"].ToString() == Constants.GOODS_CHART_NAME)
                        {
                            string itemName = itemJson["item"]["itemName"].ToString();
                            int itemCount = int.Parse(itemJson["itemCount"].ToString());

                            //���� ���Ե� �������� �������� ��
                            //�̹� postReward�� �ش� ������ ������ ������ ���� �߰�
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
                            Debug.LogWarning("���� �������� �ʴ� ��Ʈ �����Դϴ�. : " + 
                                itemJson["chartName"].ToString());

                            post.isCanReceive = false;
                        }
                    }
                    postList.Add(post);

                }
                //���� ����Ʈ �ҷ����Ⱑ �Ϸ�Ǿ��� ��  �̺�Ʈ �޼ҵ� ȣ��
                onGetPostListEvent?.Invoke(postList);

                for (int i = 0; i< postList.Count; i++)
                {
                    Debug.Log(i + "��° ���� :" + postList[i].ToString());
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
                //��Ʈ ���� �̸��� backend console�� ����� ��Ʈ �̸�
                string chartFileName = itemJson["item"]["chartFileName"].ToString();
                string chartName = itemJson["chartName"].ToString();

                int itemId = int.Parse(itemJson["item"]["itemId"].ToString());
                string itemName = itemJson["item"]["itemName"].ToString();
                string itemInfo = itemJson["item"]["itemInfo"].ToString();

                //���� �߼��� �� ���� ������ ����
                int itemCount = int.Parse(itemJson["itemCount"].ToString());

                //�������� ���� ������ ��ȭ�� ���� �� �����Ϳ� ����
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
                Debug.Log($"[{itemId}] {itemName} : {itemInfo}, ȹ����� : {itemCount}");
                Debug.Log($"�������� �����߽��ϴ�. : {itemName} - {itemCount}");

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
            Debug.LogWarning("���� �� �ִ� ������ �������� �ʽ��ϴ�. Ȥ�� ���� �ҷ����⸦ ���� ȣ�����ּ���");
            return;
        }
        if (index >= postList.Count)
        {
            Debug.LogError("�ش� ������ �������� �ʽ��ϴ�.");
            return;
        }

        Backend.UPost.ReceivePostItem(postType, postList[index].inDate, callback =>
        {
            if (!callback.IsSuccess())
            {
                Debug.LogError($"{postType.ToString()}�� {postList[index].inDate} ���� ���� �� ������ �߻��߽��ϴ�. {callback}");
                return;
            }

            postList.RemoveAt(index);

            //���� ������ �������� ���� ��
            if (callback.GetFlattenJSON()["postItems"].Count > 0)
            {
                SavePostToLocal(callback.GetFlattenJSON()["postItems"]);
                BackEndGameData.Instance.GameDataUpdate();
                MainManager.instance.UpdateMainUI();
            }
            else
            {
                Debug.LogWarning("���ɰ����� �������� �������� �ʽ��ϴ�.");
            }

        });
    }

    public void PostRecieveAll(PostType postType)
    {
        if (postList.Count <= 0)
        {
            Debug.LogWarning("���� �� �ִ� ������ �����ϴ�.");
            return;
        }
        Debug.Log("���� ��ü ������ ��û�մϴ�.");

        Backend.UPost.ReceivePostItemAll(postType, callback =>
        {
            if (!callback.IsSuccess())
            {
                Debug.LogError("���� ��ü ������ ������ �߻��߽��ϴ�.");
                return;
            }
            Debug.Log("��ü ���� ���ɿ� �����߽��ϴ�.");

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
