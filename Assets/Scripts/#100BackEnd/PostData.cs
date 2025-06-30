using System.Collections.Generic;


public class PostData
{
    public string title;
    public string content;
    public string inDate;
    public string expirationDate;

    public bool isCanReceive = false;

    // <아이템이름, 아이템개수>
    public Dictionary<string, int> postReward = new Dictionary<string, int>();

    public override string ToString()
    {
        string result = string.Empty;
        result += $"title : {title}\n";
        result += $"content : {content}\n";
        result += $"inDate : {inDate}\n";

        if (isCanReceive)
        {
            result += "우편 아이템\n";

            foreach (string itemKey in postReward.Keys)
            {
                result += $"| {itemKey} : {postReward[itemKey]}";
            }
        }
        else
        {
            result += "지원하지 않는 아이템입니다.";
        }
        
        return result;
    }

}
