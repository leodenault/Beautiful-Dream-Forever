using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraScroll : MonoBehaviour {

	private float X_MIN;
	private float X_MAX;
	private float translation;

	public Camera mallCamera;
	public Button leftButton;
	public Button rightButton;
	public float scrollSpeed = 0.1f;
	
	public void Start () {
		/* Determine camera bounds based on screen size and stuff. Based on
		 * http://answers.unity3d.com/questions/501893/calculating-2d-camera-bounds.html */
		float cameraWidth = Camera.main.camera.orthographicSize * Screen.width / Screen.height;
		X_MIN = cameraWidth - 6.65f; //6.65 was determined by testing until it worked
		X_MAX = 6.65f - cameraWidth;
		translation = 0.0f;
	}

	public void Update() {
		float axis = Input.GetAxisRaw("Horizontal");
		if (axis != 0.0f) {
			translation = axis * scrollSpeed;
		} else if ((translation < 0.0f && (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))) ||
			(translation > 0.0f && (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)))) {
			EndScroll();
		}
	}

	public void FixedUpdate() {
		if (translation != 0.0f) {
			float curX = mallCamera.transform.position.x;
			float corrected = Mathf.Clamp(translation, X_MIN - curX, X_MAX - curX);
			mallCamera.transform.Translate(new Vector3(corrected, 0.0f, 0.0f));

			setButtonVisibility(rightButton, curX, X_MAX);
			setButtonVisibility(leftButton, curX, X_MIN);
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

	private void setButtonVisibility(Button button, float camerPosition, float border) {
		if (camerPosition == border) {
			button.gameObject.SetActive(false);
		} else {
			button.gameObject.SetActive(true);
		}
	}
}
