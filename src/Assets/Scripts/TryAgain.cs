using UnityEngine;
using System.Collections;

public class TryAgain : MonoBehaviour {

	public void mulligan (int level) {
		Application.LoadLevel("Level"+level);
	}
	
}
