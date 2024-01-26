using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.LightAnchor;

public class LinkScpt : MonoBehaviour
{
    #region Instances

    [SerializeField] Rigidbody linkRigidbody;

    public Rigidbody RB => linkRigidbody;

    #endregion

    #region Movement

    //Walk
    [SerializeField] float baseSpeed;
    [SerializeField] float speedClimb;

    public float BaseSpeed => baseSpeed;
    public float BaseSpeedClimb => speedClimb;


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

    public LayerMask ClimbMask => climbLayerMask;
    public LayerMask GroundMask => groundLayerMask;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        linkRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
