using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static bool playerSpotted = false;

	// Use this for initialization
	void Start () {
		playerSpotted =  false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate () {
		if(playerSpotted) {
			Invoke("killGame", 2);
		}
	}
	
	void killGame () {
		Application.LoadLevel("GameOver");
	}
}