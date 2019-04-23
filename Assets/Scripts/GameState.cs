using System;
using System.Collections.Generic;
using UnityEngine;

public class GameState {
    private static GameState Instance = null;

    private int TotalScore;
    private int CurrentScore;
    private int Level;
    private bool[,] FlippedCards = new bool[5, 5];
    private int[,] CardValues = new int[5, 5];
    private bool Interactable;

    private List<Action<int>> Subscriptions;

    private GameState () {
        CurrentScore = 1;
        Subscriptions = new List<Action<int>>();
    }

    public static GameState GetGameState() {
        if (Instance == null) {
            Instance = new GameState();
        }
        return Instance;
    }

    public int GetTotalScore () {
        return this.TotalScore;
    }

    public void SetTotalScore (int value) {
        this.TotalScore = value;
    }

    public void AddTotalScore (int value) {
        this.TotalScore += value;
    }

    public int GetCurrentScore () {
        return this.CurrentScore;
    }

    public void SetCurrentScore (int value) {
        this.CurrentScore = value;
        foreach (Action<int> cb in Subscriptions) {
            cb(value);
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

    public int GetCardValue (int i, int j) {
        return this.CardValues[i, j];
    }

    public void SetCardValue (int i, int j, int value) {
        this.CardValues[i, j] = value;
    }

    public void SubscribeToCurrentScore (Action<int> cb) {
        this.Subscriptions.Add(cb);
    }
}
