using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellScript : MonoBehaviour
{
    PlayerStateMachine machine;
    public bool onShell;
    MeshCollider col;

    private void Start()
    {
        col = GetComponent<MeshCollider>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            if (!onShell)
            {
                machine = collision.gameObject.GetComponent<PlayerStateMachine>();
                machine.shellPosition = transform;
                machine.useRB = false;
                Physics.IgnoreCollision(collision.collider, col, true);
                machine.CurrentState.SwitchState(machine.states.ShellRiding());
                onShell = true;
            }
        }
    }

    private void Update()
    {
        if (onShell)
        {
            machine.shellPosition = transform;
        }
    }
}
