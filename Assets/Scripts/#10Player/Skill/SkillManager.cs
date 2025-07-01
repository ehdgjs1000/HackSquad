using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public SkillData[] skillDatas;
    public SkillData[] finalSkill;
    int skillCharacterNum = 0;
    SkillData choosedSkill;
    int characterCount = 0;


    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        characterCount = GameManager.instance.heroCount;
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

        //4를 현재 character 수로 변경
        skillCharacterNum = randomSkillNum / 3;
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
        int randomSkillNum = Random.Range(0, GameManager.instance.heroCount);
        skillCharacterNum = randomSkillNum;
        int masterSkillCount = 0;
        for (int i = 0; i <3; i++)
        {
            if(skillDatas[randomSkillNum*3+i].skillLevel == 3) masterSkillCount++;
        }
        if (masterSkillCount >= 2 && finalSkill[randomSkillNum].skillLevel == 0) choosedSkill = finalSkill[randomSkillNum];
        else SkillChoose();

        /*if (skillDatas[randomSkillNum*3].skillLevel == 3 &&
            skillDatas[randomSkillNum*3 + 1].skillLevel == 3 &&
            skillDatas[randomSkillNum*3 + 2].skillLevel == 3)
        {
            if (finalSkill[randomSkillNum].skillLevel == 1) SkillChoose();
            else choosedSkill = finalSkill[randomSkillNum];
        }
        else SkillChoose();*/
    }
    public int ReturnSkillCharacterNum()
    {
        return skillCharacterNum;
    }

}
