using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlataform : MonoBehaviour
{
    Player_Scp player;

    Vector3 lastPosition, movedPosition;

    private void Update()
    {
        if (player == null)
            return;

        movedPosition = this.transform.position - lastPosition;
        
        player.transform.position += movedPosition;

        lastPosition = this.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<Player_Scp>(out Player_Scp _player))
        {
            player = _player;
            lastPosition = this.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        player = null;
    }
}
