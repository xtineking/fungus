using UnityEngine;
using System.Collections;

public class Level2Collider : MonoBehaviour {
 public GameObject collisionObject;
	
	void FixedUpdate() {
 
	if(collisionObject.transform.position.x > transform.position.x && collisionObject.transform.position.y < transform.position.y) {
		
		 Debug.Log("INFECTED");
         Invoke("NextLevel", 2);	
     }
 }
 
	void NextLevel() {
		Application.LoadLevel("YouWin");
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
