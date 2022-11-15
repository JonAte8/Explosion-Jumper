using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignScript : MonoBehaviour
{
    public GameObject player;
    [TextArea]
    public string text;
    public GameObject display;
    public TextMeshProUGUI displayText;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            display.SetActive(true);
            displayText.text = text;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            display.SetActive(false);
        }
    }
}
