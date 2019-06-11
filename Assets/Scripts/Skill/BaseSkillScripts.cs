using UnityEngine;
using UnityEditor;
using System.Collections;
using FreeNet;

[System.Serializable]
public class SkillObject
{
    public GameObject Player;
    public GameObject Skill;
    public KeyCode SkillKey;
    public float BaseCooltime;
    public float Cooltime;
    public float Damage;
    public float Delay;
    public Player PlayerID;
    public delegate bool Skill_f(SkillObject s);
    public Skill_f UseSkill;

    /*
     * UseSkill을 Delegate를 사용하여 각각 스킬에따른 다른스킬효과를 적용시킨다.
     */

    public float CoolPercent()
    {
        if (Cooltime <= 0)
            return 0;

        float value = (Cooltime / BaseCooltime) * 100;

        return value;
    }
}

public class BaseSkillScripts : MonoBehaviour
{
    public SkillObject MainSkill;
    public SkillObject SubSkill;
    public SkillObject UltimateSkill;
    public Player PlayerID;
    public Avatar Idle;
    public Avatar Skill;

    public void CoolDown()
    {
        MainSkill.Cooltime -= Time.fixedDeltaTime;
        SubSkill.Cooltime -= Time.fixedDeltaTime;
        UltimateSkill.Cooltime -= Time.fixedDeltaTime;
    }

    public void UseSkill()
    {
        if (Input.GetKeyDown(MainSkill.SkillKey))
        {
            MainSkill.UseSkill(MainSkill);
            CPacket msg = CPacket.create((short)PROTOCOL.USE_SKILLONE);
            msg.push(PlayerID.player_index);

            PlayerID.manager.send(msg);
        }

        if (Input.GetKeyDown(SubSkill.SkillKey))
        {
            SubSkill.UseSkill(SubSkill);
            CPacket msg = CPacket.create((short)PROTOCOL.USE_SKILLTWO);
            msg.push(PlayerID.player_index);

            PlayerID.manager.send(msg);
        }

        if (Input.GetKeyDown(UltimateSkill.SkillKey))
        {
            UltimateSkill.UseSkill(UltimateSkill);
            CPacket msg = CPacket.create((short)PROTOCOL.USE_SKILLTHREE);
            msg.push(PlayerID.player_index);

            PlayerID.manager.send(msg);
        }
    }

    public IEnumerator ChangeAvatar(float time)
    {
        PlayerID.Anim.avatar = Skill;

        yield return new WaitForSeconds(time);

        PlayerID.Anim.avatar = Idle;
        PlayerID.UseSkill = 0;
        PlayerID.IsUseSkill = false;

        yield return null;
    }

    public bool Skill1(SkillObject s)
    {
        if (s.Cooltime > 0 || PlayerID.IsUseSkill)
            return false;

        s.Skill.SetActive(true);

        s.PlayerID.UseSkill = 1;
        s.PlayerID.IsUseSkill = true;
        s.Cooltime = s.BaseCooltime;

        StartCoroutine("ChangeAvatar", s.Delay);

        return true;
    }

    public bool Skill2(SkillObject s)
    {
        if (s.Cooltime > 0 || PlayerID.IsUseSkill)
            return false;

        s.Skill.SetActive(true);

        s.PlayerID.UseSkill = 2;
        s.PlayerID.IsUseSkill = true;
        s.Cooltime = s.BaseCooltime;

        StartCoroutine("ChangeAvatar", s.Delay);

        return true;

    }

    public bool Skill3(SkillObject s)
    {
        if (s.Cooltime > 0 || PlayerID.IsUseSkill)
            return false;

        s.Skill.SetActive(true);

        s.PlayerID.UseSkill = 3;
        s.PlayerID.IsUseSkill = true;
        s.Cooltime = s.BaseCooltime;

        StartCoroutine("ChangeAvatar", s.Delay);

        return true;
    }
}
