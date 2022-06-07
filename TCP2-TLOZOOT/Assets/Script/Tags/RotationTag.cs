using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTag : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.tag == "Rotation"){
            this.transform.Rotate(new Vector3(0, 0, 36) * Time.deltaTime);
        }
    }
}
