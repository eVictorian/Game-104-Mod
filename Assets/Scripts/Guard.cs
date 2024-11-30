using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Guard : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private GuardRaycast Raycast;

    private float playerDetectionLevel = 0;
    private float playerTargetLevel = 0.75f; //player starts being tracked by the guard
    private float playerDetectedLevel = 3.5f; //if playerDetectionLevel reaches playerDetectedLevel the player will be spotted

    public int playerLightCollisionLevel = 0; //0 = not in tourch, 1 = in outerbounds of torch, 2 = innerbounds of torch

    [SerializeField] private GameObject Torch;
    private float maxRotation = 75;
    private float minRotation = 0;
    [SerializeField] private float rotationSpeed = 5f;
    private float rotation = 0;

    private string rotateDirection = "up";
    private Quaternion prevRotation;
    
    GameObject player;
    private Bird playerScript;

    public bool printlightcollision = false;


    public GameObject detectionBar;
    private bool detectionBarHidden = true;
    private float detectionLevelPercentage;

    [SerializeField] private bool debugging = false;

    AudioSource footsteps;

    // Start is called before the first frame update
    void Start()
    {
        footsteps = GetComponent<AudioSource>();

        detectionBar.SetActive(false);
        prevRotation = transform.rotation;

        player = Bird.player;
        playerScript = player.GetComponent<Bird>();

        speed *= -1f;
    }

    // Update is called once per frame
    void Update()
    {

        if (playerLightCollisionLevel > 0)
        {
            if (playerDetectionLevel >= playerDetectedLevel)
            {
                playerScript.sendToCheckpoint();
                playerDetectionLevel = 0;
                playerLightCollisionLevel = 0;
                footsteps.Play();
            }
            else if (playerDetectionLevel >= playerTargetLevel)
            {
                AimTowardsPlayer();
                if (debugging) 
                { 
                    print(Torch.transform.localEulerAngles.z);
                    print(transform.localEulerAngles.y);

                }
                if ((Torch.transform.localEulerAngles.z < 270 && transform.localEulerAngles.y == 0) || (Torch.transform.localEulerAngles.z > 270 && transform.localEulerAngles.y == 180)) 
                {
                    
                    flip();
                    AimTowardsPlayer();
                }
                if (Torch.transform.localEulerAngles.z >= 360)
                {
                    Torch.transform.localEulerAngles = new Vector3(0, Torch.transform.localEulerAngles.y,0);
                }
                rotateDirection = "tracking";
            }



            if (Raycast.hasLineOfSight())
            {
                
                playerDetectionLevel += Time.deltaTime * playerLightCollisionLevel;
                
            }
           
            else
            {
                playerDetectionLevel = 0;
                footsteps.Play();
            }
            updateDetectionBar();

        }

        else if (playerDetectedLevel > 0) 
        {
            playerDetectionLevel = 0;
            updateDetectionBar();
            
        }

        if (playerDetectionLevel < playerTargetLevel)
        {
            rb.velocity = new Vector2(speed, 0);
            if (rotateDirection == "up")
            {
                if (rotation <= maxRotation)
                {
                    //print("rotating");
                    Torch.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
                    rotation += rotationSpeed * Time.deltaTime * -1;



                }
                else
                {
                    rotateDirection = "down";
                }
            }
            else if (rotateDirection == "down")
            {
                if (rotation >= minRotation)
                {
                    Torch.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime * -1);
                    rotation += rotationSpeed * Time.deltaTime;
                }
                else
                {
                    rotateDirection = "up";
                }
            }
            else if (rotateDirection == "tracking")
            {
                rotateDirection = "up";
                Torch.transform.localRotation = new Quaternion(0, 0, 0, 1);
                rotation = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("flipPoint")) { flip(); }
        else if (collision.gameObject.CompareTag("Player")) { playerScript.sendToCheckpoint(); }

    }

    private void flip()
    {
        speed *= -1f;
        transform.Rotate(0f, 180f, 0f);

    }

    private void AimTowardsPlayer()
    {
        footsteps.Stop();
        //The maths segment of this was copied then modified to work for the torch
        Vector3 targ = player.transform.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        Torch.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));
        Torch.transform.localScale = new Vector3(Torch.transform.localScale.x, Torch.transform.localScale.y, Torch.transform.localScale.z);
    }

    private void updateDetectionBar() 
    {
        detectionLevelPercentage = playerDetectionLevel / playerDetectedLevel;
        detectionBar.GetComponent<DetectionBar>().updateDetectionBar(detectionLevelPercentage);

        if (detectionLevelPercentage == 0 && !detectionBarHidden) 
        { 
            detectionBar.SetActive(false); 
            detectionBarHidden = true;
        }
        else if (detectionLevelPercentage > 0) 
        {
            if (detectionBarHidden) 
            { 
                detectionBar.SetActive(true);
                detectionBarHidden= false;
            }
            detectionBar.GetComponent<DetectionBar>().updateDetectionBar(detectionLevelPercentage);
        }
        
    }

}
