using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceaneLoader : MonoBehaviour
{
    public Player_Scp instaciaPlayer;

    public int iLevelToLoad;

    public int toStartPositionCode;


    private void Awake() {
        this.instaciaPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Scp>();
    }

    private void OnTriggerStay(Collider collision) {
        if(collision.gameObject.CompareTag("Player"))
        {
            LoadSceane();
        }
    }

    public void LoadSceane(){
        this.instaciaPlayer.StartingPosCode = toStartPositionCode;
        SceneManager.LoadScene(iLevelToLoad);
    }
}
