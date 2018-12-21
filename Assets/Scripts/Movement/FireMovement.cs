using UnityEngine;
using System.Collections;

public class FireMovement : Movement
{

    // Use this for initialization
    void Start()
    {
        IsOnGround = true;
        
        if(PlayerID.Title.local_player_index == PlayerID.player_index)
        {
            InvokeRepeating("ToServer", 0, 0.05f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Animation();

        if (PlayerID.Title.local_player_index != PlayerID.player_index)
        {
            return;
        }
        if(PlayerID.IsDead)
        {
            return;
        }

        Jump();
        NowSpeed();
    }

    void FixedUpdate()
    {
        if (PlayerID.Title.local_player_index != PlayerID.player_index)
            return;
        Move();
    }

    void ToServer()
    {
        Send();
    }

    public override void Animation()
    {
        PlayerID.Anim.SetBool("IsIdle", PlayerID.IsIdle);
        PlayerID.Anim.SetBool("IsWalking", PlayerID.IsWalking);
        PlayerID.Anim.SetBool("IsRunning", PlayerID.IsRunning);
        PlayerID.Anim.SetBool("IsDead", PlayerID.IsDead);
        PlayerID.Anim.SetBool("IsUseSkill", PlayerID.IsUseSkill);
        PlayerID.Anim.SetInteger("UseSkill", PlayerID.UseSkill);
    }
}
