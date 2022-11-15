using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySphere : MonoBehaviour
{
    

    public PlayerStateMachine player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player.isInGravity = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            player.GroundNormal = (player.transform.position - transform.position).normalized;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player.isInGravity = false;
        }
    }
}
