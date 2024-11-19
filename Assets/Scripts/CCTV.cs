using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTV : MonoBehaviour
{

    [SerializeField] private GuardRaycast Raycast;

    private float playerDetectionLevel = 0;
    private float playerDetectedLevel = 1; //if playerDetectionLevel reaches playerDetectedLevel the player will be spotted

    public int playerLightCollisionLevel = 0; //0 = not in tourch, 1 = in outerbounds of torch, 2 = innerbounds of torch

    [SerializeField] private GameObject Torch;
    private float maxRotation = 90;
    private float minRotation = 0;
    private float rotationSpeed = 20f;
    private float rotation = 0;

    private string rotateDirection = "clockwise";
    private Quaternion prevRotation;

    GameObject player;
    private Bird playerScript;

    public GameObject detectionBar;
    private bool detectionBarHidden = true;
    private float detectionLevelPercentage;
    // Start is called before the first frame update
    void Start()
    {
        player = Bird.player;
        playerScript = player.GetComponent<Bird>();
        detectionBar.SetActive(false);
        Torch.transform.Rotate(0f, 0f, -45);
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
            }

            if (Raycast.hasLineOfSight())
            {
                playerDetectionLevel += Time.deltaTime * playerLightCollisionLevel;
            }
            else
            {
                playerDetectionLevel = 0;
            }
            updateDetectionBar();

        }

        else if (playerDetectedLevel > 0)
        {
            playerDetectionLevel = 0;
            updateDetectionBar();
        }
        if (rotateDirection == "clockwise")
        {
            if (rotation <= maxRotation)
            {
                Torch.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
                rotation += rotationSpeed * Time.deltaTime;



            }
            else
            {
                rotateDirection = "antiClockwise";
            }
        }
        else if (rotateDirection == "antiClockwise")
        {
            if (rotation >= minRotation)
            {
                Torch.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime * -1);
                rotation += rotationSpeed * Time.deltaTime *-1;
            }
            else
            {
                rotateDirection = "clockwise";
            }
         }
        
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
                detectionBarHidden = false;
            }
            detectionBar.GetComponent<DetectionBar>().updateDetectionBar(detectionLevelPercentage);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) { playerScript.sendToCheckpoint(); }
    }


}
