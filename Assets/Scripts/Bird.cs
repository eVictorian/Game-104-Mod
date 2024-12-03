using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


// This script is attached o the player controlled object in the scene.
// Handles input, movement and animation for the player controlled object.
public class Bird : MonoBehaviour 
{
	public float upForce = 8; // the power of the bird's flap.
    public float moveForce = 1;
    [SerializeField]
    private GameObject deathUI;

    public static GameObject player;

    private bool isDead = false;			//Has the player collided with a wall?
	private Animator anim;					//Reference to the Animator component.
	private Rigidbody2D rb;               //Holds a reference to the Rigidbody2D component of the bird.
    private SpriteRenderer ren;
    private Vector2 moveVector = Vector2.zero;


    public Transform checkpoint;

    public int accessLevel = 0;

    public string currentItem = null;

    public GameObject accessLevelUIText;

    private float jumpCooldown = 0.2f;
    private float jumpCooldownProgress = 0.0f;

    AudioSource tempMovementSoundPlayer;
    [SerializeField] AudioSource movementLoopSoundPlayer;
    [SerializeField] AudioResource takingOff;
    [SerializeField] AudioResource landing;

    bool onGround = true;

    float moveForceToAdd = 0;
    float upForceToAdd = 0;

    void Start()
	{
        tempMovementSoundPlayer = GetComponent<AudioSource>();

        deathUI.SetActive(false);
		//Get reference to the Animator component attached to this GameObject.
		anim = GetComponent<Animator> ();
		//Get and store a reference to the Rigidbody2D attached to this GameObject.
		rb = GetComponent<Rigidbody2D>();
        //Get and store a reference to the renderer attached to this GameObject.
        ren = GetComponent<SpriteRenderer> ();


        
    }

    private void Awake()
    {
        player = gameObject;
    }

    void Update()
	{
        
        //Don't allow control if the bird has died.
        if (isDead == false) 
		{
            if (jumpCooldownProgress > 0)
            {
                jumpCooldownProgress -= Time.deltaTime;
            }
            // get left/right movement input...
            float direction = Input.GetAxis("Horizontal");

            if (direction > 0)
            {
                rb.drag = 5;
                //rb.AddForce(new Vector2(moveForce, 0));
                moveForceToAdd += moveForce;
                ren.flipX = false;
            }
            else if ( direction <0)
            {
                rb.drag = 5;
                //rb.AddForce(new Vector2(-moveForce, 0));
                moveForceToAdd -= moveForce;
                ren.flipX = true;
            }
            else
            {
                rb.drag = 0;
            }


            //Look for input to trigger a "flap".
            if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown("space")) && jumpCooldownProgress <= 0)
            {
                jumpCooldownProgress = jumpCooldown;
                upForceToAdd += upForce;
                //rb.AddForce(new Vector2(0, upForce));
                rb.drag = 5;


            }

        }
	}

    private void FixedUpdate()
    {
        if (moveForceToAdd != 0)
        {
            rb.AddForce(new Vector2(moveForceToAdd*Time.fixedDeltaTime*250, 0));
            moveForceToAdd = 0;
        }
        if (upForceToAdd != 0) 
            {
                rb.AddForce(new Vector2(0, upForceToAdd * Time.fixedDeltaTime * 80));
                upForceToAdd = 0;
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

    public void updateAccessLevel(int newLevel)
    {
        accessLevel = newLevel;
        accessLevelUIText.GetComponent<TMPro.TextMeshProUGUI>().text = "Access Level: "+accessLevel.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6) 
        {
            print("Test1");
            anim.SetBool("onGround", true);
            onGround = true;
            tempMovementSoundPlayer.resource = landing;
            tempMovementSoundPlayer.Play();
            movementLoopSoundPlayer.Stop();
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6) 
        {
            print("Test2");
            anim.SetBool("onGround", false);
            onGround = false;
            tempMovementSoundPlayer.resource = takingOff;
            tempMovementSoundPlayer.Play();
            movementLoopSoundPlayer.PlayDelayed(1.464f);
        }
        onGround = false;
        
    }

    

    void ShowDeathUI()
    {
        deathUI.SetActive(true);
    }
}
