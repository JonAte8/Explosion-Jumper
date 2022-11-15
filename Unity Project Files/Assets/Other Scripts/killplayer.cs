using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killplayer : MonoBehaviour
{

    public PlayerStateMachine player;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject == player.gameObject)
        {
            player.startExplosion = true;
        }
    }
}
