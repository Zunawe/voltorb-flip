using System;
using UnityEngine;

public class DetectTapped : MonoBehaviour{
	Action Callback;

	void Update(){
		if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended){
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			if(GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos)){
				Callback();
			}
		}
	}

#if UNITY_EDITOR
	void OnMouseUp(){
		Callback();
	}
#endif

	public void RegisterCallback(Action cb){
		Callback = cb;
	}
}
