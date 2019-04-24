using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardController : MonoBehaviour {
	public GameObject NumberPrefab;

	private GameObject DisplayTotal;
	private GameObject DisplayCurrent;

	// Use this for initialization
	void Awake () {
		GameState.GetGameState().OnChange("CurrentScore", UpdateCurrentScore);
		GameState.GetGameState().OnChange("TotalScore", UpdateTotalScore);

		DisplayTotal = (GameObject)Instantiate(NumberPrefab, transform.position, Quaternion.identity, transform);
		DisplayTotal.GetComponent<NumberController>().SetNumDigits(5);
		DisplayTotal.GetComponent<NumberController>().SetValue(0);
		DisplayTotal.GetComponent<NumberController>().UseBigSprites(true);
		DisplayTotal.transform.parent = gameObject.transform;
		DisplayTotal.transform.localPosition = new Vector3(5.78125f, -0.28125f, -1);

		DisplayCurrent = (GameObject)Instantiate(NumberPrefab, transform.position, Quaternion.identity, transform);
		DisplayCurrent.GetComponent<NumberController>().SetNumDigits(5);
		DisplayCurrent.GetComponent<NumberController>().SetValue(0);
		DisplayCurrent.GetComponent<NumberController>().UseBigSprites(true);
		DisplayCurrent.transform.parent = gameObject.transform;
		DisplayCurrent.transform.localPosition = new Vector3(5.78125f, -1.53125f, -1);
	}

	private void UpdateCurrentScore (GameState state) {
		DisplayCurrent.GetComponent<NumberController>().SetValue(state.GetCurrentScore());
	}

	private void UpdateTotalScore (GameState state) {
		DisplayTotal.GetComponent<NumberController>().SetValue(state.GetTotalScore());
	}
}
