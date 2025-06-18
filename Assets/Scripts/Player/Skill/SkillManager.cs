using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public SkillData[] skillDatas;
    int skillCharacterNum = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public SkillData SkillChoose()
    {
        int randomSkillNum = Random.Range(0, skillDatas.Length);
        skillCharacterNum = (randomSkillNum + 1) / 4;
        /*if (skillDatas[randomSkillNum].skillLevel == 3 || skillDatas[randomSkillNum] == null)
        {
            SkillChoose();
            return null;
        }
        else
        {
            return skillDatas[randomSkillNum];
        }*/
        return skillDatas[randomSkillNum];
    }
    public int ReturnSkillCharacterNum()
    {
        return skillCharacterNum;
    }

}
