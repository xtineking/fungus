using UnityEngine;
 using System.Collections;
 
 public class QuitGame : MonoBehaviour 
 {
 
     public void OnMouseDown ()
     {
		 Debug.Log("Quit game.");
         Application.Quit();
     }
 }