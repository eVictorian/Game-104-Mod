using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keycard : MonoBehaviour
{
    [SerializeField] private int cardLevel;
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
            if (collision.gameObject.GetComponent<Bird>().accessLevel < cardLevel) 
            { 
                collision.gameObject.GetComponent<Bird>().updateAccessLevel(cardLevel);
                collision.GetComponent<Bird>().playPickup();
                Destroy(this.gameObject);
            }
        }
    }
}
