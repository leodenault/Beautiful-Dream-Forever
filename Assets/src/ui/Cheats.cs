using UnityEngine;
using System.Collections;

public class Cheats : MonoBehaviour {
	

	public void Update() {
		if (Input.anyKeyDown) {
			CheatsController.GetInstance().ActivateCheats();
		}
	}
}
