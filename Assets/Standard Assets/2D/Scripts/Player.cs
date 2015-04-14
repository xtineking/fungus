using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public string axisName = "Horizontal";
	public float speed = 1.0f;
	int _currentAnimationState = AntIdle;
	const int AntIdle = 1;
	const int AntWalk = 2;
	const int AntHit = 3;
	public GameObject shot;
	public Transform ShotSpawn;
	public float fireRate;
	
	private float nextFire;
	private Animator animator;

	// Use this for initialization
	protected void Start () {
		animator = this.GetComponent<Animator> ();

	}

	void Update(){
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate(shot, ShotSpawn.position, ShotSpawn.rotation);		

		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		//int horizontal = 0;
		//int vertical = 0;

		//horizontal = (int)Input.GetAxisRaw ("Horizontal");
		//vertical = (int)Input.GetAxisRaw ("Vertical");
		if (Input.GetAxis (axisName) < 0)
		{
			Vector3 newScale = transform.localScale;
			newScale.y = 1.0f;
			newScale.x = 1.0f;
			transform.localScale = newScale;
		} 
		else if (Input.GetAxis (axisName) > 0)
		{
			Vector3 newScale =transform.localScale;
			newScale.x = 1.0f;
			transform.localScale = newScale;        
		}
		
		transform.position += transform.right *Input.GetAxis(axisName)* speed * Time.deltaTime;
		changeState (AntWalk);
	}


	void changeState(int state){
		
		if (_currentAnimationState == state)
			return;
		
		switch (state) {
			
		case AntWalk:
			animator.SetInteger ("state", AntWalk);
			break;
			
		case AntHit:
			animator.SetInteger ("state", AntHit);
			break;
			
		
			
		}
		
		_currentAnimationState = state;
	}
}
