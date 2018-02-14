using UnityEngine;

public class CardController : MonoBehaviour{
	public Sprite[] FlippedSprites;
	public Sprite UnflippedSprite;
	public bool Selected;
	public GameObject Scoreboard;
	public GameObject Board;

	private SpriteRenderer SR;
	private int Value;
	private bool Flipped;

	void Awake(){
		Flipped = false;
		SR = GetComponent<SpriteRenderer>();
		SetValue(1);
		GetComponent<DetectTapped>().RegisterCallback(OnTap);
	}

	public void OnTap(){
		if(Selected){
			Flip();
			Scoreboard.SendMessage("MultiplyScore", Value);
		}
		else{
			Board.SendMessage("Select", gameObject);
		}
	}

	public void Flip(){
		Flipped = true;
		ChangeSprite();
	}

	public bool IsFlipped(){
		return Flipped;
	}

	public int GetValue(){
		return Value;
	}

	public void SetValue(int v){
		Value = v;
		ChangeSprite();
	}

	private void ChangeSprite(){
		SR.sprite = Flipped ? FlippedSprites[Value] : UnflippedSprite;
	}
}
