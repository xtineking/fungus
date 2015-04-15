using UnityEngine;
using System.Collections;

public class Queen : MonoBehaviour {

	public GameManager game;

	public float nextLevelDelay = 1f;

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("LEVEL "+game.level+" COMPLETE");
		Invoke ("NextLevel", nextLevelDelay);
	}

	private void NextLevel(){
		game.level++;
		//PlayerPrefs.SetInt ("Level", game.level);
		Application.LoadLevel ("Level" + game.level);
		//level++;
	}
}
