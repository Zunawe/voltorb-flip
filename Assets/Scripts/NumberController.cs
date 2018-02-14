using UnityEngine;

public class NumberController : MonoBehaviour{
	public GameObject Digit;
	public int NumDigits;

	private bool BigSprites;
	private int Value;
	private GameObject[] Digits;

	void Awake(){
		BigSprites = false;
		SetNumDigits(1);
	}

	public void UseBigSprites(bool value){
		BigSprites = value;
		SetNumDigits(NumDigits);
	}

	public void SetNumDigits(int newValue){
		foreach(Transform child in transform){
			Destroy(child.gameObject);
		}

		NumDigits = newValue;
		Digits = new GameObject[NumDigits];
		for(int i = 0; i < NumDigits; ++i){
			Digits[i] = (GameObject)Instantiate(Digit, transform.position + new Vector3((BigSprites ? -0.5f : -0.25f) * i, 0.0f, 0.0f), Quaternion.identity, transform);
			Digits[i].GetComponent<DigitController>().BigSprite = BigSprites;
		}
		SetValue(Value);
	}

	public void SetValue(int v){
		Value = v;
		for(int i = 0; i < NumDigits; ++i){
			Digits[i].GetComponent<DigitController>().SetValue((Value / (int)(Mathf.Pow(10.0f, i))) % 10);
		}
	}

	public int GetValue(){
		return Value;
	}
}
