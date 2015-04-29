using UnityEngine;
using System.Collections.Generic;

public class Playlist {
	private static string PLAYLIST_DIRECTORY = "music/";

	private AudioSource source;
	private string playlistName;
	private List<AudioClip> playlist;
	private int position;
	private float clipLength;
	private float elapsedTime;

	public Playlist(AudioSource source) {
		this.source = source;
	}

	public void Play(string playlistName) {
		if (!playlistName.Equals(this.playlistName)) {
			List<AudioClip> list = new List<AudioClip>(Resources.LoadAll<AudioClip>(PLAYLIST_DIRECTORY + playlistName));

			if (list.Count > 0) {
				this.playlistName = playlistName;
				playlist = randomize(list);
				playNext();
			}
		}
	}

	public void Update(float delta) {
		elapsedTime += delta;

		if (elapsedTime > clipLength && playlist != null) {
			playNext();
		}
	}

	private void playNext() {
		if (position == playlist.Count) {
			position = 0;
			playlist = randomize(playlist);
		}

		source.clip = playlist[position++];
		clipLength = source.clip.length;
		elapsedTime = 0;
		source.Play();
	}

	private List<AudioClip> randomize(List<AudioClip> original) {
		List<AudioClip> randomized;
		
		if (original.Count == 1) {
			randomized = original;
		} else {
			System.Random random = new System.Random();
			List<AudioClip> source = new List<AudioClip>(original);
			randomized = new List<AudioClip>();
			int first = random.Next(source.Count);
			AudioClip firstClip = source[first];

			// TODO: This will bug out if the playlist is 2 songs long, where the playlist
			// will ALWAYS start with the first song
			if (firstClip.name.Equals(original[original.Count - 1].name)) {
				first = (first + 1) % source.Count;
			}

			randomized.Add(source[first]);
			source.RemoveAt(first);

			while (source.Count > 0) {
				int index = random.Next(source.Count);
				randomized.Add(source[index]);
				source.RemoveAt(index);
			}
		}

		position = 0;
		return randomized;
	}
}
