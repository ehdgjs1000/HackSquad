using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public SkillData[] skillDatas;
    public SkillData[] finalSkill;
    int skillCharacterNum = 0;
    SkillData choosedSkill;



    private void Awake()
    {
        if (instance == null) instance = this;
    }
    public void ResetSkillLevel()
    {
        for (int a = 0; a < skillDatas.Length; a++)
        {
            if(skillDatas[a] != null) skillDatas[a].skillLevel = 0;
        }
        for (int a = 0; a < finalSkill.Length; a++)
        {
            if (finalSkill[a] != null) finalSkill[a].skillLevel = 0;
        }
    }

    public SkillData SkillChoose()
    {
        int skillType = Random.Range(0, 10);
        if (skillType < 5) FinalSkillChoose();
        else NormalSkillChoose();

        return choosedSkill;
    }
    private void NormalSkillChoose()
    {
        int randomSkillNum = Random.Range(0, skillDatas.Length);
        skillCharacterNum = (randomSkillNum + 1) / 4;
        if (skillDatas[randomSkillNum] == null) NormalSkillChoose();
        else
        {
            if (skillDatas[randomSkillNum].skillLevel == 3)
            {
                NormalSkillChoose();
            }
            else
            {
                choosedSkill = skillDatas[randomSkillNum];
            }
        }
        
    }
    private void FinalSkillChoose()
    {
        int randomSkillNum = Random.Range(0, 4);
        skillCharacterNum = randomSkillNum;
        if (skillDatas[randomSkillNum*3].skillLevel == 3 &&
            skillDatas[randomSkillNum*3 + 1].skillLevel == 3 &&
            skillDatas[randomSkillNum*3 + 2].skillLevel == 3)
        {
            if (finalSkill[randomSkillNum].skillLevel == 1) SkillChoose();
            else choosedSkill = finalSkill[randomSkillNum];
        }
        else
        {
            SkillChoose();
        }
    }
    public int ReturnSkillCharacterNum()
    {
        return skillCharacterNum;
    }

}
