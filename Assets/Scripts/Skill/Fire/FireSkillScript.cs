using UnityEngine;
using System.Collections;

public class FireSkillScript : BaseSkillScripts
{

    // Use this for initialization
    void Start()
    {
        MainSkill.UseSkill += Skill1;
        SubSkill.UseSkill += Skill2;
        UltimateSkill.UseSkill += Skill3;

        MainSkill.Skill.GetComponent<SkillInformation>().Damage = MainSkill.Damage;
        MainSkill.Skill.GetComponent<SkillInformation>().player_index = MainSkill.PlayerID.player_index;
        SubSkill.Skill.GetComponent<SkillInformation>().Damage = SubSkill.Damage;
        SubSkill.Skill.GetComponent<SkillInformation>().player_index = SubSkill.PlayerID.player_index;
        UltimateSkill.Skill.GetComponent<SkillInformation>().Damage = UltimateSkill.Damage;
        UltimateSkill.Skill.GetComponent<SkillInformation>().player_index = UltimateSkill.PlayerID.player_index;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerID.Title.local_player_index != PlayerID.player_index)
            return;

        UseSkill();
    }

    private void FixedUpdate()
    {
        CoolDown();
    }

    bool Skill1(SkillObject s)
    {
        if (s.Cooltime > 0 || PlayerID.IsUseSkill)
            return false;

        s.Skill.SetActive(true);

        s.PlayerID.UseSkill = 1;
        s.PlayerID.IsUseSkill = true;
        s.Cooltime = s.BaseCooltime;

        StartCoroutine("ChangeAvatar", 5.0f);

        return true;
    }

    bool Skill2(SkillObject s)
    {
        if (s.Cooltime > 0 || PlayerID.IsUseSkill)
            return false;

        s.Skill.SetActive(true);

        s.PlayerID.UseSkill = 2;
        s.PlayerID.IsUseSkill = true;
        s.Cooltime = s.BaseCooltime;

        StartCoroutine("ChangeAvatar", 2.8f);

        return true;

    }

    bool Skill3(SkillObject s)
    {
        if (s.Cooltime > 0 || PlayerID.IsUseSkill)
            return false;

        s.Skill.SetActive(true);

        s.PlayerID.UseSkill = 3;
        s.PlayerID.IsUseSkill = true;
        s.Cooltime = s.BaseCooltime;

        StartCoroutine("ChangeAvatar", 3.3f);

        return true;
    }
}
