using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using FreeNet;

public class Movement : MonoBehaviour
{

    public float Speed;
    public float JumpValue;
    public float RunSpeed;

    public bool IsOnGround;

    public GameObject Player;
    public Rigidbody Rig;
    public KeyCode[] MoveKey;
    public Vector3[] MoveDir;
    public KeyCode RunKey;
    public KeyCode JumpKey;
    public Player PlayerID;

    public Vector3 dir;

    public virtual void NowSpeed()
    {
        dir = Vector3.zero;

        for (int i = 0; i < MoveKey.Length; i++)
        {
            if (Input.GetKey(MoveKey[i]))
            {
                dir += Player.transform.TransformDirection(MoveDir[i]);
            }
        }
    }

    public virtual void Move()
    {
        if(dir == Vector3.zero)
        {
            PlayerID.IsIdle = true;
            PlayerID.IsWalking = false;
            PlayerID.IsRunning = false;

            return;
        }

        if(Input.GetKey(RunKey))
        {
            if (PlayerID.IsUseSkill)
                return;

            PlayerID.IsRunning = true;
            PlayerID.IsWalking = false;
            PlayerID.IsIdle = false;

            if (Input.GetKey(MoveKey[0]) && !Input.GetKey(MoveKey[2]))
            {
                Rig.AddForce(dir.normalized * RunSpeed, ForceMode.Impulse);

                float speed;
                speed = new Vector3(Rig.velocity.x, 0, Rig.velocity.z).magnitude;

                if (speed > 20.0f)
                {
                    float jumpV = Rig.velocity.y;
                    Rig.velocity = new Vector3(Rig.velocity.x, 0, Rig.velocity.z).normalized * 20.0f;
                    Rig.velocity += new Vector3(0, jumpV, 0);
                }
            }
            else
            {
                Rig.AddForce(dir.normalized * Speed, ForceMode.Impulse);

                float speed;
                speed = new Vector3(Rig.velocity.x, 0, Rig.velocity.z).magnitude;

                if (speed > 10.0f)
                {
                    float jumpV = Rig.velocity.y;
                    Rig.velocity = new Vector3(Rig.velocity.x, 0, Rig.velocity.z).normalized * 10.0f;
                    Rig.velocity += new Vector3(0, jumpV, 0);
                }
            }
        }
        else
        {
            if (PlayerID.IsUseSkill)
                return;

            PlayerID.IsRunning = false;
            PlayerID.IsWalking = true;
            PlayerID.IsIdle = false;

            Rig.AddForce(dir.normalized * Speed,ForceMode.Impulse);

            float speed;
            speed = new Vector3(Rig.velocity.x, 0, Rig.velocity.z).magnitude;

            if(speed > 10.0f)
            {
                float jumpV = Rig.velocity.y;
                Rig.velocity = new Vector3(Rig.velocity.x, 0, Rig.velocity.z).normalized * 10.0f;
                Rig.velocity += new Vector3(0, jumpV, 0);
            }
        }   
    }

    public virtual void SetVelocity(Vector3 vec)
    {
        Rig.velocity = vec;
    }

    public virtual void Jump()
    {
        if(Physics.Raycast(Player.transform.position, Player.transform.TransformDirection(Vector3.down), 0.7f) && Rig.velocity.y == 0)
        {
            IsOnGround = true;
        }
        if(Input.GetKeyDown(JumpKey) && IsOnGround)
        {
            Rig.AddForce(Vector3.up * JumpValue, ForceMode.Impulse);
            IsOnGround = false;
        }
    }

    public virtual void Animation()
    {
        Debug.Log("Base Animation");
    }

    public void CollisionOnGround(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            IsOnGround = true;
        }
    }

    public void Send()
    {
        CPacket msg = CPacket.create((short)PROTOCOL.MOVING_REQ);
        msg.push(PlayerID.player_index);


        // position
        msg.push_single(Player.transform.position.x);
        msg.push_single(Player.transform.position.y);
        msg.push_single(Player.transform.position.z);
        
        // rotation
        msg.push_single(Player.transform.rotation.eulerAngles.x);
        msg.push_single(Player.transform.rotation.eulerAngles.y);
        msg.push_single(Player.transform.rotation.eulerAngles.z);

        // Movement Animation Parameter
        msg.push((PlayerID.IsIdle ? (byte)1 : (byte)0));
        msg.push((PlayerID.IsWalking ? (byte)1 : (byte)0));
        msg.push((PlayerID.IsRunning? (byte)1 : (byte)0));
        msg.push((PlayerID.IsDead ? (byte)1 : (byte)0));

        PlayerID.manager.send(msg);
    }
}
