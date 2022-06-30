using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutController : MonoBehaviour {
  public GameObject BoardGameObject;
  private BoardController Board;
  public GameObject ScoreboardGameObject;
  private ScoreboardController Scoreboard;

  void Awake () {
    GameState.GetGameState().OnChange("CurrentScore", HandleScoreUpdate);
    Board = BoardGameObject.GetComponent<BoardController>();
    Scoreboard = ScoreboardGameObject.GetComponent<ScoreboardController>();
    GameState.GetGameState().SetCurrentScore(1);
    Board.GenerateBoard(1);
  }

  public void HandleScoreUpdate (GameState state) {
    int currentScore = state.GetCurrentScore();
    int level = state.GetLevel();
    if (currentScore == 0) {
      state.SetLevel(Math.Max(level - 1, 1));
      state.Refresh();
      Board.GenerateBoard(Math.Max(level - 1, 1));
    } else if (currentScore == Board.GetWinningScore()) {
      state.AddTotalScore(currentScore);
      state.SetLevel(Math.Min(level + 1, 8));
      state.Refresh();
      Board.GenerateBoard(Math.Min(level + 1, 8));
    }
  }
}
