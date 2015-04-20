using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static bool playerSpotted = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate () {
		if(playerSpotted) {
			Invoke("killGame", 2);

			//SoundManager.instance.Stop(); //stops background music when game over
			if(Application.loadedLevelName == "GameOver"){
			/*	if(Input.anyKeyDown){
					Application.LoadLevel ("StartScene");
				} */


			}

		}
	}
	

	void killGame () {
		Application.LoadLevel("GameOver");
		//yield return new WaitForSeconds (2);
		//Application.LoadLevel ("StartScene");
		//gOver ();
		Invoke("reload", 2);
	}


	IEnumerator gOver () {
		Application.LoadLevel("GameOver");
		yield return new WaitForSeconds(2);
		Application.LoadLevel("StartScene");
		
	}

	void reload () {
		Application.LoadLevel ("StartScene"); //goes back to main menu after game over
	}
}
