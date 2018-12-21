using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Observing : MonoBehaviour
{
    public Player PlayerID;
    private Player Index;

    private void FixedUpdate()
    {
        if (PlayerID.player_index != PlayerID.Title.local_player_index)
        {
            if(!PlayerID.Title.PlayerList.Contains(PlayerID))
            {
                PlayerID.Title.PlayerList.Enqueue(PlayerID);
            }
            return;
        }

        if (!PlayerID.IsDead)
        {
            if(Index)
            {
                Index.Cam.gameObject.SetActive(false);
            }
            PlayerID.Cam.gameObject.SetActive(true);
            Index = null;
            return;
        }

        
        if(Index == null)
        {
            Debug.Log("Find Camera");

            Index = FindAlivePlayer(Index);

            Debug.Log(Index);
        }

        PlayerID.Cam.gameObject.SetActive(false);
        Index.Cam.gameObject.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Index.Cam.gameObject.SetActive(false);
            Index = FindAlivePlayer(Index);
        }
    }

    Player FindAlivePlayer(Player NowIndex)
    {
        Player result = null;

        var firstP = PlayerID.Title.PlayerList.Peek();

        while(firstP != PlayerID.Title.PlayerList.Peek())
        {
            Player nowP = PlayerID.Title.PlayerList.Peek();

            if(!nowP.IsDead)
            {
                if(nowP != NowIndex)
                {
                    result = nowP;
                    PlayerID.Title.PlayerList.Dequeue();
                    PlayerID.Title.PlayerList.Enqueue(result);
                    break;
                }
            }
            
            PlayerID.Title.PlayerList.Dequeue();
            PlayerID.Title.PlayerList.Enqueue(nowP);
        }
        return result;
    }
}
