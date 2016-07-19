using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public GameObject player; 
	public int speed = 10;
	public void Move(int direction)
	{
		player.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (direction * speed* Time.deltaTime,0));
	}
}
