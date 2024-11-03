using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardInnerDetection : MonoBehaviour
{
    [SerializeField] private Guard guardScript;


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
            guardScript.playerLightCollisionLevel += 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            guardScript.playerLightCollisionLevel -= 1;
        }
    }
}
