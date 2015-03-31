﻿using UnityEngine;
using System.Collections;

public class CameraScroll : MonoBehaviour {

	private float X_MIN;
	private float X_MAX;
	private float translation;

	public Camera mallCamera;
	public float scrollSpeed = 0.1f;
	
	public void Start () {
		/* Determine camera bounds based on screen size and stuff. Based on
		 * http://answers.unity3d.com/questions/501893/calculating-2d-camera-bounds.html */
		float cameraWidth = Camera.main.camera.orthographicSize * Screen.width / Screen.height;
		X_MIN = cameraWidth - 6.65f; //6.65 was determined by testing until it worked
		X_MAX = 6.65f - cameraWidth;
		translation = 0.0f;
	}

	public void FixedUpdate() {
		if (translation != 0.0f) {
			float curX = mallCamera.transform.position.x;
			float corrected = Mathf.Clamp(translation, X_MIN - curX, X_MAX - curX);
			mallCamera.transform.Translate(new Vector3(corrected, 0.0f, 0.0f));
		}
	}
			
	public void ScrollLeft() {
		translation = -scrollSpeed;
	}

	public void ScrollRight() {
		translation = scrollSpeed;
	}

	public void EndScroll() {
		translation = 0.0f;
	}
}
