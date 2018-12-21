using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMovement : Movement
{
    // Start is called before the first frame update
    // Use this for initialization
    void Start()
    {
        IsOnGround = true;

        if (PlayerID.Title.local_player_index == PlayerID.player_index)
        {
            InvokeRepeating("ToServer", 0, 0.05f);
           // Player.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }

    void Update()
    {
        if (PlayerID.Title.local_player_index != PlayerID.player_index)
        {
            return;
        }
        if (PlayerID.IsDead)
        {
            return;
        }

        Jump();
        NowSpeed();
        Animation();
    }

    // Update is called once per frame
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


    public override void Animation() // PlayerID.Anim
    {
        PlayerID.Anim.SetBool("IsIdle", PlayerID.IsIdle);
        PlayerID.Anim.SetBool("IsWalking", PlayerID.IsWalking);
        PlayerID.Anim.SetBool("IsRunning", PlayerID.IsRunning);
        PlayerID.Anim.SetBool("IsDead", PlayerID.IsDead);
        PlayerID.Anim.SetBool("IsUseSkill", PlayerID.IsUseSkill);
        PlayerID.Anim.SetInteger("UseSkill", PlayerID.UseSkill);
    }
}
