using UnityEngine;
using System.Collections;

public class CameraScroll : MonoBehaviour {

	public Camera mallCamera;

	// Update is called once per frame
	void Update () {
		float xAxisValue = Input.GetAxis("Horizontal");
		if (Camera.current != null) {
			mallCamera.transform.Translate(new Vector3(xAxisValue, 0.0f, 0.0f));
		}
	}
}
