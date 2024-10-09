using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Guard : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private bool facingLeft = false;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private GuardRaycast Raycast;

    private float playerDetectionLevel = 0;
    private float playerDetectedLevel = 4; //if playerDetectionLevel reaches playerDetectedLevel the player will be spotted

    public int playerLightCollisionLevel = 0; //0 = not in tourch, 1 = in outerbounds of torch, 2 = innerbounds of torch

    [SerializeField] private GameObject Torch;
    private float maxRotation = 75;
    private float minRotation = 0;
    [SerializeField] private float rotationSpeed = 5f;
    private float rotation = 0;

    private string rotateDirection = "up";

    // Start is called before the first frame update
    void Start()
    {
        if (facingLeft == true){
            speed *= -1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speed, 0);
        if (playerLightCollisionLevel > 0)
        {
            if (playerDetectionLevel >= playerDetectedLevel)
            {
                print("PLAYER SPOTTED");
                playerDetectionLevel = 0;
            }

            if (Raycast.hasLineOfSight())
            {
                playerDetectionLevel += Time.deltaTime * playerLightCollisionLevel;
            }
            else 
            {
                playerDetectionLevel = 0;
            }

            
        }

        print(rotation + " max: "+ maxRotation);
        print(rotateDirection);
        //print(Torch.transform.localRotation.z >= maxRotation);
        if (rotateDirection == "up")
        {
            if (rotation <= maxRotation)
            {
                print("rotating");
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
                Torch.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime*-1);
                rotation += rotationSpeed * Time.deltaTime;
            }
            else
            {
                rotateDirection = "up";
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        flip();

    }

    private void flip()
    {
        speed *= -1f;
        transform.Rotate(0f,180f,0f);
        if (facingLeft == true)
        {
            facingLeft = false;
        }
        else
        {
            facingLeft = true;
        }
    }

}
