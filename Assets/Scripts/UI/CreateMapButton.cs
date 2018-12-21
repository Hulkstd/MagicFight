using UnityEngine;
using System.Collections;
using FreeNet;
using FreeNetUnity;

public class CreateMapButton : MonoBehaviour
{
    public CMainTitle Title;
    public CNetworkManager Manager;

    public void CreateRoom()
    {
        CPacket packet = CPacket.create((short)PROTOCOL.CREATE_GAME_ROOM_REQ);

        Manager.send(packet);
    }
}
