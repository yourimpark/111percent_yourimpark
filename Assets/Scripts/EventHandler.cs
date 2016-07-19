using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EventHandler : MonoBehaviour {
	public Text go;
	public GameObject prefab_money;

	void Awake()
	{
		LevelSetting ();
	}
	void Start () {
		StartCoroutine(StartGame());
		InvokeRepeating ("GenerateMoney",3f,3f);
		InvokeRepeating ("DestroyMoney", 5f, 3f);
	}
		
	private void LevelSetting ()
	{
		//Debug.Log ("---level settingcomplete--");
		GameObject other = GameObject.Find ("Other");
		GameObject ball = GameObject.Find ("Ball");

		other.GetComponent<AIBehavior> ().SetAISpeed(StageManager.static_ai_speed);
		other.GetComponent<AIBehavior> ().SetAIJump(StageManager.static_ai_jumpForce);
		ball.GetComponent<BallManager> ().SetBallSpeed (StageManager.static_ball_speed);
		other.GetComponent<BoxCollider2D> ().offset = new Vector2(
			other.GetComponent<BoxCollider2D> ().offset.x,StageManager.static_jumpTrigger);
	}

	IEnumerator StartGame()
	{
		yield return new WaitForSeconds (1f);
		go.gameObject.SetActive (false);
		GameObject.Find("Ball").SendMessage("BallStart");
	}
	private void GenerateMoney()
	{
		Instantiate(prefab_money, new Vector3(Random.Range(-1,-6), Random.Range(0,3),0), Quaternion.identity);
	}

	private void DestroyMoney()
	{
		GameObject[] moneyObj = GameObject.FindGameObjectsWithTag ("Money");
		foreach (GameObject money in moneyObj) 
		{
			GameObject.Destroy (money);
		}
	}
}
