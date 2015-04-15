using UnityEngine;

public class TitleSceneLoader : MonoBehaviour {
	public void LoadScene(string sceneName) {
		Application.LoadLevel(sceneName);
	}
}
