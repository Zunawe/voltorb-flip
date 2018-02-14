using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardController : MonoBehaviour{
	public GameObject NumberPrefab;

	private int Total;
	private GameObject DisplayTotal;
	private int Current;
	private GameObject DisplayCurrent;

	// Use this for initialization
	void Awake(){
		Total = 0;
		Current = 0;

		DisplayTotal = (GameObject)Instantiate(NumberPrefab, transform.position, Quaternion.identity, transform);
		DisplayTotal.GetComponent<NumberController>().SetNumDigits(5);
		DisplayTotal.GetComponent<NumberController>().SetValue(Total);
		DisplayTotal.GetComponent<NumberController>().UseBigSprites(true);
		DisplayTotal.transform.parent = gameObject.transform;
		DisplayTotal.transform.localPosition = new Vector3(5.78125f, -0.28125f, -1);

		DisplayCurrent = (GameObject)Instantiate(NumberPrefab, transform.position, Quaternion.identity, transform);
		DisplayCurrent.GetComponent<NumberController>().SetNumDigits(5);
		DisplayCurrent.GetComponent<NumberController>().SetValue(Current);
		DisplayCurrent.GetComponent<NumberController>().UseBigSprites(true);
		DisplayCurrent.transform.parent = gameObject.transform;
		DisplayCurrent.transform.localPosition = new Vector3(5.78125f, -1.53125f, -1);
	}

	public void MultiplyScore(int multiplier){
		Current = Current == 0 ? multiplier : Current * multiplier;
		UpdateDisplay();
	}

	public void Flush(){
		Total += Current;
		Current = 0;
		UpdateDisplay();
	}

	private void UpdateDisplay(){
		DisplayCurrent.GetComponent<NumberController>().SetValue(Current);
		DisplayTotal.GetComponent<NumberController>().SetValue(Total);
	}
}
