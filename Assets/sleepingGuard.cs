using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sleepingGuard : MonoBehaviour


{

    GameObject player;
    private Bird playerScript;
    // Start is called before the first frame update
    void Start()
    {
        player = Bird.player;
        playerScript = player.GetComponent<Bird>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) { playerScript.sendToCheckpoint(); }
    }
}
