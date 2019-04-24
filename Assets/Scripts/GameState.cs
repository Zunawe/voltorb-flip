using System;
using System.Collections.Generic;
using UnityEngine;

public enum LevelState {
    Playing,
    Won,
    Lost
}

public class GameState {
    private static GameState Instance = null;

    private int TotalScore;
    private int CurrentScore;
    private int Level;
    private LevelState State;
    private bool[,] FlippedCards = new bool[5, 5];
    private int[,] CardValues = new int[5, 5];
    private int[] Selected = new int[2];
    private bool Interactable;

    private Dictionary<string, List<Action<GameState>>> Subscriptions;

    private GameState () {
        this.Subscriptions = new Dictionary<string, List<Action<GameState>>>();
    }

    public static GameState GetGameState() {
        if (Instance == null) {
            Instance = new GameState();
        }
        return Instance;
    }

    public void Refresh () {
        SetCurrentScore(1);
        for (int i = 0; i < 5; ++i) {
            for (int j = 0; j < 5; ++j) {
                FlippedCards[i, j] = false;
                CardValues[i, j] = 1;
            }
        }
    }

    public int GetTotalScore () {
        return this.TotalScore;
    }

    public void SetTotalScore (int value) {
        this.TotalScore = value;

        foreach (Action<GameState> cb in Subscriptions["TotalScore"]) {
            cb(this);
        }
    }

    public void AddTotalScore (int value) {
        this.SetTotalScore(this.TotalScore + value);
    }

    public int GetCurrentScore () {
        return this.CurrentScore;
    }

    public void SetCurrentScore (int value) {
        this.CurrentScore = value;

        foreach (Action<GameState> cb in Subscriptions["CurrentScore"]) {
            cb(this);
        }
    }

    public void MultiplyCurrentScore (int value) {
        this.SetCurrentScore(this.CurrentScore * value);
    }

    public int GetLevel () {
        return this.Level;
    }

    public void SetLevel (int value) {
        this.Level = value;
    }

    public LevelState GetLevelState () {
        return this.State;
    }

    public void SetLevelState (LevelState value) {
        this.State = value;
    }

    public bool IsFlipped (int i, int j) {
        return FlippedCards[i, j];
    }

    public void SetFlipped (int i, int j, bool value) {
        FlippedCards[i, j] = value;
    }

    public bool IsSelected (int i, int j) {
        return i == Selected[0] && j == Selected[1];
    }

    public int GetSelectedRow () {
        return Selected[0];
    }

    public int GetSelectedColumn () {
        return Selected[1];
    }

    public void Select (int i, int j) {
        Selected[0] = i;
        Selected[1] = j;

        foreach (Action<GameState> cb in Subscriptions["Select"]) {
            cb(this);
        }
    }

    public int GetCardValue (int i, int j) {
        return this.CardValues[i, j];
    }

    public void SetCardValue (int i, int j, int value) {
        this.CardValues[i, j] = value;
    }

    public void TapCard (int i, int j) {
        if (IsSelected(i, j) && !IsFlipped(i, j)) {
            MultiplyCurrentScore(GetCardValue(i, j));
            SetFlipped(i, j, true);
        }
        Select(i, j);
    }

    public void OnChange (string name, Action<GameState> cb) {
        if (!this.Subscriptions.ContainsKey(name)) {
            this.Subscriptions.Add(name, new List<Action<GameState>>());
        }
        this.Subscriptions[name].Add(cb);
    }
}
