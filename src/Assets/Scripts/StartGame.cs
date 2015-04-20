using UnityEngine;
 using System.Collections;
using System;
using System.IO;
 

 
 public class StartGame : MonoBehaviour 
 {
 
	string  fileName;
 
	void Start() {
		fileName = "Level.txt";
	}
 
     public void OnMouseDown()
     {
        var sr = File.CreateText(fileName);
        sr.WriteLine ("1");
        sr.Close();
         Application.LoadLevel("Level1");
     }
 }