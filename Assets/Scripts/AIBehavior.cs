using UnityEngine;
using System.Collections;

public class AIBehavior : MonoBehaviour {
	public GameObject target;

	private int jump_force 	= 7;
	private int moveForce 	= 12;

	private Vector2 startPos;
	private Vector2 endPos;

	private bool move_flag = false;
	private Rigidbody2D rb;

	void Start () 
	{
		rb = this.GetComponent<Rigidbody2D> ();
	}

	void Update () 
	{

		if (target.transform.position.x > -1) 
		{
			move_flag = true;
		} else 
		{
			move_flag = false;
		}
	}

	void FixedUpdate () 
	{
		startPos = this.transform.position;
		endPos = target.transform.position;
		AIMove (startPos,endPos);
	}

	private void AIMove(Vector2 startPos, Vector2 endPos)
	{
		if (move_flag == true) 
		{
			this.transform.position = new Vector2(Mathf.Lerp(startPos.x, endPos.x, moveForce * Time.fixedDeltaTime),startPos.y);

			if (Mathf.Abs(startPos.x - endPos.x) >= 0.5f) 
			{
				move_flag = false;
			}
		}
	}
	public void SetAISpeed(int s)
	{
		moveForce = s;
	}
	public void SetAIJump(int j)
	{
		jump_force = j;
	}

	//when ball comes into enemy's field, then jump
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Ball"&& this.transform.position.y<1f)
			rb.AddForce (new Vector2(rb.velocity.normalized.x * jump_force, jump_force),ForceMode2D.Impulse);
	}
}
