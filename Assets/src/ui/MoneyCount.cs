using UnityEngine;
using UnityEngine.UI;

public class MoneyCount : MonoBehaviour {

	public Text text;

	public void Start() {
		text.text = Protagonist.GetInstance().Balance.ToString();
	}
}
