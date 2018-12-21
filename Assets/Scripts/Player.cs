using UnityEngine;
using System.Collections;
using FreeNet;

public class Player : MonoBehaviour
{
    public GameObject GPlayer;
    public byte player_index;
    public float HP;
    public CNetworkManager manager;
    public CMainTitle Title;
    public Vector3 SpawnPoint;
    public Animator Anim;
    public Camera Cam;

    public bool IsWalking;
    public bool IsRunning;
    public bool IsIdle;
    public bool IsDead;
    public bool IsUseSkill;
    public int  UseSkill;

    public TEAM team;
    public JOB job;

    public void Initialize()
    {
        manager = GameObject.Find("NetworkManager").GetComponent<CNetworkManager>();
        Title = GameObject.Find("MainTitle").GetComponent<CMainTitle>();
        IsDead = false;
    }

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        if (HP <= 0)
        {
            Dead();
            CPacket msg = CPacket.create((short)PROTOCOL.PLAYER_DEAD);
            msg.push(player_index);

            manager.send(msg);
        }

        if(transform.GetChild(0).position.y <= -50)
        {
            Dead();
            CPacket msg = CPacket.create((short)PROTOCOL.PLAYER_DEAD);
            msg.push(player_index);

            manager.send(msg);
        }

    }

    public void loading_complete()
    {
        CPacket msg = CPacket.create((short)PROTOCOL.LOADING_COMPLETED);
        //msg.push(player_index);
        msg.push((byte)job);
        manager = GameObject.Find("NetworkManager").GetComponent<CNetworkManager>();
        manager.send(msg);
    }

    public void Damaged(float damage, byte enemy_index)
    {
        if (IsDead)
            return;
        if (this.player_index != enemy_index)
        {
            HP -= damage;

            CPacket msg = CPacket.create((short)PROTOCOL.DAMAGED);
            msg.push(this.player_index);
            msg.push_single(HP);

            manager.send(msg);
        }
    }

    public void Dead()
    {
        IsDead = true;

        IsIdle = false;
        IsWalking = false;
        IsRunning = false;
        IsUseSkill = false;
        
        Anim.SetBool("IsIdle", IsIdle);
        Anim.SetBool("IsWalking", IsWalking);
        Anim.SetBool("IsRunning", IsRunning);
        Anim.SetBool("IsDead", IsDead);
        Anim.SetBool("IsUseSkill", IsUseSkill);
        Anim.SetInteger("UseSkill", UseSkill);

        UseSkill = 0;

        /* 사먕시 다른플레이어 관전 가능(미구현) */
        HP = 150;
        CPacket msg = CPacket.create((short)PROTOCOL.DAMAGED);
        msg.push(this.player_index);
        msg.push_single(HP);

        manager.send(msg);


        StartCoroutine("Respawn");
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(4.0f);
        
        transform.GetChild(0).position = SpawnPoint;
        HP = 150;
        IsDead = false;

        yield return null;
    }
}
