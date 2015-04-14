using System;
using UnityEngine;

    public class PlatformerCharacter2D : MonoBehaviour
    {
		
		public RayCheckLib lib;
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        public bool m_FacingRight = true;  // For determining which way the player is currently facing.
		private const int UPRIGHT = 0;
		private const int LEFTOUT = 1;
		private const int UPSIDEDOWN = 2;
		private const int RIGHTOUT = 3;
		private int m_Orientation= UPRIGHT; // True being horizontal
		private bool m_Jump;

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }

		private void Update(){
			if (!m_Jump)
			{
				// Read the jump input in Update so button presses aren't missed.
				m_Jump = Input.GetButtonDown("Jump");
			}
			Vector3 fwd = transform.TransformDirection(Vector3.forward);
			if (Physics.Raycast(transform.position, fwd, 10))
				Debug.Log("HIT WALL");
			//lib.castRays(transform, m_FacingRight, m_Orientation);
		}


        private void FixedUpdate()
        {
			// Read the inputs.
			bool crouch = Input.GetKey(KeyCode.LeftControl);
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");
			
			bool[] hits = lib.castRays(transform, m_FacingRight, m_Orientation);
			
			if(m_Orientation == UPRIGHT && m_FacingRight && v > 0) {
				if(hits[1] && hits[2] && hits[3]) {
					lib.rotateCounterClockwise(transform);
					m_Orientation = LEFTOUT;
					Physics.gravity = new Vector3(1,0,0);
				}
			}
			else if(m_Orientation == UPRIGHT && !m_FacingRight && v > 0) {
				if(hits[1] && hits[2] && hits[3]) {
					lib.rotateClockwise(transform);
					m_Orientation = RIGHTOUT;
				}
			}
			else if(m_Orientation == UPRIGHT && m_FacingRight && v < 0) {
				if(!hits[1] && !hits[2] && hits[3]) {
					lib.rotateClockwise(transform);
					m_Orientation = RIGHTOUT;
				}
			}
			else if(m_Orientation == UPRIGHT && !m_FacingRight && v < 0) {
				if(!hits[1] && !hits[2] && hits[3]) {
					lib.rotateCounterClockwise(transform);
					m_Orientation = LEFTOUT;
				}
			}
			else if(m_Orientation == UPSIDEDOWN && m_FacingRight && v > 0) {
				if(!hits[1] && !hits[2] && hits[3]) {
					lib.rotateClockwise(transform);
					m_Orientation = LEFTOUT;
				}
			}
			else if(m_Orientation == UPSIDEDOWN && !m_FacingRight && v > 0) {
				if(!hits[1] && !hits[2] && hits[3]) {
					lib.rotateCounterClockwise(transform);
					m_Orientation = RIGHTOUT;
				}
			}
			else if(m_Orientation == UPSIDEDOWN && m_FacingRight && v < 0) {
				if(hits[1] && hits[2] && hits[3]) {
					lib.rotateCounterClockwise(transform);
					m_Orientation = RIGHTOUT;
				}
			}
			else if(m_Orientation == UPSIDEDOWN && !m_FacingRight && v < 0) {
				if(hits[1] && hits[2] && hits[3]) {
					lib.rotateClockwise(transform);
					m_Orientation = LEFTOUT;
				}
			}
			else if(m_Orientation == LEFTOUT && m_FacingRight && h > 0) {
				if(!hits[1] && !hits[2] && hits[3]) {
					lib.rotateClockwise(transform);
					m_Orientation = UPRIGHT;
				}
			}
			else if(m_Orientation == LEFTOUT && !m_FacingRight && h > 0) {
				if(!hits[1] && !hits[2] && hits[3]) {
					lib.rotateCounterClockwise(transform);
					m_Orientation = UPSIDEDOWN;
				}
			}
			else if(m_Orientation == LEFTOUT && m_FacingRight && h < 0) {
				if(hits[1] && hits[2] && hits[3]) {
					lib.rotateCounterClockwise(transform);
					m_Orientation = UPSIDEDOWN;
				}
			}
			else if(m_Orientation == LEFTOUT && !m_FacingRight && h < 0) {
				if(hits[1] && hits[2] && hits[3]) {
					lib.rotateClockwise(transform);
					m_Orientation = UPRIGHT;
				}
			}
			else if(m_Orientation == RIGHTOUT && m_FacingRight && h > 0) {
				if(hits[1] && hits[2] && hits[3]) {
					lib.rotateCounterClockwise(transform);
					m_Orientation = UPRIGHT;
				}
			}
			else if(m_Orientation == RIGHTOUT && !m_FacingRight && h > 0) {
				if(hits[1] && hits[2] && hits[3]) {
					lib.rotateClockwise(transform);
					m_Orientation = UPSIDEDOWN;
				}
			}
			else if(m_Orientation == RIGHTOUT && m_FacingRight && h < 0) {
				if(!hits[1] && !hits[2] && hits[3]) {
					lib.rotateClockwise(transform);
					m_Orientation = UPSIDEDOWN;
				}
			}
			else if(m_Orientation == RIGHTOUT && !m_FacingRight && h < 0) {
				if(!hits[1] && !hits[2] && hits[3]) {
					lib.rotateCounterClockwise(transform);
					m_Orientation = UPRIGHT;
				}
			}
			else{
				// Pass all parameters to the character control script.
				Move(m_Orientation == UPRIGHT || m_Orientation == UPSIDEDOWN ? h : (-1)*v, crouch, m_Jump);
			}
			
			//if(m_FacingRight && )
			
			switch(m_Orientation) {
				case UPRIGHT:
					m_Rigidbody2D.AddForce(new Vector3(0,-100,0));
					break;
				case LEFTOUT:
					m_Rigidbody2D.AddForce(new Vector3(100,0,0));
					break;
				case RIGHTOUT:
					m_Rigidbody2D.AddForce(new Vector3(-100,0,0));
					break;
				case UPSIDEDOWN:
					m_Rigidbody2D.AddForce(new Vector3(0,100,0));
					break;
				default:
					break;
			}
			
			m_Jump = false;

            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }
            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }


        public void Move(float move, bool crouch, bool jump)
        {
            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move*m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

				if (m_Orientation == UPRIGHT || m_Orientation == UPSIDEDOWN) {
					// Move the character horizontally
					m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);
					//Vector3 velocity = new Vector3(move, 1,1);
					//m_Rigidbody2D.MovePosition((transform.position + velocity) * Time.fixedDeltaTime);
				} else {
					// Move the character vertically
					m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, -move*m_MaxSpeed);
					//Vector3 velocity = new Vector3(1, move,1);
					//m_Rigidbody2D.MovePosition((transform.position + velocity) * Time.fixedDeltaTime);
				}
                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight && m_Orientation == UPRIGHT && vert())
                {
                    // ... flip the player.
                    Flip();
					//Debug.Log("FLIPA");
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight && m_Orientation == UPRIGHT  && vert())
                {
                    // ... flip the player.
                    Flip();
					//Debug.Log("FLIPB");
                }
                else if (move > 0 && m_FacingRight && m_Orientation == LEFTOUT  && !vert())
                {
                    // ... flip the player.
                    Flip();
					//Debug.Log("FLIPC");
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && !m_FacingRight && m_Orientation == LEFTOUT  && !vert())
                {
                    // ... flip the player.
                    Flip();
					//Debug.Log("FLIPD");
                }
                else if (move > 0 && !m_FacingRight && m_Orientation == RIGHTOUT && !vert())
                {
                    // ... flip the player.
                    Flip();
					//Debug.Log("FLIPA");
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight && m_Orientation == RIGHTOUT  && !vert())
                {
                    // ... flip the player.
                    Flip();
					//Debug.Log("FLIPB");
                }
                else if (move > 0 && m_FacingRight && m_Orientation == UPSIDEDOWN  && vert())
                {
                    // ... flip the player.
                    Flip();
					//Debug.Log("FLIPC");
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && !m_FacingRight && m_Orientation == UPSIDEDOWN  && vert())
                {
                    // ... flip the player.
                    Flip();
					//Debug.Log("FLIPD");
                }
            }
            // If the player should jump...
            /*if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }*/
			if(jump) {
				if(lib.transferLevels(transform)) {
					if(m_Orientation == UPRIGHT) {
						m_Orientation = UPSIDEDOWN;
					}
					else if(m_Orientation == UPSIDEDOWN) {
						m_Orientation = UPRIGHT;
					}
					else if(m_Orientation == LEFTOUT) {
						m_Orientation = RIGHTOUT;
					}
					else {
						m_Orientation = LEFTOUT;
					}
				}
			}
        }
		
		private bool vert () {
			return m_Orientation == UPRIGHT || m_Orientation == UPSIDEDOWN;
		}

        private void Flip()
        {
            // Switch the way the player is labelled as facing.
			//Debug.Log("FLIPPING");
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
		
		/*
		private void HorizFlip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.y *= -1;
            transform.localScale = theScale;
        }*/
    }