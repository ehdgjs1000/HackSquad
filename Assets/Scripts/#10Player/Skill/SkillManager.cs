using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public int[] choosedSkillID = new int[3];
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

    public SkillData SkillChoose(int _order)
    {
        int skillType = Random.Range(0, 10);
        if (skillType < 5) FinalSkillChoose(_order);
        else NormalSkillChoose(_order);

        return choosedSkill;
    }
    private void NormalSkillChoose(int _order)
    {
        int randomSkillNum = Random.Range(0, skillDatas.Length);

        //4�� ���� character ���� ����
        skillCharacterNum = randomSkillNum / 3;
        if (skillDatas[randomSkillNum] == null) NormalSkillChoose(_order);
        else
        {
            //�̹� ������ ��ų�� ��� �ٽ� �̱�
            if (skillDatas[randomSkillNum].skillLevel == 3)
            {
                NormalSkillChoose(_order);
            }
            else
            {
                choosedSkill = skillDatas[randomSkillNum];
                choosedSkillID[_order] = randomSkillNum;
            }
        }
        //�� ��ų�� ID�� skillmanager�� �̹� �� ��ų index ��
        for (int i = 0; i < 3; i++)
        {
            if (i != _order)
            {
                //�̹� �� ��ų�� ���
                if (choosedSkillID[i] == randomSkillNum)
                {
                    NormalSkillChoose(_order);
                    break;
                }
            }
            
        }
    }
    private void FinalSkillChoose(int _order)
    {
        int randomSkillNum = Random.Range(0, GameManager.instance.heroCount);
        skillCharacterNum = randomSkillNum;
        int masterSkillCount = 0;
        for (int i = 0; i <3; i++)
        {
            if(skillDatas[randomSkillNum*3+i].skillLevel == 3) masterSkillCount++;
        }
        if (masterSkillCount >= 2 && finalSkill[randomSkillNum].skillLevel == 0) choosedSkill = finalSkill[randomSkillNum];
        else SkillChoose(_order);
    }
    public int ReturnSkillCharacterNum()
    {
        return skillCharacterNum;
    }

}
