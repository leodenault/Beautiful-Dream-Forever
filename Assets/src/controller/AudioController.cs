using UnityEngine;

public class AudioController {
	private static AudioController INSTANCE;

	private AudioSource source;
	private Playlist playlist;
	
	public static AudioController GetInstance() {
		if (INSTANCE == null) {
			INSTANCE = new AudioController();
		}

		return INSTANCE;
	}

	public void LoadPlaylist(AudioSource source, string name) {
		if (this.source == null) {
			this.source = GameObject.Instantiate(source) as AudioSource;
			GameObject.DontDestroyOnLoad(this.source);
			this.playlist = new Playlist(this.source);
		}

		this.playlist.Play(name);
	}

	public void Update(float delta) {
		playlist.Update(delta);
	}
}
