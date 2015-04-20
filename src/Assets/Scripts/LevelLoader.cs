using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class LevelLoader : MonoBehaviour {

	private string fileName;
	private string lvl;
	
	// Use this for initialization
	void Start () {
		fileName = "Level.txt";
		string line = "";
		lvl = "";
		 if(File.Exists(fileName)){
				Debug.Log("Reading File");
				var sr = File.OpenText(fileName);
				line = sr.ReadLine();
				Debug.Log("First line is "+line);
				while(line != null){
					lvl = line; // prints each line of the file
					line = sr.ReadLine();
				}  
				sr.Close();
			} else {
				Debug.Log("DNE");
				return;
			}
			
			int level = Int32.Parse(lvl);
			level ++;
		Debug.Log("Loading Level "+lvl);
			lvl = level+"";
			Rewrite(lvl);
         Invoke("NextLevel", 4);
	}
	
	void NextLevel() {
		Application.LoadLevel("Level"+lvl);
	}
	
	void Rewrite(string lvl) {
		var sr = File.CreateText(fileName);
		sr.WriteLine(lvl);
		sr.Close();
		}
	
	// Update is called once per frame
	void Update () {
	
	}
}
