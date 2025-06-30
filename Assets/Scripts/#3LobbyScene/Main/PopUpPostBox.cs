using System.Collections.Generic;
using UnityEngine;

public class PopUpPostBox : MonoBehaviour
{
    [SerializeField] BackendPostSystem backendPostSystem;
    [SerializeField] GameObject postPrefab;
    [SerializeField] Transform parentContent;
    [SerializeField] GameObject textSystem;

    List<GameObject> postList;

    private void Awake()
    {
        postList = new List<GameObject>();
    }
    private void OnDisable()
    {
        DestroyPostAll();
    }
     
    public void SpawnPostAll(List<PostData> postDataList)
    {
        for (int i = 0; i < postDataList.Count; i++)
        {
            GameObject clone = Instantiate(postPrefab, parentContent);
            clone.GetComponent<Post>().SetUp(backendPostSystem, this, postDataList[i]);
            postList.Add(clone);
        }

        textSystem.SetActive(false);
    }
    public void DestroyPostAll()
    {
        foreach (GameObject post in postList)
        {
            if(post != null) Destroy(post);
        }

        postList.Clear();
        textSystem.SetActive(true);
    }
    public void DestroyPost(GameObject post)
    {
        Destroy(this);
        postList.Remove(post);

        if (postList.Count == 0)
        {
            textSystem.SetActive(true);
        }

    }
}
 