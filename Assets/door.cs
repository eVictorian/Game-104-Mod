using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class door : MonoBehaviour
{
    public int keyLevel;
    [SerializeField] private GameObject frontPart;

    [SerializeField] private AudioSource doorOpeningSFX;
    [SerializeField] public AudioSource rejectedSFX;

    private Vector2 boxSize1 = Vector2.zero;
    private Vector2 boxSize2 = Vector2.zero;

    [SerializeField]private TextMeshProUGUI rejectMessage;
    private float showMessageCountdown;

    // Start is called before the first frame update
    void Start()
    {
        boxSize1 = GetComponent<BoxCollider2D>().size;
        boxSize2 = frontPart.GetComponent<BoxCollider2D>().size;
        rejectMessage.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (showMessageCountdown > 0) 
        {
            showMessageCountdown -= Time.deltaTime;
        }
        else if (rejectMessage.text != "")
        {
            rejectMessage.text = "";
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        { 
            openDoor(); 
        }
            
    }

    public void openDoor()
    {
        GetComponent<BoxCollider2D>().size = new Vector2(0, 0);
        GetComponent<SpriteRenderer>().enabled = false;
        frontPart.GetComponent<BoxCollider2D>().size = new Vector2(0, 0);
        doorOpeningSFX.Play();
    }

    public void closeDoor()
    {
        GetComponent<BoxCollider2D>().size = boxSize1;
        GetComponent<SpriteRenderer>().enabled = true;
        frontPart.GetComponent<BoxCollider2D>().size = boxSize2;
    }
    public void rejected()
    {
        rejectedSFX.Play();
        rejectMessage.text = ("Level "+ keyLevel.ToString()+" keycard required");
        showMessageCountdown = 3;
    }
}
