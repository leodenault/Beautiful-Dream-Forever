using UnityEngine;
using System.Collections;

public class CameraScroll : MonoBehaviour {

	public Camera camera;

	// Update is called once per frame
	void Update () {
		float xAxisValue = Input.GetAxis("Horizontal");
		if (Camera.current != null) {
			camera.transform.Translate(new Vector3(xAxisValue, 0.0f, 0.0f));
		}
	}
}
