using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RotateObject : MonoBehaviour {

    public float RotateSensitivity = 50.0f; 

    public GameObject Rotation;
    public GameObject Player;
    public Player PlayerID;

    // Use this for initialization
    void Start () {
        Cursor.visible = false;
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (PlayerID.IsDead)
            return;

        if (PlayerID.Title.local_player_index != PlayerID.player_index)
        {
            //transform.GetChild(2).gameObject.SetActive(false);
            return;
        }

        try
        {
            Rotation.transform.position = Player.transform.position;
            Rotation.transform.rotation = Player.transform.rotation;

            PlayerID.Cam.transform.position = Vector3.Lerp(PlayerID.Cam.transform.position, Rotation.transform.TransformPoint(new Vector3(0, 4, -8)), Time.deltaTime * 3);

            Player.transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * RotateSensitivity, Space.World);

            RaycastHit raycastHit;

            Vector3 vec = Player.transform.TransformPoint(new Vector3(0, 4, -8));

            if (Physics.Raycast(vec, Rotation.transform.TransformDirection(Vector3.forward), out raycastHit, 5))
            {
                if (raycastHit.transform.gameObject.tag != "Player")
                {
                    PlayerID.Cam.transform.position = raycastHit.point;

                    /*if (transform.localPosition.z > 0)
                    {
                        Cam.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
                    }*/
                    //Cam.transform.position = transform.position;
                }
            }
            else
            {
                //transform.localPosition = new Vector3(0, 2, Offset);
            }

            PlayerID.Cam.transform.LookAt(Player.transform.position + new Vector3(0.0f, 3.0f, 0.0f));

        }
        catch(Exception e)
        {
            Debug.Log("Error " + e.Message);
        }
    }
}
