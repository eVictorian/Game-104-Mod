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

    [SerializeField] private GameObject Player;
    [SerializeField] private Bird playerScript;
    // Start is called before the first frame update
    void Start()
    {
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
                print("YOU SEE ME");
                playerDetectionLevel += Time.deltaTime * playerLightCollisionLevel;
            }
            else
            {
                playerDetectionLevel = 0;
            }


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


}
