using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSkillScript : BaseSkillScripts
{

    // Start is called before the first frame update
    void Start()
    {
        MainSkill.UseSkill += Skill2;
        SubSkill.UseSkill += Skill1;
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
}
