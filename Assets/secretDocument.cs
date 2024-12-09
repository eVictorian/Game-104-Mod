using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secretDocument : MonoBehaviour
{
    [SerializeField] UIController UI;
    [SerializeField] LevelComplete LevelComplete;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            
        {
            UI.timerActive = true;
            LevelComplete.levelCompleted = true;

            collision.GetComponent<Bird>().playPickup();
            
            Destroy(gameObject);

        }
    }



}
