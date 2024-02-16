using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using static UnityEngine.LightAnchor;

public class LinkScpt : MonoBehaviour
{
    #region Instances

    [SerializeField] Rigidbody linkRigidbody;
    [SerializeField] CinemachineCamera cam;
    public Rigidbody RB => linkRigidbody;

    #endregion

    #region Movement

    //Walk
    [SerializeField] float baseSpeed;
    [SerializeField] float speedClimb;

    public float BaseSpeed => baseSpeed;
    public float BaseSpeedClimb => speedClimb;

    //Swim
    bool isInWater;
    public bool IsInWater => isInWater;


    //Jump
    [SerializeField] float jumpforce;
    public float JumpForce => jumpforce;

    //Camera
    [SerializeField] Transform cameraTransform;
    [SerializeField] float turnSmoothVelocity, turnSmoothTime;
    public float TurnSmoothVelocity => turnSmoothVelocity;
    public float TurnSmoothTime => turnSmoothTime;


    //Layers
    [SerializeField] LayerMask climbLayerMask;
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] LayerMask waterLayerMask;

    public LayerMask ClimbMask => climbLayerMask;
    public LayerMask GroundMask => groundLayerMask;
    public LayerMask WaterMask => waterLayerMask;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        linkRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        DynamicFOV();
    }

    private void DynamicFOV()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ChangeFov(70);
        }
        else
        {
            ChangeFov(60);
        }
    }

    public bool IsOnGround()
    {
        return Physics.CheckSphere(transform.position + new Vector3(0, -0.65f, 0), 0.45f, GroundMask);
    }

    public void Jump(Vector3 jumpDirection, float jumpForce)
    {
        linkRigidbody.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
    }

    public void LookAtMoveDirection()
    {
        Vector3 velocity = linkRigidbody.velocity;
        velocity.y = 0;
        velocity.Normalize();
        transform.LookAt(this.transform.position + velocity, transform.up);
    }

    public bool CanClimbCorner()
    {
        if (RayFrontal())
        {
            if (!RayAcimaFrontal() && RayAcima())
            {
                return true;
            }
        }
        return false;
    }

    public bool RayFrontal()
    {
        //Verifica se há uma parede escalavel a frente
        Vector3 rayStartPos = transform.position + new Vector3(0, 1.15f, 0);

        Debug.DrawRay(rayStartPos, transform.forward * 1.3f, Color.green);

        if (Physics.Raycast(rayStartPos, transform.forward, out RaycastHit rayFrontal, 0.8f, ClimbMask)
         || Physics.Raycast(rayStartPos, transform.forward, out rayFrontal, 0.8f, GroundMask))
        {
            return true;
        }
        return false;
    }

    public bool RayAcima()
    {

        //Verifica se há uma parede escalavel a frente
        Vector3 rayStartPos = transform.position + transform.forward * 1f + new Vector3(0f, 1.5f, 0f);

        Debug.DrawRay(rayStartPos, -transform.up * 0.5f, Color.green);

        if (Physics.Raycast(rayStartPos, -transform.up, out RaycastHit rayAcima, 0.2f, ClimbMask)
        || Physics.Raycast(rayStartPos, -transform.up, out rayAcima, 0.2f, GroundMask))
        {
            return true;
        }
        return false;
    }

    public bool RayAcimaFrontal()
    {
        //Verifica se há uma parede escalavel a frente
        Vector3 rayStartPos = transform.position + new Vector3(0f, 1.5f, 0f);

        Debug.DrawRay(rayStartPos, transform.forward * 2.0f, Color.green);

        if (Physics.Raycast(rayStartPos, transform.forward, out RaycastHit rayAcimaFrontal, 2.4f, ClimbMask)
        || Physics.Raycast(rayStartPos, transform.forward, out rayAcimaFrontal, 2.0f, GroundMask))
        {
            return true;
        }
        return false;
    }

    public void ChangeFov(int fov)
    {
        float newFov;
        if(fov > cam.Lens.FieldOfView)
            newFov = Mathf.Lerp(cam.Lens.FieldOfView, fov, 1f * Time.deltaTime);
        else
            newFov = Mathf.Lerp(cam.Lens.FieldOfView, fov, 10f*Time.deltaTime);

        cam.Lens.FieldOfView = newFov;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water")) isInWater = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water")) isInWater = false;
    }
}
