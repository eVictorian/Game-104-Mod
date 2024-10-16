using NUnit.Framework.Constraints;
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
    private float playerTargetLevel = 1;
    private float playerDetectedLevel = 5; //if playerDetectionLevel reaches playerDetectedLevel the player will be spotted

    public int playerLightCollisionLevel = 0; //0 = not in tourch, 1 = in outerbounds of torch, 2 = innerbounds of torch

    [SerializeField] private GameObject Torch;
    private float maxRotation = 75;
    private float minRotation = 0;
    [SerializeField] private float rotationSpeed = 5f;
    private float rotation = 0;

    private string rotateDirection = "up";
    private Quaternion prevRotation;

    [SerializeField] private GameObject Player;
    [SerializeField] private Bird playerScript;

    // Start is called before the first frame update
    void Start()
    {
        //if (facingLeft == true)
        //{
        //    speed *= -1f;
        //}

        prevRotation = transform.rotation;
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
            else if (playerDetectionLevel > playerTargetLevel)
            {
                Vector3 targ = Player.transform.position;
                targ.z = 0f;

                Vector3 objectPos = transform.position;
                targ.x = targ.x - objectPos.x;
                targ.y = targ.y - objectPos.y;

                float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
                Torch.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+180));
                Torch.transform.localScale = new Vector3(Torch.transform.localScale.x , Torch.transform.localScale.y, Torch.transform.localScale.z);

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
            else
            {
                rotateDirection = "up";
                Torch.transform.rotation = new Quaternion(0, 0, 0, 1);
                rotation = 0;
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
        transform.Rotate(0f, 180f, 0f);
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
