using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public int keyLevel;
    [SerializeField] private GameObject frontPart;

    private Vector2 boxSize1 = Vector2.zero;
    private Vector2 boxSize2 = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        boxSize1 = GetComponent<BoxCollider2D>().size;
        boxSize2 = frontPart.GetComponent<BoxCollider2D>().size;
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
        GetComponent<BoxCollider2D>().size = new Vector2(0, 0);
        frontPart.GetComponent<BoxCollider2D>().size = new Vector2(0, 0);
    }

    public void closeDoor()
    {
        GetComponent<BoxCollider2D>().size = boxSize1;
        frontPart.GetComponent<BoxCollider2D>().size = boxSize2;
    }
}
