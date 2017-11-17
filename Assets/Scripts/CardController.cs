using UnityEngine;

public class CardController : MonoBehaviour{

	public Sprite[] FlippedSprites;
	public Sprite UnflippedSprite;
	private SpriteRenderer SR;
	private int Value;
	private bool Flipped;

	void Awake(){
		Flipped = false;
		SR = GetComponent<SpriteRenderer>();
		SetValue(1);
		GetComponent<DetectTapped>().RegisterCallback(Flip);
	}

	public void Flip(){
		Flipped = true;
		ChangeSprite();
	}

	public void SetValue(int v){
		Value = v;
		ChangeSprite();
	}

	private void ChangeSprite(){
		SR.sprite = Flipped ? FlippedSprites[Value] : UnflippedSprite;
	}
}
