using UnityEngine;
 using System.Collections;
using System;
using System.IO;
 
 public class LoadGame : MonoBehaviour 
 {
 
	string  fileName;
 
	void Start() {
		fileName = "Level.txt";
	}
 
     public void OnMouseDown()
     {
		 string line = "";
		 string lvl = "";
		 if(File.Exists(fileName)){
				Debug.Log("Reading File");
				var sr = File.OpenText(fileName);
				line = sr.ReadLine();
				Debug.Log("First line is "+line);
				while(line != null){
					lvl = line; // prints each line of the file
					line = sr.ReadLine();
				}  
			} else {
				Debug.Log("DNE");
				return;
			}
		Debug.Log("Loading Level "+lvl);
         Application.LoadLevel("Level"+lvl);
     }
 }