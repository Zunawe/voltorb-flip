using System;
using UnityEngine;

public class DetectTapped : MonoBehaviour{
	Action Callback;

	void Update(){
		if(Input.touchCount == 1){
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			if(GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos)){
				Callback();
			}
		}
	}

	public void RegisterCallback(Action cb){
		Callback = cb;
	}
}
