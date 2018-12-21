using UnityEngine;
using System.Collections;
using FreeNet;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CMainTitle : MonoBehaviour
{
    public GameObject[] PlayerObject;
    public byte local_player_index;
    public Queue<Player> PlayerList;

    [SerializeField]
    private Vector3[] TeamSpawnpoints;
    
    enum USER_STATE
    {
        NOT_CONNECTED,
        CONNECTED,
        WAITING_MATCHING
    }

    CNetworkManager network_manager;
    USER_STATE user_state;

    void Start()
    {
        this.user_state = USER_STATE.NOT_CONNECTED;
        PlayerList = new Queue<Player>();
        this.network_manager = GameObject.Find("NetworkManager").GetComponent<CNetworkManager>();
        this.user_state = USER_STATE.NOT_CONNECTED;
        enter();

        DontDestroyOnLoad(gameObject);
    }

    public void enter()
    {
        StopCoroutine("after_connected");

        this.network_manager.message_receiver = this;

        if(!this.network_manager.is_connected())
        {
            this.user_state = USER_STATE.CONNECTED;
            this.network_manager.connect();
        }
        else
        {
            on_connected();
        }
    }

    /// <summary>
    /// 서버에 접속된 이후에 처리할 루프.
    /// 마우스 입력이 들어오면 ENTER_GAME_ROOM_REQ 프로토콜을 요청하고
    /// 중복 요청을 방지하기 위해서 현재 코루틴을 중지시킨다.
    /// </summary>
    /// <returns></returns>
    IEnumerator after_connected()
    {
        // CBattleRoom의 게임 오버 상태에서 마우스 입력을 통해 메인 화면으로
        // 넘어오도록 되어 있는데,
        // 한 프레임 내에서 이 코루틴이 실행될 경우 아직 마우스 입력이
        // 남아있는 것으로 판단되어
        // 메인 화면으로 돌아오자 마자 ENTER_GAME_ROOM_REQ 패킷을 보내는 일이 발생한다.
        // 따라서 강제로 한 프레임을 건너 뛰어 다음 프레임부터 코루틴의 내용이
        // 수행될 수 있도록 한다.
        yield return new WaitForEndOfFrame();
        while (true)
        {
            if (this.user_state == USER_STATE.CONNECTED)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    this.user_state = USER_STATE.WAITING_MATCHING;
                    CPacket msg = CPacket.create((short)PROTOCOL.ENTER_GAME_ROOM_REQ);
                    this.network_manager.send(msg);
                    StopCoroutine("after_connected");
                }
            }
            yield return 0;
        }
    }

    public void on_connected()
    {
        this.user_state = USER_STATE.CONNECTED;

        StartCoroutine("after_connected");
    }

    /// <summary>
    /// 패킷을 수신했을 때 호출됨. 
    /// </summary>
    /// <param name= "protocol"></param>
    /// <param name= "msg"></param>
    public void on_recv(CPacket msg)
    {
        // 제일 먼저 프로토콜 아이디를 꺼내온다.
        PROTOCOL protocol_id = (PROTOCOL)msg.pop_protocol_id();

        Debug.Log("protocol id " + protocol_id);

        switch (protocol_id)
        {
            case PROTOCOL.START_LOADING:
                {
                    //Camera.main.gameObject.SetActive(false);
                    byte player_index = msg.pop_byte();

                    //float HP = msg.pop_Single();
                    //Vector3 vec = new Vector3(msg.pop_Single(), msg.pop_Single(), msg.pop_Single());

                    //GameObject player = Instantiate(Player, new Vector3(), new Quaternion());

                    //Player m = player.GetComponent<Player>();

                    //m.GPlayer.transform.position = vec;
                    //m.player_index = player_index;
                    //m.HP = HP;
                    //player.name = ((int)player_index).ToString();
                    local_player_index = player_index;
                    //m.loading_complete();

                    ////gameObject.SetActive(false);

                    StartCoroutine(SceneTransporter.LoadScene(1));

                }
                break;
            case PROTOCOL.GAME_START:
                {
                    Debug.Log("게임 시작");

                    int playerCounts = (int)msg.pop_byte();

                    for(int i=0; i<playerCounts; i++)
                    {
                        byte player_index = msg.pop_byte();
                        byte Job = msg.pop_byte();
                        byte Team = msg.pop_byte();

                        if (local_player_index == player_index)
                        {
                            GameObject me = GameObject.Find(((int)player_index).ToString());

                            me.GetComponent<Player>().team = (TEAM)Team;

                            continue;
                        }
                        GameObject player = GameObject.Find(((int)player_index).ToString());

                        if(player == null)
                        {
                            Debug.Log(Job + " " + Team);

                            player = Instantiate(PlayerObject[Job - 1], TeamSpawnpoints[Team-1], new Quaternion());
                            player.gameObject.name = ((int)player_index).ToString();
                            player.transform.GetChild(2).gameObject.SetActive(false);
                        }

                        Player p = player.GetComponent<Player>();

                        p.SpawnPoint = TeamSpawnpoints[Team - 1];
                        p.player_index = player_index;
                        p.job = (JOB)Job;
                        p.team = (TEAM)Team;
                    }
                }
                break;

            case PROTOCOL.SERVER_LIST_SEND:
                {
                    int count = msg.pop_int32();

                    ServerListBox.Counts = count;
                }
                break;

            case PROTOCOL.PLAYER_MOVED:
                {
                    byte player_index = msg.pop_byte();
                    if (local_player_index == player_index)
                    {
                        return;
                    }

                    Debug.Log("player_index" + player_index);

                    GameObject player = GameObject.Find(((int)player_index).ToString());

                    float HP = msg.pop_Single();

                    Player p = player.GetComponent<Player>();
                    p.HP = HP;

                    Vector3 vec = new Vector3(msg.pop_Single(), msg.pop_Single(), msg.pop_Single());
                    Quaternion qua = Quaternion.Euler(msg.pop_Single(), msg.pop_Single(), msg.pop_Single());
                    player.transform.GetChild(0).transform.position = vec;
                    player.transform.GetChild(0).transform.rotation = qua;

                    bool idle = msg.pop_byte() == 1 ? true : false;
                    bool walk = msg.pop_byte() == 1 ? true : false;
                    bool run = msg.pop_byte() == 1 ? true : false;
                    bool die = msg.pop_byte() == 1 ? true : false;

                    p.IsIdle = idle;
                    p.IsWalking = walk;
                    p.IsRunning = run;
                    p.IsDead = die;
                }
                break;
            case PROTOCOL.USE_SKILLONE:
                {
                    byte player_index = msg.pop_byte();
                    if (local_player_index == player_index)
                    {
                        return;
                    }

                    BaseSkillScripts player = GameObject.Find(((int)player_index).ToString()).GetComponent<BaseSkillScripts>();

                    player.MainSkill.UseSkill(player.MainSkill);
                }
                break;
            case PROTOCOL.USE_SKILLTWO:
                {
                    byte player_index = msg.pop_byte();
                    if (local_player_index == player_index)
                    {
                        return;
                    }

                    BaseSkillScripts player = GameObject.Find(((int)player_index).ToString()).GetComponent<BaseSkillScripts>();

                    player.SubSkill.UseSkill(player.SubSkill);
                }
                break;
            case PROTOCOL.USE_SKILLTHREE:
                {
                    byte player_index = msg.pop_byte();
                    if (local_player_index == player_index)
                    {
                        return;
                    }

                    BaseSkillScripts player = GameObject.Find(((int)player_index).ToString()).GetComponent<BaseSkillScripts>();

                    player.UltimateSkill.UseSkill(player.UltimateSkill);
                }
                break;
        }
    }
}
