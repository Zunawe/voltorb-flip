using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour {
  void Awake () {
    #if UNITY_STANDALONE
      Screen.SetResolution(384, 684, true);
      Screen.fullScreen = false;
    #endif
  }

  void Start () {
    #if UNITY_ANDROID
      float aspectRatio = (float)Camera.main.pixelWidth / (float)Camera.main.pixelHeight;
      Camera.main.orthographicSize = 3.0f / aspectRatio;
    #endif
  }
}
