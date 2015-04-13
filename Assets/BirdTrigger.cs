using UnityEngine;
using System.Collections;

public class BirdTrigger : MonoBehaviour {

	public GameObject enemy;
	public GameObject Player;
	public Vector2 spawnpoint = new Vector2(0,0);
	
	
	private float deathdelay = 3.0f;
	private float timewhen;
	private bool dietimer;
	private float timewhen2;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Something has entered this zone.");    
		
		GameObject enemyspawn = (GameObject)Instantiate(enemy, spawnpoint, transform.rotation);

		//Rigidbody2D cc = GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
		//cc.drag = 10000000; // Turn off the component

		
		
	}
	void OnTriggerStay2D(Collider2D other)
	{
		if (!dietimer) {
			dietimer = true;
			timewhen = Time.time + deathdelay;
		} else {
			
			if (Time.time >= timewhen) {
				Destroy (Player);
				Application.LoadLevel ("gameover");
				dietimer=false;
				
			}
		}
		
	}
	
}


