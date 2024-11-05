using UnityEngine;


// This script is attached o the player controlled object in the scene.
// Handles input, movement and animation for the player controlled object.
public class Bird : MonoBehaviour 
{
	public float upForce = 8; // the power of the bird's flap.
    public float moveForce = 1;
    [SerializeField]
    private GameObject deathUI;



	private bool isDead = false;			//Has the player collided with a wall?
	private Animator anim;					//Reference to the Animator component.
	private Rigidbody2D rb;               //Holds a reference to the Rigidbody2D component of the bird.
    private SpriteRenderer ren;
    private Vector2 moveVector = Vector2.zero;


    public Transform checkpoint;

    public int accessLevel = 0;

    void Start()
	{
        deathUI.SetActive(false);
		//Get reference to the Animator component attached to this GameObject.
		anim = GetComponent<Animator> ();
		//Get and store a reference to the Rigidbody2D attached to this GameObject.
		rb = GetComponent<Rigidbody2D>();
        //Get and store a reference to the renderer attached to this GameObject.
        ren = GetComponent<SpriteRenderer> ();
    }

    void Update()
	{
        //Don't allow control if the bird has died.
        if (isDead == false) 
		{
            // get left/right movement input...
            float direction = Input.GetAxis("Horizontal");

            if (direction > 0)
            {
                rb.drag = 5;
                rb.AddForce(new Vector2(moveForce, 0));
                ren.flipX = false;
                anim.SetBool("Flap", true);
            }
            else if ( direction <0)
            {
                rb.drag = 5;
                rb.AddForce(new Vector2(-moveForce, 0));
                ren.flipX = true;
                anim.SetBool("Flap", true);
            }
            else
            {
                rb.drag = 0;
            }


            //Look for input to trigger a "flap".
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
            {
                //...tell the animator about it and then...
                anim.SetBool("Flap", true);
                rb.AddForce(new Vector2(0, upForce));
                rb.drag = 5;
            }
            else
            {
                anim.SetBool("Flap", false);           
            }
        }
	}
    // If the bird collides with anything in the scene, it dies.
	void OnCollisionEnter2D(Collision2D other)
	{
  //      //if invisible borders at the edges of the level are tagged,
  //      //they can prevent the player from flying out of bounds.
  //      if (other.gameObject.CompareTag("Border"))
  //          return;

  //      //colliding with anything other than the invisible barriers will kill the player..

		//// Zero out the bird's velocity
		//rb.velocity = Vector2.zero;
		//// If the bird collides with something set it to dead...
		//isDead = true;
		////...tell the Animator about it...
		//anim.SetTrigger ("Die");
  //      Invoke("ShowDeathUI", 1.5f);
	}

    public void sendToCheckpoint()
    {
        transform.position = checkpoint.position;
    }

    void ShowDeathUI()
    {
        deathUI.SetActive(true);
    }
}
