using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    //LIST OF ALL VARIABLES

    public Rigidbody rb;
    public Animator anim;
    public bool grounded;
    public Vector3 groundNormal;
    public bool isInGravity;
    public float gravityTurnSpeed;
    public float gravityTurnMultiplier;

    Controls inputActions;
    Vector2 input;
    float tilt_mag;
    float input_forward;
    float input_right;
    public bool aPressed;
    public bool bPressed;
    public bool xPressed;
    public bool yPressed;
    public bool zPressed;
    public Camera cameraObject;
    public Transform followTarget;
    public Transform marioRotationObject;
    public Vector3 inputVector3;
    public Vector3 relativeToCameraInputVector3;
    public int jumpCounter = 0;
    public float jumpTimer;
    public Coroutine courotine;
    public float groundedGravity;
    public float jumpGravity;
    public float currentGravity;
    public float terminalVelocity;
    public float sideAcceleration;

    public bool wallHit;
    public Vector3 wallNormal;

    public Transform ledgeCastObject;
    public bool lowLedgeHit;
    public Vector3 lowLedgeNormal;
    public bool highLedgeHit;
    public Vector3 highLedgeForward;
    public LayerMask ledgeMask;
    public Vector3 pullUpPoint;

    Vector3 ground_Velocity;
    public float groundSpeed;
    Vector3 jump_Velocity;
    public float jumpSpeed;
    [SerializeReference]
    Vector3 side_air_velocity;
    public float sideAirSpeed;

    Vector2 cameraInput;
    public float cameraFollowSpeed;
    public float cameraSpeed;
    public float maxCameraHeight;
    public float minCameraHeight;

    public Transform shellPosition;
    public bool shellGrounded;
    public bool useRB = true;

    public ParticleSystem dustParticles;
    public ParticleSystem[] explosion;
    public bool startExplosion;
    public bool dead;
    public GameObject[] destroyObjects;
    public Animator canvasAnim;
    public universalScript universalObject;



    //LIST OF ALL GETERS/SETTERS
    public Vector3 GroundNormal { get { return groundNormal; } set { groundNormal = value; } }
    public Vector2 Input { get { return input; }}
    public float Tilt_Mag { get { return tilt_mag; }}
    public float Input_Forward { get { return input_forward; } }
    public float Input_Right { get { return input_right; } }
    public Vector3 Ground_Velocity { get { return ground_Velocity; } set { ground_Velocity = value; } }
    public Vector3 Jump_Velocity { get { return jump_Velocity; } set { jump_Velocity = value; } }
    public Vector3 Side_Air_Velocity { get { return side_air_velocity; } set { side_air_velocity = value; } }

    PlayerBaseState currentState;
    public string currentStateName;
    public PlayerStateFactory states;

    public PlayerBaseState CurrentState { get { return currentState; } set { currentState = value; } }

    private void Awake()
    {
        inputActions = new Controls();
        inputActions.PlayerMovement.Move.started += MovementInput;
        inputActions.PlayerMovement.Move.performed += MovementInput;
        inputActions.PlayerMovement.Move.canceled += MovementInput;
        inputActions.PlayerMovement.AButton.started += JumpInput;
        inputActions.PlayerMovement.AButton.canceled += JumpInput;
        inputActions.PlayerMovement.Camera.started += cameraMovement;
        inputActions.PlayerMovement.Camera.performed += cameraMovement;
        inputActions.PlayerMovement.Camera.canceled += cameraMovement;
        inputActions.PlayerMovement.ZButton.started += CrouchButtonInput;
        inputActions.PlayerMovement.ZButton.canceled += CrouchButtonInput;
        inputActions.PlayerMovement.BButton.started += BButtonInput;
        inputActions.PlayerMovement.BButton.canceled += BButtonInput;
        inputActions.PlayerMovement.XButton.started += XButtonInput;
        inputActions.PlayerMovement.XButton.canceled += XButtonInput;
        inputActions.PlayerMovement.YButton.started += YButtonInput;
        inputActions.PlayerMovement.YButton.canceled += YButtonInput;
        states = new PlayerStateFactory(this);
        currentState = states.Grounded();
        currentState.EnterState();
    }

    void MovementInput(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        input = new Vector2(Mathf.Clamp(input.x, -1, 1), Mathf.Clamp(input.y, -1, 1));
        tilt_mag = input.magnitude;
        inputVector3.x = input.x;
        inputVector3.z = input.y;
    }

    void JumpInput(InputAction.CallbackContext context)
    {
        if(context.ReadValueAsButton() == true)
        {
            currentState.APressFunction();
        }
        aPressed = context.ReadValueAsButton();
    }

    void cameraMovement(InputAction.CallbackContext context)
    {
        cameraInput = context.ReadValue<Vector2>();
    }

    void CrouchButtonInput(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton() == true)
        {
            currentState.ZPressFunction();
        }
        zPressed = context.ReadValueAsButton();
    }

    void BButtonInput(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton() == true)
        {
            currentState.BPressFunction();
        }
        bPressed = context.ReadValueAsButton();
    }

    void XButtonInput(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton() == true)
        {
            currentState.XPressFunction();
        }
        xPressed = context.ReadValueAsButton();
    }

    void YButtonInput(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton() == true)
        {
            currentState.YPressFunction();
        }
        yPressed = context.ReadValueAsButton();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (startExplosion)
        {
            for(int i = 0; i < explosion.Length; i++)
            {
                explosion[i].transform.parent = null;
                explosion[i].Play();
            }
            cameraObject.transform.parent = null;
            cameraObject.GetComponent<CameraControl>().enabled = false;
            canvasAnim.SetBool("transition", true);
            universalObject.StartCoroutine(universalObject.LoadLevel(1));
            for (int i = destroyObjects.Length - 1; i > -1; i--)
            {
                Destroy(destroyObjects[i]);
            }
            startExplosion = false;
        }
        if(transform.position.y < -100 && !dead)
        {
            dead = true;
            startExplosion = true;
        }
        if (!isInGravity)
        {
            GroundNormal = Vector3.up;
        }
        Vector3 dot = Vector3.Cross(transform.up, groundNormal);
        float angle = Vector3.Angle(transform.up, groundNormal) < gravityTurnSpeed ? Vector3.Angle(transform.up, groundNormal) * Time.deltaTime : gravityTurnSpeed * Time.deltaTime;
        angle = Vector3.SignedAngle(transform.up, groundNormal, dot) < 0 ? -angle : angle;
        transform.RotateAround(transform.position, dot, angle * gravityTurnMultiplier);
        marioRotationObject.transform.up = groundNormal;
        followTarget.position = Vector3.MoveTowards(followTarget.transform.position, transform.position, cameraFollowSpeed * Time.deltaTime);
        cameraObject.transform.eulerAngles = marioRotationObject.eulerAngles;
        float camAngle = Vector3.SignedAngle(cameraObject.transform.forward, Vector3.ProjectOnPlane(transform.position - cameraObject.transform.position, groundNormal), groundNormal);
        cameraObject.transform.RotateAround(cameraObject.transform.position, groundNormal, camAngle);
        float camXAngle = Vector3.SignedAngle(cameraObject.transform.forward, transform.position - cameraObject.transform.position, cameraObject.transform.right);
        cameraObject.transform.RotateAround(cameraObject.transform.position, cameraObject.transform.right, camXAngle);
        Vector3 test = followTarget.localEulerAngles + new Vector3(-cameraInput.y, cameraInput.x, 0) * cameraSpeed * Time.deltaTime;
        if(minCameraHeight > maxCameraHeight)
        {
            if(test.x > minCameraHeight)
            {
                test = new Vector3(Mathf.Clamp(test.x, minCameraHeight, 361), test.y, test.z);
            }
            else if(test.x < maxCameraHeight)
            {
                test = new Vector3(Mathf.Clamp(test.x, -1, maxCameraHeight), test.y, test.z);
            }
            else
            {
                if(minCameraHeight - test.x < test.x - maxCameraHeight)
                {
                    test = new Vector3(minCameraHeight, test.y, test.z);
                }
                else
                {
                    test = new Vector3(maxCameraHeight, test.y, test.z);
                }
            }
        }
        followTarget.localEulerAngles = test;
        relativeToCameraInputVector3 = cameraObject.transform.rotation * inputVector3;
        Vector3 projectTest = Vector3.ProjectOnPlane(relativeToCameraInputVector3, groundNormal);
        if(projectTest != Vector3.zero)
        {
            ledgeCastObject.transform.forward = projectTest;
        }
        else
        {
            ledgeCastObject.transform.forward = transform.forward;
        }
        input_forward = Vector3.Dot(transform.forward, projectTest);
        input_right = Vector3.Dot(transform.right, projectTest);
        currentStateName = currentState.StateName;
        currentState.UpdateState();
        groundSpeed = ground_Velocity.magnitude * Time.deltaTime;
        jumpSpeed = jump_Velocity.magnitude * Time.deltaTime;
        sideAirSpeed = side_air_velocity.magnitude * Time.deltaTime;
        Vector3 totalVelocity = Ground_Velocity + Jump_Velocity + Side_Air_Velocity;
        if (useRB)
        {
            rb.velocity = totalVelocity * Time.deltaTime;
        }
    }

    private void OnEnable()
    {
        inputActions.PlayerMovement.Enable();
    }

    private void OnDisable()
    {
        inputActions.PlayerMovement.Disable();
    }

    public IEnumerator jumpTimerReset()
    {
        yield return new WaitForSeconds(jumpTimer);
        jumpCounter = 0;
    }

    public Vector3 jumpMovement(float forwardClampMin, float forwardClampMax, float runSpeed)
    {
        float clampedThingForward = Mathf.Clamp(Input_Forward, forwardClampMin, forwardClampMax);
        Vector3 forwardMovement = Vector3.zero;
        if(Input_Forward > 0.3f || Input_Forward < -0.3f)
        {
            forwardMovement = Vector3.MoveTowards(forwardMovement, clampedThingForward * runSpeed * transform.forward, runSpeed);
        }
        else
        {
            forwardMovement = Vector3.MoveTowards(forwardMovement, Vector3.zero, runSpeed);
        }
        return forwardMovement;
    }
}
