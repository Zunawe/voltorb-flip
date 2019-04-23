using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    // Start is called before the first frame update
    void Start () {
        float aspectRatio = (float)Camera.main.pixelWidth / (float)Camera.main.pixelHeight;
        Camera.main.orthographicSize = 3.0f / aspectRatio;
    }
}
