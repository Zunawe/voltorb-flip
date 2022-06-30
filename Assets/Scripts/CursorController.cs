using UnityEngine;

public class CursorController : MonoBehaviour {
	public Sprite NormalSprite;
	public GameObject Board;

	private SpriteRenderer SR;
	private GameState State = GameState.GetGameState();

	void Awake () {
		GameState.GetGameState().OnChange("Select", HandleSelect);
		SR = GetComponent<SpriteRenderer>();
	}

	public void HandleSelect (GameState state) {
		GameObject card = Board.GetComponent<BoardController>().GetCardAt(state.GetSelectedRow(), state.GetSelectedColumn());
		gameObject.transform.position = card.transform.position + (new Vector3(0, 0, -1));
	}
}
