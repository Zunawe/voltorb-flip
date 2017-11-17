using UnityEngine;

public class DigitController : MonoBehaviour{

	public Sprite[] DigitSprites;
	private int Value;
	private SpriteRenderer SR;

	void Awake(){
		SR = GetComponent<SpriteRenderer>();
		SetValue(0);
	}

	public void SetValue(int v){
		Value = v;
		ChangeSprite();
	}

	private void ChangeSprite(){
		SR.sprite = DigitSprites[Value];
	}
}
