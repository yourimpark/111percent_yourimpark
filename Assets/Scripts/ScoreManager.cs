using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	private int player_score	= 0;
	private int other_score		= 0;
	private int temp_money		= 0;

	public Text text_player_score;
	public Text text_other_score;
	public Text text_reward;
	public Text text_money;
	public GameObject result_board;

	//load Score
	void Awake()
	{
		temp_money		= PlayerPrefs.GetInt ("tmp_money");
		player_score	= PlayerPrefs.GetInt ("p_score");
		other_score 	= PlayerPrefs.GetInt ("o_score");
		ApplyUIScore ();
	}

	public void SetScore(bool win)
	{
		if (win == true) {
			player_score++;
			//temp_money += 1;
		} else { 
			other_score++;
		}
		if (!CheckFinalResult ()) 
		{
			StartCoroutine (SlowDown());
			ApplyUIScore ();
			SaveScore ();
		} 
		else 
		{
			//1 set end
			int addon =1;
			int reward = 0;
			result_board.SetActive(true);

			Text text_resultBoard = result_board.transform.GetChild(0).GetComponent<Text>();
			if (player_score > other_score) {
				text_resultBoard.text = "You Win!";
				addon = StageManager.static_winner_gold_multiplier;
			} else {
				text_resultBoard.text = "You Lose!";
			}

			reward = temp_money * addon;
			EarnMoney (reward);
			StartCoroutine (WaitUntilNewGame(reward));
			//Reset ();
		}

	}
	public void SetMoney(int amount)
	{
		temp_money += amount;
		text_money.text = temp_money.ToString ();
		PlayerPrefs.SetInt ("tmp_money", temp_money);
	}
	IEnumerator SlowDown()
	{
		Time.timeScale = 0.1F;
		yield return new WaitForSeconds(0.1f);
		Time.timeScale = 1.0F;
		Restart (SceneManager.GetActiveScene().buildIndex);
	}
	public void Restart(int num)
	{
		if (num == 1)
			Reset ();
		SceneManager.LoadScene(num);
	}
	private void ApplyUIScore()
	{
		text_money.text = temp_money.ToString ();
		text_player_score.text = player_score.ToString ();
		text_other_score.text = other_score.ToString ();
	}

	private void SaveScore()
	{
		PlayerPrefs.SetInt ("p_score", player_score);
		PlayerPrefs.SetInt ("o_score", other_score);
		PlayerPrefs.SetInt ("tmp_money", temp_money);
	}

	private void EarnMoney(int amount)
	{
		int current_money = PlayerPrefs.GetInt ("Money");
		PlayerPrefs.SetInt ("Money", current_money + amount);
	}

	private bool CheckFinalResult()
	{
		if (player_score >= 15 || other_score >= 15) 
		{
			return true;
		}
		return false;
	}
	private void Reset()
	{
		player_score 	= 0;
		other_score 	= 0;
		temp_money 		= 0;
		ApplyUIScore ();
		SaveScore ();
	}

	IEnumerator WaitUntilNewGame(int reward)
	{
		for (int i = 0; i <= reward; i++) {
			text_reward.text = i.ToString ();
			yield return new WaitForSeconds (0.01f);
		}
		yield return new WaitForSeconds (2.0f);
		result_board.SetActive(false);
		Restart (1);
	}

	void OnApplicationQuit()
	{
		Reset();
	}
}
