using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour
{
	public Transform[] Waypoints;
	public float Speed;
	public int curWayPoint;
	public bool doPatrol = true;
	public Vector2 Target;
	public Vector2 MoveDirection;
	public Vector2 Velocity;
	public bool facingRight = false;

	void Create() {
		facingRight = true;
	}
	
	void Update()
	{
		if(curWayPoint < Waypoints.Length)
		{
			Target= Waypoints[curWayPoint].position;
			MoveDirection = Target - (Vector2)transform.position;
			Velocity = GetComponent<Rigidbody2D>().velocity;

			if(MoveDirection.magnitude < 1)
			{
				curWayPoint++;
				Vector3 theScale = transform.localScale;
				theScale.x *= -1;
				transform.localScale = theScale;
				facingRight = !facingRight;
			}
			else
			{
				Velocity = MoveDirection.normalized * Speed;
			}
		}
		else
		{
			if(doPatrol)
			{
				curWayPoint=0;
			}
			else
			{
				Velocity = Vector2.zero;
			}
		}

		GetComponent<Rigidbody2D>().velocity = Velocity;

	}


}