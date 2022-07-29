using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public Combat playerCombat;


    private Combat enemyCombat;
    public float life = 3;

    void Awake()
    {
        this.playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>();
        Destroy(gameObject, life);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyCombat = collision.gameObject.GetComponent<Combat>();
            enemyCombat.TakeKnockback(playerCombat.knockbackforce, playerCombat.transform.TransformDirection(Vector3.forward));
            enemyCombat.TakeDamage(playerCombat.GiveDamage(), playerCombat.dmgModifier);
        }
        //Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
