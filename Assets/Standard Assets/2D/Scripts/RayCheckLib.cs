using System;
using UnityEngine;

public class RayCheckLib : MonoBehaviour {
	
	Vector3 rayHorizontal;
	Vector3 rayDiagonal;
	Vector3 rayVertical;
	
	RaycastHit2D hitHorizontal;
	RaycastHit2D hitDiagonal;
	RaycastHit2D hitVertical;
	
	public bool[] castRays(Transform trans, bool facingRight, int orientation) {
		
		//trans.Rotate(Vector3.up);
		
		/*trans.localEulerAngles = new Vector3(
			trans.localEulerAngles.x,
			trans.localEulerAngles.y,
			90f
		);*/
		Vector3 pos = trans.position;
		
		//this array is 1-indexed because se said
		bool[] hits = new bool[4];
		//endSight = new Vector3((startSight.position.x+(3*turn)), (startSight.position.y), startSight.position.z);

		//Vector3 rayHorizontal = new Vector3(2,0,0);
		//Vector3 rayDiagonal = new Vector3(2,-2,0);
		//Vector3 rayVertical = new Vector3(0,-2,0);		
		if(facingRight) {
			rayHorizontal = trans.right;
		}
		else {
			rayHorizontal = -trans.right;
		}
		if(facingRight) {
			rayDiagonal = trans.right-trans.up;
		}
		else {
			rayDiagonal = -trans.right-trans.up;
		}
		rayVertical = -trans.up;
		
		hitHorizontal = Physics2D.Raycast (pos, rayHorizontal, 2f, 1 << LayerMask.NameToLayer("HillLayer"));
		hitDiagonal = Physics2D.Raycast (pos, rayDiagonal, 2f, 1 << LayerMask.NameToLayer("HillLayer"));
		hitVertical = Physics2D.Raycast (pos, rayVertical, 2f, 1 << LayerMask.NameToLayer("HillLayer"));
		
		//Debug.DrawRay(pos, rayHorizontal, Color.red); 
		//Debug.DrawRay(pos, rayDiagonal, Color.red); 
		//Debug.DrawRay(pos, rayVertical, Color.red); 
		
		if(hitHorizontal.collider != null) {
			Debug.DrawLine(pos, hitHorizontal.point, Color.red); 
			hits[1] = true;
		}
		if(hitDiagonal.collider != null) {
			Debug.DrawLine(pos, hitDiagonal.point, Color.red); 
			hits[2] = true;
		}
		if(hitVertical.collider != null) {
			Debug.DrawLine(pos, hitVertical.point, Color.red); 
			hits[3] = true;
		}
		
		return hits;
	}
	
	public bool transferLevels(Transform trans) {
		Vector3 rayV = trans.up;
		RaycastHit2D hitV = Physics2D.Raycast (trans.position, rayV, 4f, 1 << LayerMask.NameToLayer("HillLayer"));
		if(hitV.collider != null) {
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			trans.localEulerAngles = new Vector3(
				trans.localEulerAngles.x,
				trans.localEulerAngles.y,
				trans.localEulerAngles.z+180f
			);
			return true;
		}
		return false;
	}
	
	public void rotateCounterClockwise(Transform trans) {
		trans.localEulerAngles = new Vector3(
			trans.localEulerAngles.x,
			trans.localEulerAngles.y,
			trans.localEulerAngles.z+90f
		);
	}
	
	public void rotateClockwise(Transform trans) {
		trans.localEulerAngles = new Vector3(
			trans.localEulerAngles.x,
			trans.localEulerAngles.y,
			trans.localEulerAngles.z-90f
		);
	}
}