using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    private GameObject[] carry;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded(){
        carry = GameObject.FindGameObjectsWithTag("doNotRepeat");

        if(carry.Length > 1){
            Destroy(carry[1]);
        }
    }
}
