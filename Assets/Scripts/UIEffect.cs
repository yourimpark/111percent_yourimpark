using UnityEngine;
using System.Collections;

public class UIEffect : MonoBehaviour {

	public enum EffectOption
	{
		SIZE,
		POS
	}

	public EffectOption effectToUse = EffectOption.SIZE;
	private bool toggle_size 		= false;
	private bool toggle_pos 		= false;
	private float current_pos;
	void Start()
	{
		current_pos = this.GetComponent<RectTransform> ().localPosition.y;
		toggle_size = (effectToUse == EffectOption.SIZE);
		toggle_pos = (effectToUse == EffectOption.POS);
	}

	// Update is called once per frame
	void Update () 
	{
		if(toggle_size)
			SizeEffect (0.3f, 90, 110);
		if (toggle_pos) {
			//float current_pos = this.GetComponent<RectTransform> ().localPosition.y;
			PosEffect (1.0f,current_pos-5,current_pos+5);
		}
			
	}

	void SizeEffect(float t, int min_size, int max_size)
	{
		this.GetComponent<RectTransform> ().sizeDelta = Vector2.Lerp(new Vector2(min_size,min_size),
			new Vector2(max_size,max_size),
			Mathf.PingPong(Time.time,t));
	}
	void PosEffect(float t, float min_pos, float max_pos)
	{
		Vector2 pos = this.GetComponent<RectTransform> ().localPosition;
		this.GetComponent<RectTransform> ().localPosition = Vector2.Lerp(new Vector2(pos.x,min_pos),
			new Vector2(pos.x,max_pos),
			Mathf.PingPong(Time.time,t));
	}
}
