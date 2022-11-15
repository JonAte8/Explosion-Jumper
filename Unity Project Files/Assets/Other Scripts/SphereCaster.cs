using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCaster : MonoBehaviour
{

    public GameObject currentHitObject;

    public float sphereRadius;
    public float maxDistance;
    public LayerMask layerMask;

    private Vector3 origin;
    public string direction = "forward";
    private Vector3 actualDirection;
    public bool hitSomething;
    public string valueName;
    public PlayerStateMachine machine;
    private float currentHitDistance;

    // Start is called before the first frame update
    void Start()
    {
        if(machine == null)
        {
            machine = GameObject.Find("Player").GetComponent<PlayerStateMachine>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        origin = transform.position;
        switch (direction)
        {
            case "forward":
                actualDirection = transform.forward;
                break;
            case "back":
                actualDirection = -transform.forward;
                break;
            case "up":
                actualDirection = transform.up;
                break;
            case "down":
                actualDirection = -transform.up;
                break;
            case "right":
                actualDirection = transform.right;
                break;
            case "left":
                actualDirection = -transform.right;
                break;
        }
        RaycastHit hit;
        if(Physics.SphereCast(origin, sphereRadius, actualDirection, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal))
        {
            Gizmos.color = Color.green;
            currentHitObject = hit.transform.gameObject;
            currentHitDistance = hit.distance;
            hitSomething = true;
        }
        else
        {
            Gizmos.color = Color.red;
            currentHitDistance = maxDistance;
            currentHitObject = null;
            hitSomething = false;
        }
        if(valueName == "grounded")
        {
            machine.grounded = hitSomething;
        }
        else if(valueName == "wallhit")
        {
            machine.wallHit = hitSomething;
            machine.wallNormal = hit.normal;
        }
        else if(valueName == "lowledge")
        {
            machine.lowLedgeHit = hitSomething;
            machine.lowLedgeNormal = hit.normal;
        }
        else if(valueName == "highledge")
        {
            machine.highLedgeHit = hitSomething;
            machine.highLedgeForward = transform.forward;
        }
        else if(valueName == "shellGrounded")
        {
            machine.shellGrounded = hitSomething;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + actualDirection * currentHitDistance);
        Gizmos.DrawWireSphere(origin + actualDirection * currentHitDistance, sphereRadius);
    }
}
