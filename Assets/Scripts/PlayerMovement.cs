using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public Camera camera;

	private const int UP = 0;
	private const int LEFT = 1;
	private const int DOWN = 2;
	private const int RIGHT = 3;
	private int orientation = UP;
	
	private bool jump;
	
    private Animator animator;            // Reference to the player's animator component.
    private Rigidbody2D rigidbody;
    public bool facingRight = false;  // For determining which way the player is currently facing.

	public Transform buttCollider;
	public Transform headCollider;

	public RayCheckLib rays;
	
	private float h = 0;
	
	public float maxSpeed = 3;
	public float speed = 4f;
	
	//private bool m_Jump;

	// Use this for initialization
    private void Awake()
    {
        // Setting up references.
        animator = gameObject.GetComponent<Animator>();
        rigidbody =  gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		camera.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
		 if(!jump)
			jump = Input.GetButtonDown("Jump");
		//animator.SetFloat("Speed", Mathf.Abs(h));
	}
	
	private void FixedUpdate()
    {
		// Read the inputs.
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		
			
		bool[] hits = rays.castRays(transform, facingRight);
		
		if(Mathf.Abs(rigidbody.velocity.x) < 0.01f && Mathf.Abs(rigidbody.velocity.y) < 0.01f) {
			if(orientation == UP && facingRight && v > 0) {
				if(hits[1] && hits[2] && hits[3]) {
					rays.rotateCounterClockwise(transform);
					orientation = LEFT;
				}
			}
			else if(orientation == UP && !facingRight && v > 0) {
				if(hits[1] && hits[2] && hits[3]) {
					rays.rotateClockwise(transform);
					orientation = RIGHT;
				}
			}
			else if(orientation == UP && facingRight && v < 0) {
				if(!hits[1] && !hits[2] && hits[3]) {
					transform.position = new Vector3(transform.position.x + .2f, transform.position.y - .2f, transform.position.z);
					rays.rotateClockwise(transform);
					orientation = RIGHT;
				}
			}
			else if(orientation == UP && !facingRight && v < 0) {
				if(!hits[1] && !hits[2] && hits[3]) {
					transform.position = new Vector3(transform.position.x - .2f, transform.position.y - .2f, transform.position.z);
					rays.rotateCounterClockwise(transform);
					orientation = LEFT;
				}
			}
			else if(orientation == DOWN && facingRight && v > 0) {
				if(!hits[1] && !hits[2] && hits[3]) {
					transform.position = new Vector3(transform.position.x - .2f, transform.position.y + .2f, transform.position.z);
					rays.rotateClockwise(transform);
					orientation = LEFT;
				}
			}
			else if(orientation == DOWN && !facingRight && v > 0) {
				if(!hits[1] && !hits[2] && hits[3]) {
					transform.position = new Vector3(transform.position.x + .2f, transform.position.y + .2f, transform.position.z);
					rays.rotateCounterClockwise(transform);
					orientation = RIGHT;
				}
			}
			else if(orientation == DOWN && facingRight && v < 0) {
				if(hits[1] && hits[2] && hits[3]) {
					rays.rotateCounterClockwise(transform);
					orientation = RIGHT;
				}
			}
			else if(orientation == DOWN && !facingRight && v < 0) {
				if(hits[1] && hits[2] && hits[3]) {
					rays.rotateClockwise(transform);
					orientation = LEFT;
				}
			}
			else if(orientation == LEFT && facingRight && h > 0) {
				if(!hits[1] && !hits[2] && hits[3]) {
					transform.position = new Vector3(transform.position.x + .2f, transform.position.y + .2f, transform.position.z);
					rays.rotateClockwise(transform);
					orientation = UP;
				}
			}
			else if(orientation == LEFT && !facingRight && h > 0) {
				if(!hits[1] && !hits[2] && hits[3]) {
					transform.position = new Vector3(transform.position.x + .2f, transform.position.y - .2f, transform.position.z);
					rays.rotateCounterClockwise(transform);
					orientation = DOWN;
				}
			}
			else if(orientation == LEFT && facingRight && h < 0) {
				if(hits[1] && hits[2] && hits[3]) {
					rays.rotateCounterClockwise(transform);
					orientation = DOWN;
				}
			}
			else if(orientation == LEFT && !facingRight && h < 0) {
				if(hits[1] && hits[2] && hits[3]) {
					rays.rotateClockwise(transform);
					orientation = UP;
				}
			}
			else if(orientation == RIGHT && facingRight && h > 0) {
				if(hits[1] && hits[2] && hits[3]) {
					rays.rotateCounterClockwise(transform);
					orientation = UP;
				}
			}
			else if(orientation == RIGHT && !facingRight && h > 0) {
				if(hits[1] && hits[2] && hits[3]) {
					rays.rotateClockwise(transform);
					orientation = DOWN;
				}
			}
			else if(orientation == RIGHT && facingRight && h < 0) {
				if(!hits[1] && !hits[2] && hits[3]) {
					transform.position = new Vector3(transform.position.x - .2f, transform.position.y - .2f, transform.position.z);
					rays.rotateClockwise(transform);
					orientation = DOWN;
				}
			}
			else if(orientation == RIGHT && !facingRight && h < 0) {
				if(!hits[1] && !hits[2] && hits[3]) {
					transform.position = new Vector3(transform.position.x - .2f, transform.position.y + .2f, transform.position.z);
					rays.rotateCounterClockwise(transform);
					orientation = UP;
				}
			}
			else{
				// Pass all parameters to the character control script.
				if((!hits[1] && !hits[2] && hits[3])) {
					float temp_speed = speed;
					speed = 0;
					move(orientation == UP || orientation == DOWN ? h : (-1)*v,  jump);
					speed = temp_speed;
				}
				else 
					move(orientation == UP || orientation == DOWN ? h : (-1)*v,  jump);
			}
		}
		else{
			// Pass all parameters to the character control script.
			if((!hits[1] && !hits[2] && hits[3])) {
				float temp_speed = speed;
				speed = 0;
				move(orientation == UP || orientation == DOWN ? h : (-1)*v,  jump);
				speed = temp_speed;
			}
			else 
				move(orientation == UP || orientation == DOWN ? h : (-1)*v,  jump);
		}
		
		switch(orientation) {
				case UP:
					rigidbody.AddForce(new Vector3(0,-100,0));
					break;
				case LEFT:
					rigidbody.AddForce(new Vector3(100,0,0));
					break;
				case RIGHT:
					rigidbody.AddForce(new Vector3(-100,0,0));
					break;
				case DOWN:
					rigidbody.AddForce(new Vector3(0,100,0));
					break;
				default:
					break;
		}
		
		jump = false;
		
		animator.SetFloat("Speed", Mathf.Abs(orientation == UP || orientation == DOWN ? h : (-1)*v));
	}
	
	private void move(float move, bool jump) {
		
		if ((orientation == UP || orientation == DOWN) && !jump) {
			// Move the character horizontally
			rigidbody.velocity = new Vector2 (move * speed, rigidbody.velocity.y);
		} else {
			// Move the character vertically
			rigidbody.velocity = new Vector2 (rigidbody.velocity.x, -move * speed);
		}
		
		if (move > 0 && !facingRight && orientation == UP && vert())
                {
                    // ... flip the player.
                    flip();
					//Debug.Log("FLIPA");
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && facingRight && orientation == UP  && vert())
                {
                    // ... flip the player.
                    flip();
					//Debug.Log("FLIPB");
                }
                else if (move > 0 && facingRight && orientation == LEFT  && !vert())
                {
                    // ... flip the player.
                    flip();
					//Debug.Log("FLIPC");
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && !facingRight && orientation == LEFT  && !vert())
                {
                    // ... flip the player.
                    flip();
					//Debug.Log("FLIPD");
                }
                else if (move > 0 && !facingRight && orientation == RIGHT && !vert())
                {
                    // ... flip the player.
                    flip();
					//Debug.Log("FLIPA");
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && facingRight && orientation == RIGHT  && !vert())
                {
                    // ... flip the player.
                    flip();
					//Debug.Log("FLIPB");
                }
                else if (move > 0 && facingRight && orientation == DOWN  && vert())
                {
                    // ... flip the player.
                    flip();
					//Debug.Log("FLIPC");
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && !facingRight && orientation == DOWN  && vert())
                {
                    // ... flip the player.
                    flip();
					//Debug.Log("FLIPD");
                }
				
		if (jump) {
			Debug.Log (rigidbody.velocity.x);
		}

		if(jump && Mathf.Abs(rigidbody.velocity.x) < 0.01f && Mathf.Abs(rigidbody.velocity.y) < 0.01f) {
			if(rays.transferLevels(transform, buttCollider, headCollider)) {
				if(orientation == UP) {
					orientation = DOWN;
				}
				else if(orientation == DOWN) {
					orientation = UP;
				}
				else if(orientation == LEFT) {
					orientation = RIGHT;
				}
				else {
					orientation = LEFT;
				}
			}
		}		
	}
	
	private void flip()
        {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
	
	private bool vert () {
		return orientation == UP || orientation == DOWN;
	}
}


