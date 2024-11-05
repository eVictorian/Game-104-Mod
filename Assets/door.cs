using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public int keyLevel;
    [SerializeField] private GameObject frontPart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) { openDoor(); }
            
    }

    public void openDoor()
    {
        print("t");
        GetComponent<BoxCollider2D>().size = new Vector2(0, 0);
        frontPart.GetComponent<BoxCollider2D>().size = new Vector2(0, 0);
    }
}
