using UnityEngine;
using System.Collections;

public class PatrolSight : MonoBehaviour {

	public Transform startSight;
	public Vector3 endSight;
	public Patroller patroller;
	public int foundCount = 0;
	public bool spotted = false;
	//public Transform guard;
	//public int guardSightAmount = 10;
	
	// Update is called once per frame
	void Update () {
		search();
	}
	
	void search() {	
		int turn = 0;
		if(patroller.facingRight) {
			turn = 1;
		}
		else turn = -1;
		endSight = new Vector3((startSight.position.x+(1*turn)), (startSight.position.y), startSight.position.z);
		Debug.DrawLine(startSight.position, endSight, Color.green); 
		spotted = Physics2D.Linecast (startSight.position, endSight, 1 << LayerMask.NameToLayer("PlayerCharacterLayer"));
		respond();
	}
	
	void respond() {
		if(spotted) {
			Debug.Log("FOUND THE PLAYER");
			GetComponent<AudioSource>().Play();
			GameManager.playerSpotted = true;
		}
	}
}
