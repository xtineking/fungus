using UnityEngine;
using System.Collections;

public class ArtificialGravity : MonoBehaviour {
	
	private Rigidbody2D rigidbody;

	// Use this for initialization
	void Start () {
		rigidbody = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody.AddForce(new Vector3(0,100,0));
	}
}
