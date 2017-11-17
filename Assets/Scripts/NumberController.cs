using UnityEngine;

public class NumberController : MonoBehaviour{

	public GameObject Digit;

	private int Value;
	private GameObject[] Digits;

	void Awake(){
		Digits = new GameObject[2];
		Digits[0] = (GameObject)Instantiate(Digit, transform.position, Quaternion.identity, transform);
		Digits[1] = (GameObject)Instantiate(Digit, transform.position + new Vector3(-0.25f, 0.0f, 0.0f), Quaternion.identity, transform);

		SetValue(0);
	}

	public void SetValue(int v){
		Value = v;
		Digits[0].GetComponent<DigitController>().SetValue(Value % 10);
		Digits[1].GetComponent<DigitController>().SetValue((Value / 10) % 10);
	}
}
