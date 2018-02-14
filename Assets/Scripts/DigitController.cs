using UnityEngine;

public class DigitController : MonoBehaviour{

	public Sprite[] DigitSprites;
	public bool BigSprite;
	private int Value;
	private SpriteRenderer SR;

	void Awake(){
		SR = GetComponent<SpriteRenderer>();
		BigSprite = false;
		SetValue(0);
	}

	public void SetValue(int v){
		Value = v;
		ChangeSprite();
	}

	private void ChangeSprite(){
		SR.sprite = DigitSprites[Value + (BigSprite ? 10 : 0)];
	}
}
