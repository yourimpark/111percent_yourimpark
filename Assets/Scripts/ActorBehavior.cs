using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class ActorBehavior : MonoBehaviour {
	public Sprite sprite_jump;
	//
	private Sprite sprite_idle;
	private float jump_force = 15;
	private int moveForce = 12;
	private Rigidbody2D rb;

	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
		sprite_idle = GetComponent<SpriteRenderer> ().sprite;
	}
	void Update()
	{
		//horizontal move
		Vector2 move = new Vector2 (CrossPlatformInputManager.GetAxis ("Horizontal") * moveForce,rb.velocity.y);
		rb.velocity = move;

		//jump
		if(CrossPlatformInputManager.GetButtonDown("Jump")||Input.GetKeyDown(KeyCode.Space)) 
		{
			rb.AddForce (new Vector2(rb.velocity.normalized.x * jump_force, jump_force),ForceMode2D.Impulse);
			GetComponent<SpriteRenderer> ().sprite = sprite_jump;
		}

		if (CrossPlatformInputManager.GetButtonUp("Jump")||Input.GetKeyUp(KeyCode.Space)) 
		{
			GetComponent<SpriteRenderer> ().sprite = sprite_idle;
		}
	}

	//if user get money
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Money") {
			//Debug.Log ("+10");
			ScoreManager score_manager = GameObject.Find ("_Manager").GetComponent<ScoreManager> ();
			score_manager.SetMoney (3);
			Destroy (coll.gameObject);

		}
	}

}