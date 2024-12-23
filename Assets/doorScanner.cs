using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScanner : MonoBehaviour
{
    private int requiredKey;
    // Start is called before the first frame update
    void Start()
    {
        requiredKey = GetComponentInParent<door>().keyLevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            {
            if (collision.gameObject.GetComponent<Bird>().accessLevel >= requiredKey)
            {
                GetComponentInParent<door>().openDoor();
            }
            else
            {
                GetComponentInParent<door>().rejected();
            }
        }
    }
}
