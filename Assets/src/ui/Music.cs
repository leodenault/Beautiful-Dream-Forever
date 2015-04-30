using UnityEngine;
using System.Collections.Generic;

public class Music : MonoBehaviour {
	private AudioController controller;

	public string playlist;

	public AudioSource source;

	public void Start() {
		controller = AudioController.GetInstance();
		controller.LoadPlaylist(source, playlist);
	}

	public void Update() {
		controller.Update(Time.deltaTime);
	}
}
