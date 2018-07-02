using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour{
	public GameObject NumberPrefab;
	public GameObject CardPrefab;
	public GameObject CursorPrefab;

	public GameObject Scoreboard;

	private List<int[]>[] Layouts;

	private GameObject[,] Cards;
	private GameObject SelectedCard;
	private GameObject Cursor;

	private bool InProgress = false;
	private bool MemoOpen = false;
	private int Level;
	private int Score;
	private int WinningScore;
	private int[] Multipliers;

	private int[] SumInRow;
	private int[] BombsInRow;
	private int[] SumInColumn;
	private int[] BombsInColumn;

	public void GenerateBoard(int level){
		ClearChildren();
		
		Cursor = (GameObject)Instantiate(CursorPrefab, new Vector3(-5, -5, -1), Quaternion.identity, transform);

		Level = level;
		Score = 1;
		WinningScore = 1;

		GenerateLayouts();

		Cards = new GameObject[5, 5];
		int randomIndex = (int)Random.Range(0, Layouts[Level - 1].Count);
		Multipliers = (int[])Layouts[Level - 1][randomIndex].Clone();
		SumInRow = new int[5];
		BombsInRow = new int[5];
		SumInColumn = new int[5];
		BombsInColumn = new int[5];

		// Place Cards
		for(int i = 0; i < 5; ++i){
			for(int j = 0; j < 5; ++j){
				int multiplier;
				float p = Random.value;
				if(p < (float)Multipliers[0] / (float)(25 - ((5 * i) + j))){
					multiplier = 0;
				}
				else if(p < (float)(Multipliers[0] + Multipliers[1]) / (float)(25 - ((5 * i) + j))){
					multiplier = 1;
				}
				else if(p < (float)(Multipliers[0] + Multipliers[1] + Multipliers[2]) / (25 - ((5 * i) + j))){
					multiplier = 2;
				}
				else{
					multiplier = 3;
				}
				--Multipliers[multiplier];
				if(multiplier != 0){
					WinningScore *= multiplier;
				}

				Vector3 offset = new Vector3(j, -i, 0);
				Cards[i, j] = (GameObject)Instantiate(CardPrefab, transform.position + offset, Quaternion.identity, transform);
				Cards[i, j].GetComponent<CardController>().SetValue(multiplier);
				Cards[i, j].GetComponent<CardController>().Board = gameObject;
				Cards[i, j].GetComponent<CardController>().Scoreboard = Scoreboard;

				SumInRow[i] += multiplier;
				SumInColumn[j] += multiplier;

				BombsInRow[i] += multiplier == 0 ? 1 : 0;
				BombsInColumn[j] += multiplier == 0 ? 1 : 0;
			}
		}

		// Place Numbers
		for(int i = 0; i < 5; ++i){
			GameObject displayNumber;
			Vector3 offset;

			offset = new Vector2(5.875f, -(i + 0.125f));
			displayNumber = (GameObject)Instantiate(NumberPrefab, transform.position + offset, Quaternion.identity, transform);
			displayNumber.GetComponent<NumberController>().SetNumDigits(2);
			displayNumber.GetComponent<NumberController>().SetValue(SumInRow[i]);

			offset = new Vector2(5.875f, -(i + 0.53125f));
			displayNumber = (GameObject)Instantiate(NumberPrefab, transform.position + offset, Quaternion.identity, transform);
			displayNumber.GetComponent<NumberController>().SetValue(BombsInRow[i]);

			offset = new Vector2(i + 0.875f, -5.125f);
			displayNumber = (GameObject)Instantiate(NumberPrefab, transform.position + offset, Quaternion.identity, transform);
			displayNumber.GetComponent<NumberController>().SetNumDigits(2);
			displayNumber.GetComponent<NumberController>().SetValue(SumInColumn[i]);

			offset = new Vector2(i + 0.875f, -5.53125f);
			displayNumber = (GameObject)Instantiate(NumberPrefab, transform.position + offset, Quaternion.identity, transform);
			displayNumber.GetComponent<NumberController>().SetValue(BombsInColumn[i]);
		}

		InProgress = true;
	}

	private void ClearChildren(){
		foreach(Transform child in transform){
			Destroy(child.gameObject);
		}
	}

	private void GenerateLayouts(){
		Layouts = new List<int[]>[8];
		for(int i = 0; i < Layouts.Length; ++i){
			Layouts[i] = new List<int[]>();
		}
		Layouts[0].Add(new int[]{6, 0, 3, 1});
		Layouts[0].Add(new int[]{6, 0, 0, 3});
		Layouts[0].Add(new int[]{6, 0, 5, 0});
		Layouts[0].Add(new int[]{6, 0, 2, 2});
		Layouts[0].Add(new int[]{6, 0, 4, 1});

		Layouts[1].Add(new int[]{7, 0, 1, 3});
		Layouts[1].Add(new int[]{7, 0, 6, 0});
		Layouts[1].Add(new int[]{7, 0, 3, 2});
		Layouts[1].Add(new int[]{7, 0, 0, 4});
		Layouts[1].Add(new int[]{7, 0, 5, 1});

		Layouts[2].Add(new int[]{8, 0, 2, 3});
		Layouts[2].Add(new int[]{8, 0, 7, 0});
		Layouts[2].Add(new int[]{8, 0, 4, 2});
		Layouts[2].Add(new int[]{8, 0, 1, 4});
		Layouts[2].Add(new int[]{8, 0, 6, 1});

		Layouts[3].Add(new int[]{8, 0, 3, 3});
		Layouts[3].Add(new int[]{8, 0, 0, 5});
		Layouts[3].Add(new int[]{10, 0, 8, 0});
		Layouts[3].Add(new int[]{10, 0, 5, 2});
		Layouts[3].Add(new int[]{10, 0, 2, 4});

		Layouts[4].Add(new int[]{10, 0, 7, 1});
		Layouts[4].Add(new int[]{10, 0, 4, 3});
		Layouts[4].Add(new int[]{10, 0, 1, 5});
		Layouts[4].Add(new int[]{10, 0, 9, 0});
		Layouts[4].Add(new int[]{10, 0, 6, 2});

		Layouts[5].Add(new int[]{10, 0, 3, 4});
		Layouts[5].Add(new int[]{10, 0, 0, 6});
		Layouts[5].Add(new int[]{10, 0, 8, 1});
		Layouts[5].Add(new int[]{10, 0, 2, 5});

		Layouts[6].Add(new int[]{10, 0, 9, 1});
		Layouts[6].Add(new int[]{10, 0, 6, 3});

		Layouts[7].Add(new int[]{10, 0, 0, 7});
		Layouts[7].Add(new int[]{10, 0, 8, 2});
		Layouts[7].Add(new int[]{10, 0, 2, 6});
		Layouts[7].Add(new int[]{10, 0, 7, 3});


		for(int i = 0; i < Layouts.Length; ++i){
			for(int j = 0; j < Layouts[i].Count; ++j){
				Layouts[i][j][1] = 25 - Layouts[i][j][0] - Layouts[i][j][2] - Layouts[i][j][3];
			}
		}
	}

	public int GetScore(){
		return Score;
	}

	public bool IsLost(){
		return Score == 0;
	}

	public bool IsWon(){
		return Score == WinningScore;
	}

	public void Select(GameObject card){
		CardController cardController = card.GetComponent<CardController>();
		if(cardController.Selected && !cardController.IsFlipped() && !MemoOpen){
			cardController.Flip();
		}
		else{
			if(SelectedCard != null){
				SelectedCard.GetComponent<CardController>().Selected = false;
			}
			card.GetComponent<CardController>().Selected = true;
			SelectedCard = card;
			Cursor.transform.localPosition = card.transform.localPosition + (new Vector3(0, 0, -1));
		}
	}

	void Update(){
		if(InProgress){
			UpdateScore();
		}
	}

	void UpdateScore(){
		Score = 1;
		for(int i = 0; i < 5; ++i){
			for(int j = 0; j < 5; ++j){
				if(Cards[i, j].GetComponent<CardController>().IsFlipped()){
					Score *= Cards[i, j].GetComponent<CardController>().GetValue();
				}
			}
		}
	}
}
