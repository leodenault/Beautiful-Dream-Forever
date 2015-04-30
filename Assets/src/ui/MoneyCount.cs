using UnityEngine;
using UnityEngine.UI;

public class MoneyCount : MonoBehaviour {

	public Text text;

	public void Start() {
		UpdateFunds();
	}

	public void UpdateFunds() {
		text.text = Protagonist.GetInstance().Balance.ToString();
	}
}
