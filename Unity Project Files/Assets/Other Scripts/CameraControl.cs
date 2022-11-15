using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;
    public float maxDistance;
    public LayerMask mask;
    RaycastHit info;

    private void Start()
    {
    }
    private void FixedUpdate()
    {
        if(Physics.SphereCast(followTarget.transform.position, 0.5f, cam.transform.position - followTarget.transform.position, out info, maxDistance, mask, QueryTriggerInteraction.Ignore))
        {
            cam.transform.localPosition = new Vector3(0, 0, -info.distance);
        }
        else
        {
            cam.transform.localPosition = new Vector3(0, 0, -maxDistance);
        }
    }
}
