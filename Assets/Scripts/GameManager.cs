using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	
	public float levelStartDelay = 2f;
	public float restartDelay = 2f;
	public static GameManager instance = null;
	public float timer;

	public PatrolSight spotted;

	private Text levelText;
	private Text timerText;
	private Text gameOverText;
	private GameObject gameOverImage;
	private GameObject levelImage;
	public int level = 1;
	private int queensKilled;
	private bool doingSetup;


	/*private static GameManager s_Instance = null;

	// Use this for initialization
	void Awake () {
		if (s_Instance == null)
		{
			s_Instance = this;
			DontDestroyOnLoad(gameObject);
			
			//Initialization code goes here[/INDENT]
		}
		else
		{
			Destroy(gameObject);
		}

		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		
		DontDestroyOnLoad (gameObject);

		InitGame ();
	}*/

	void Start () {
		InitGame ();
	}
	
	private void OnLevelWasLoaded (int index){

		Debug.Log ("LEVEL "+level+" WAS LOADED");
		Debug.Log ("Instance "+this);
		InitGame();
	}
	
	void InitGame(){
		//if(PlayerPrefs.GetInt ("Level") != 0)
			//level = PlayerPrefs.GetInt ("Level");
		doingSetup = true;
		levelImage = GameObject.Find ("LevelImage");
		levelText = GameObject.Find ("LevelText").GetComponent<Text> ();
		levelText.text = "Level " + level;
		levelImage.SetActive (true);
		Invoke ("HideLevelImage", levelStartDelay);

	}
	
	private void HideLevelImage(){
		levelImage.SetActive (false);
		doingSetup = false;

	}

	private void HideGameOverImageImage(){
		gameOverImage.SetActive (false);
		doingSetup = false;
		
	}


	
	public void GameOver(){
		queensKilled = level - 1;
		gameOverImage = GameObject.Find ("GameOverImage");
		gameOverText = GameObject.Find ("GameOverText").GetComponent<Text> ();
		gameOverText.text = "You killed " + queensKilled + " Queens in " + timer + " time. Try Again?";
		gameOverImage.SetActive (true);
		Invoke ("HideGameOverImage", restartDelay);
		//enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		timerText = GameObject.Find ("TimerText").GetComponent<Text> ();
		timerText.text = "Time: " + timer;
	}

	//public void CheckIfGameOver (){
		//for (int i = 0; i < patrols.size; i++) {
		//if (spotted) {
		//	GameOver ();
	//	}
		//}
	//}


	

	

}
