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
}
