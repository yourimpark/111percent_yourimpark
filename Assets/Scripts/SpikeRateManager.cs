using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpikeRateManager : MonoBehaviour {
	public Text text_current_rate;
	public Text text_next_rate;
	public Text text_money_toSpend;
	public Text text_total_money;

	private int money;
	private float current_rate;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("spike_rate"))
			current_rate = PlayerPrefs.GetFloat ("spike_rate");
		else
			current_rate = 0.1f;
		money = PlayerPrefs.GetInt ("Money");
		ApplyUIText ();
	}

	private void SaveSpikeRate()
	{
		PlayerPrefs.SetFloat ("spike_rate", current_rate);
		PlayerPrefs.SetInt ("Money", money);
	}

	private void ApplyUIText ()
	{
		text_current_rate.text = (Mathf.FloorToInt(current_rate * 100)).ToString() + "%";
		text_next_rate.text = (Mathf.FloorToInt((current_rate+0.1f) * 100)).ToString() + "%";
		text_money_toSpend.text = Mathf.FloorToInt(current_rate * 1000).ToString();
		text_total_money.text = money.ToString ();
	}
	public void BuyItem()
	{
		float price = current_rate * 1000;

		if (money >= price) {
			if(current_rate <= 1f)
				current_rate += 0.1f;
			money -= Mathf.FloorToInt (price);
			ApplyUIText ();
			SaveSpikeRate ();
		}
	}
}
