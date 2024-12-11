using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    [SerializeField] private Transform self;
    private bool usedCheckpoint = false;
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
        if (collision.gameObject.tag == "Player" && !(usedCheckpoint))
        {
            collision.gameObject.GetComponent<Bird>().checkpoint = self;
            usedCheckpoint = true;
        }
    }
}
