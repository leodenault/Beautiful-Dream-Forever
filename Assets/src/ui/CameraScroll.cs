using UnityEngine;
using System.Collections;

public class CameraScroll : MonoBehaviour {

	public Camera mallCamera;
	private const float X_MIN = -1.8f;
	private const float X_MAX = 1.8f;

	// Update is called once per frame
	void Update () {
		float xAxisValue = Input.GetAxis("Horizontal");
		if (Camera.current != null) {
			float xCur = mallCamera.transform.position.x;
			xAxisValue = Mathf.Clamp(xAxisValue, X_MIN - xCur, X_MAX - xCur);
			mallCamera.transform.Translate(new Vector3(xAxisValue, 0.0f, 0.0f));
		}
	}
}
