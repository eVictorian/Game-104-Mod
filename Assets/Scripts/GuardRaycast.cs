using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardRaycast : MonoBehaviour
{
    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }
     public bool hasLineOfSight()
    {
        bool canSee = false;
        Vector2 endPos = player.transform.position;

        RaycastHit2D hit = Physics2D.Linecast(transform.position,endPos,1 << LayerMask.NameToLayer("Action"));
        Debug.DrawLine(transform.position, player.transform.position, Color.red);

        

        if (hit.collider != null)
        {
            
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                canSee = true;
            }
        }
        

        return canSee;
    }
}
