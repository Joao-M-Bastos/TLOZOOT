using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform camTransform;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camPosition = new Vector3(camTransform.position.x, transform.position.y, camTransform.position.z);
        Vector3 direction = (transform.position - camPosition).normalized;
        Vector3 forwardMovement = direction * Input.GetAxis("Vertical");
        Vector3 movement = Vector3.ClampMagnitude(forwardMovement, 1);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
}
