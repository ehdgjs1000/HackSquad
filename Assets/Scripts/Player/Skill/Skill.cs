using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Skill : MonoBehaviour
{
    public SkillData skill;
    [SerializeField] GameObject upgradePanel;

    [SerializeField] TextMeshProUGUI skillNameText;
    [SerializeField] TextMeshProUGUI skillDescText;
    [SerializeField] Image skillCharacterProfileImage;
    [SerializeField] Image skillImage;
    int skillId;
    int skillCharacterNum;

    private void SkillUpdate()
    {
        skillNameText.text = skill.name;
        skillDescText.text = skill.skillDescription;
        skillImage.sprite = skill.skillImage;
        skillId = skill.ID;
        //skillCharacterProfileImage = skill;
    }
    public void SkillChoose()
    {
        skill = SkillManager.instance.SkillChoose();
        skillCharacterNum = SkillManager.instance.ReturnSkillCharacterNum();
        SkillUpdate();
    }
    public void SkillChooseOnClick()
    {
        UpgradeSkill();
        upgradePanel.transform.DOScale(Vector3.zero,0.1f);
    }
    private void UpgradeSkill()
    {
        if (skillId == 101) GameManager.instance.players[skillCharacterNum].damage *= 1.1f;
        else if (skillId == 102) GameManager.instance.players[skillCharacterNum].fireRate *= 0.9f;
        else if (skillId == 103) GameManager.instance.players[skillCharacterNum].reloadingTime *= 0.9f;
        else if (skillId == 201) GameManager.instance.players[skillCharacterNum].damage *= 1.1f;
        else if (skillId == 202) GameManager.instance.players[skillCharacterNum].maxBullet += 5;
        else if (skillId == 203) GameManager.instance.players[skillCharacterNum].GetComponent<Veteran>().FixMaxRandomDegree(-5);
        else if (skillId == 301) GameManager.instance.players[skillCharacterNum].maxBullet += 1;
        else if (skillId == 302) GameManager.instance.players[skillCharacterNum].GetComponent<Bazooka>().UpgradeExploseRadius(1.1f);
        else if (skillId == 303) GameManager.instance.players[skillCharacterNum].reloadingTime *= 0.9f;
        else if (skillId == 401) GameManager.instance.players[skillCharacterNum].damage *= 1.1f;
        else if (skillId == 402) GameManager.instance.players[skillCharacterNum].attackRange *= 1.2f;
        else if (skillId == 403) GameManager.instance.players[skillCharacterNum].fireRate *= 0.9f;
        else if (skillId == 501) GameManager.instance.players[skillCharacterNum].damage *= 1.1f;
        else if (skillId == 502) GameManager.instance.players[skillCharacterNum].fireRate *= 1.2f;
        else if (skillId == 503) GameManager.instance.players[skillCharacterNum].maxBullet++;
        else if (skillId == 601) GameManager.instance.players[skillCharacterNum].damage *= 1.1f;
        else if (skillId == 602) GameManager.instance.players[skillCharacterNum].maxBullet++;
        else if (skillId == 603) GameManager.instance.players[skillCharacterNum].GetComponent<Iceman>().UpgradeSlowSkill();
        else if (skillId == 701) GameManager.instance.players[skillCharacterNum].damage *= 1.1f;
        else if (skillId == 702) GameManager.instance.players[skillCharacterNum].maxBullet += 3;
        else if (skillId == 703) GameManager.instance.players[skillCharacterNum].GetComponent<Alien>().StunSkillUpgrade();
        else if (skillId == 801) GameManager.instance.players[skillCharacterNum].damage *= 1.1f;
        else if (skillId == 802) GameManager.instance.players[skillCharacterNum].maxBullet += 5;
        else if (skillId == 803) GameManager.instance.players[skillCharacterNum].GetComponent<Veteran>().FixMaxRandomDegree(-1);
        else if (skillId == 901) GameManager.instance.players[skillCharacterNum].damage *= 1.1f;
        else if (skillId == 902) GameManager.instance.players[skillCharacterNum].maxBullet += 5;
        else if (skillId == 903) GameManager.instance.players[skillCharacterNum].GetComponent<Veteran>().FixMaxRandomDegree(-1);
        else if (skillId == 1001) GameManager.instance.players[skillCharacterNum].damage *= 1.1f;
        else if (skillId == 1002) GameManager.instance.players[skillCharacterNum].maxBullet += 5;
        else if (skillId == 1003) GameManager.instance.players[skillCharacterNum].GetComponent<Veteran>().FixMaxRandomDegree(-2);
        else if (skillId == 1101) GameManager.instance.players[skillCharacterNum].damage *= 1.1f;
        else if (skillId == 1102) GameManager.instance.players[skillCharacterNum].maxBullet += 1;
        else if (skillId == 1103) GameManager.instance.players[skillCharacterNum].GetComponent<Jester>().PoisionTermUpgrade();


    }
    


}
