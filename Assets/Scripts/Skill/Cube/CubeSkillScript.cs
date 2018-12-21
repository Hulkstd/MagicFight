using UnityEngine;
using System.Collections;

public class CubeSkillScript : BaseSkillScripts
{
    void Start()
    {
        MainSkill.UseSkill += Skill1;
        SubSkill.UseSkill += Skill1;
        UltimateSkill.UseSkill += Skill1;
    }

    // Update is called once per framed 
    void Update()
    {
        if (PlayerID.Title.local_player_index != PlayerID.player_index)
            return;

        UseSkill();
    }

    void FixedUpdate()
    {
        CoolDown();
    }

    bool Skill1(SkillObject s)
    {
        if (s.Cooltime > 0)
            return false;

        GameObject skill = Instantiate(s.Skill, s.Player.transform.position + s.Player.transform.TransformDirection(Vector3.forward) * 2, new Quaternion());
        skill.GetComponent<SkillInformation>().Damage = s.Damage;
        skill.GetComponent<SkillInformation>().player_index = PlayerID.player_index;
        skill.GetComponent<Rigidbody>().velocity = s.Player.transform.TransformDirection(Vector3.forward) * 3;
        s.Cooltime = s.BaseCooltime;

        Destroy(skill, 3.0f);

        return true;
    }
}
