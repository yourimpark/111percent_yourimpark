using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour {
	public static int static_ai_speed				= 0;
	public static int static_ai_jumpForce			= 0;
	public static float static_ball_speed			= 0;
	public static float static_jumpTrigger			= 0;
	public static int static_winner_gold_multiplier	= 0;
	public Text text_money;
	private int money;

	void Awake()
	{
		if (PlayerPrefs.HasKey ("Money")) {
			money = PlayerPrefs.GetInt ("Money");
		}
		ApplyUIText ();
	}

	public void GotoScene(int num)
	{
		SceneManager.LoadScene (num);
	}

	public void EndGame()
	{
		Application.Quit();		
	}

	public void LevelSetting(int level)
	{
		switch (level) {
		case 1:
			SetLevelInfoData (12, 6, 15f, 0.2f, 2);
			break;
		case 2:
			SetLevelInfoData (14, 5, 16f, 0.18f, 5);
			break;
		case 3:
			SetLevelInfoData (16, 6, 17f, 0.2f, 10);
			break;
		case 4:
			SetLevelInfoData (19, 7, 18f, 0.14f, 100);
			break;
		default:
			break;
		}
	}
	public void AllReset()
	{
		PlayerPrefs.DeleteAll ();
		money = 0;
		ApplyUIText ();
	}
	public void MoneyCheat()
	{
		money += 100;
		PlayerPrefs.SetInt ("Money", money);
		ApplyUIText ();
	}
	private void SetLevelInfoData(int ai_speed,int jump,float ball_speed, float jump_trigger,int gold)
	{
		static_ai_speed = ai_speed;
		static_ai_jumpForce = jump;
		static_ball_speed = ball_speed;
		static_jumpTrigger = jump_trigger;
		static_winner_gold_multiplier = gold;
	}
	private void ApplyUIText()
	{
		text_money.text = money.ToString ();
	}

}
