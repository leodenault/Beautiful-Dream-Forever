using UnityEngine;
using System.Collections;

public class CameraScroll : MonoBehaviour {
	
	public Camera mallCamera;
	private float X_MIN;
	private float X_MAX;
	
	void Start () {
		/* Determine camera bounds based on screen size and stuff. Based on
		 * http://answers.unity3d.com/questions/501893/calculating-2d-camera-bounds.html */
		float cameraWidth = Camera.main.camera.orthographicSize * Screen.width / Screen.height;
		Debug.Log (cameraWidth);
		X_MIN = cameraWidth - 6.65f; //6.65 was determined by testing until it worked
		X_MAX = 6.65f - cameraWidth;
	}
			
	// Update is called once per frame
	void Update () {
		float curX = mallCamera.transform.position.x;
		float xAxisValue = Mathf.Clamp(Input.GetAxis("Horizontal"), X_MIN - curX, X_MAX - curX);
		if (Camera.current != null) {
			mallCamera.transform.Translate(new Vector3(xAxisValue, 0.0f, 0.0f));
		}
	}
}
