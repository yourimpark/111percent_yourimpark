using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BallManager : MonoBehaviour {

	private float max_ballSpeed = 15f;
	private bool resultCheck = false; 
	private Rigidbody2D rb;
	private float spike_rate = 0.1f;
	private int spike_force = 10;
	private bool is_spike = false;

	void Start()
	{
		rb = this.GetComponent<Rigidbody2D> ();
		rb.gravityScale = 0;

		//ignore ball collision above net (only human cannot passthrough)
		Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), GameObject.Find("AboveNet").GetComponent<Collider2D>());
		spike_rate = PlayerPrefs.GetFloat ("spike_rate");
		Debug.Log (spike_rate);
	}
	void Update()
	{
		if (!is_spike) {	
			//update ball speed threshold
			if (rb.velocity.x > max_ballSpeed || rb.velocity.y > max_ballSpeed) {
				rb.velocity = max_ballSpeed * (rb.velocity.normalized);
			}
		} 
	}

	private void BallStart()
	{
		rb.gravityScale = 2;
	}
		
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (is_spike)
			is_spike = false;
		
		if (coll.gameObject.tag == "Player") {
			ApplyResult (false);
		} else if (coll.gameObject.tag == "Other") {
			ApplyResult (true);
		} else if (coll.gameObject.tag == "Character") {
			//if player is jumping now
			if (coll.transform.position.y > -1)
				CheckSpike ();
		}
	}

	void CheckSpike()
	{
		//check spike rate and apply it
		if (Random.value <= spike_rate) {
			Debug.Log ("Spike");
			is_spike = true;
			Vector2 direction =  new Vector2 (4, -2) - (Vector2) this.transform.position;
			rb.AddForce(direction.normalized * spike_force, ForceMode2D.Impulse);
		}
	}
	void ApplyResult(bool win)
	{
		if (!resultCheck) {
			resultCheck = true;
			ScoreManager score_manager = GameObject.Find ("_Manager").GetComponent<ScoreManager> ();
			score_manager.SetScore (win);
		}
	}
	public void SetBallSpeed(float s)
	{
		max_ballSpeed = s;
	}
}
