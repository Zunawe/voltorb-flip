using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutController : MonoBehaviour{
	public GameObject BoardGameObject;
	private BoardController Board;

	private float ResetTimer = 0;
	private bool IsResetting = false;

	void Awake(){
		Board = BoardGameObject.GetComponent<BoardController>();
		Board.GenerateBoard(1);
	}
	
	void Update(){
		if(Board.IsLost()){
			Reset();
		}

		if(Board.IsWon()){
			Reset();
		}
	}

	public void Reset(){
		if(!IsResetting){
			ResetTimer = 3;
			IsResetting = true;
		}
		else if(ResetTimer > 0){
			ResetTimer -= Time.deltaTime;
		}
		else{
			IsResetting = false;
			Board.GenerateBoard(1);
		}
	}
}
