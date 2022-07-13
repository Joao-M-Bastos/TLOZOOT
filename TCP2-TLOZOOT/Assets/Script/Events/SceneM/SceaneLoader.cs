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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider collision) {
        if(collision.gameObject.tag == "Player"){
            LoadSceane();
        }
    }

    public void LoadSceane(){
        this.instaciaPlayer.StartingPosCode = toStartPositionCode;
        SceneManager.LoadScene(iLevelToLoad);
    }
}
