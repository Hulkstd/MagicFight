using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FreeNetUnity;
using FreeNet;

public class CNetworkManager : MonoBehaviour {

    CFreeNetUnityService gameserver;
    string received_msg;

    public MonoBehaviour message_receiver;

    void Awake()
    {
        this.received_msg = " ";

        this.gameserver = gameObject.AddComponent<CFreeNetUnityService>();

        this.gameserver.appcallback_on_status_changed += on_status_changed;

        this.gameserver.appcallback_on_message += on_message;


        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        connect();   
    }

    public void connect()
    {
        this.gameserver.connect("127.0.0.1", 7979);
    }   

    public bool is_connected()
    {
        return this.gameserver.is_connected();
    }

    void on_status_changed(NETWORK_EVENT status)
    {
        switch(status)
        {
            case NETWORK_EVENT.connected:
                {
                    Debug.Log("on connected");
                    this.received_msg += "on connected\n";
                    GameObject.Find("MainTitle").GetComponent<CMainTitle>().on_connected();
                }
                break;

            case NETWORK_EVENT.disconnected:
                {
                    Debug.Log("disconnected");
                    this.received_msg += "disconnected\n";
                }
                break;
        }
    }

    void on_message(CPacket msg)
    {
        this.message_receiver.SendMessage("on_recv", msg);
    }

    public void send(CPacket msg)
    {
        this.gameserver.send(msg);
    }
}
