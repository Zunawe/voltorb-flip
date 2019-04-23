using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutController : MonoBehaviour {
	public GameObject BoardGameObject;
	private BoardController Board;
	public GameObject ScoreboardGameObject;
	private ScoreboardController Scoreboard;

	private int Level = 1;
	private float ResetTimer = 0;
	private bool IsResetting = false;

	void Awake () {
		Board = BoardGameObject.GetComponent<BoardController>();
		Scoreboard = ScoreboardGameObject.GetComponent<ScoreboardController>();
		Board.GenerateBoard(1);
	}
	
	void Update () {
		if (Board.IsLost()) {
			GameState.GetGameState().SetLevel(1);
			Level = Math.Max(Level - 1, 1);
			Reset();
		} else if (Board.IsWon()) {
			Level = Math.Min(Level + 1, 8);
			Reset();
		}
	}

	public void Reset () {
		if (!IsResetting) {
			ResetTimer = 3.0f;
			IsResetting = true;
		} else if (ResetTimer > 0) {
			ResetTimer -= Time.deltaTime;
		} else {
			IsResetting = false;
			Board.GenerateBoard(Level);
		}
	}
}
