using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Cube : Movement
{
    // Use this for initialization
    void Start()
    {
        IsOnGround = true;
        
        if (PlayerID.Title.local_player_index == PlayerID.player_index)
        {   
            InvokeRepeating("ToServer", 0, 0.05f);
            Player.GetComponent<MeshRenderer>().material.color = Color.blue;
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

    private void OnCollisionEnter(Collision collision)
    {
        //CollisionOnGround(collision);
    }
}
