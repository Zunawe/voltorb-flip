using UnityEngine;

public class CardController : MonoBehaviour {
	public Sprite[] FlippedSprites;
	public Sprite UnflippedSprite;
	public GameObject BlinkEffectPrefab;
	public GameObject ExplosionEffectPrefab;

	private SpriteRenderer SR;
	private Animator animator;
	private int Value;
	private int Row;
	private int Column;
	private bool Flipped;
	private GameState State = GameState.GetGameState();

	void Awake () {
		Flipped = false;
		animator = GetComponent<Animator>();
		SetValue(1);
		GetComponent<DetectTapped>().RegisterCallback(OnTap);
	}

	public void OnTap () {
		if (State.IsInteractable()) {
			State.TapCard(Row, Column);
			if (State.IsFlipped(Row, Column) && !Flipped) {
				Flip();
			}
		}
	}

	public void Flip () {
		Flipped = true;
		animator.SetBool("Flipped", true);
		if (Value == 0) {
			State.SetInteractable(false);
		}
	}

	void FlipAnimationEnd () { 
		if (Value == 0) {
			Instantiate(ExplosionEffectPrefab, transform.position + (new Vector3(-0.5f, 0.5f, -2)), Quaternion.identity, transform);
		} else {
			Instantiate(BlinkEffectPrefab, transform.position + (new Vector3(-0.5f, 0.5f, -2)), Quaternion.identity, transform);
		}
	}

	public bool IsFlipped () {
		return Flipped;
	}

	public int GetValue () {
		return Value;
	}

	public void SetValue (int v) {
		Value = v;
		animator.SetInteger("Value", v);
	}

	public int GetPosition () {
		return Value;
	}

	public void SetPosition (int r, int c) {
		Row = r;
		Column = c;
	}
}
