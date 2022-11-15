using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public GameObject player;
    public universalScript universal;
    public int lev;
    public Color color;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            universal.loadLevelFunction(lev);
            universal.setColor(color);
            Destroy(gameObject);
        }
    }
}
