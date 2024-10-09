using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Adapted from a tutorial by Dani: https://www.youtube.com/watch?v=zit45k6CUMk




public class ScrollingBackground : MonoBehaviour
{
    public float parallaxAmount=1;
    static Transform targetTransform;
    private float startPos, length;
    


    private void Awake()
    {
        if (targetTransform == null)
        {
            //targetTransform = Camera.main.transform;
            targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer> ().bounds.size.x;
    }

    private void Update()
    {
        float camDelta = targetTransform.position.x * (1 - parallaxAmount);
        float dist = targetTransform.position.x * parallaxAmount;     
 
        if(camDelta>startPos+length) 
        { 
            startPos += length*2; 
        }
        else if(camDelta<startPos-length) { startPos -= length*2; }

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
    }
}
