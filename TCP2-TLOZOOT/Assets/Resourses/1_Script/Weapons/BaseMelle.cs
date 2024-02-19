using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMelle : MonoBehaviour
{
    [SerializeField] float cooldownBase, knockback;
    [SerializeField] int damage, id, type;

    private float cooldown, comboCount;



    // Update is called once per frame
    void Update()
    {
        
    }
}
