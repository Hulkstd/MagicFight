using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInformation : MonoBehaviour {
    public float Damage;
    public byte player_index;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");

        if (other.tag == "Player")
        {
            Player player = other.gameObject.transform.parent.GetComponent<Player>();

            player.Damaged(Damage, player_index);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");

        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.transform.parent.GetComponent<Player>();

            player.Damaged(Damage, player_index);
        }
    }
}
