using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkScpt : MonoBehaviour
{
    #region Instances

    public Rigidbody linkRigidbody;

    #endregion

    #region Movement

    //Walk
    [SerializeField] public float baseSpeed;
    [SerializeField] public float speedClimb;
    

    //Jump
    public float jumpforce;

    //Camera
    [SerializeField] Transform cameraTransform;
    [SerializeField] float turnSmoothVelocity, turnSmoothTime;
    public float TurnSmoothVelocity => turnSmoothVelocity;
    public float TurnSmoothTime => turnSmoothTime;


    //Layers
    public LayerMask climbLayerMask;
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
}
