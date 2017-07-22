using UnityEngine;
using UnityEngine.UI;

public class StatsPanel : MonoBehaviour {

	public Text styleText;
	public Text slotText;
	public Text essenceText;

	public void UpdateStats(ClothingData data) {
		if (data != null) {
			styleText.text = Util.ReadableEnumName<ClothingData.ClothingStyle>(data.Style);
			slotText.text = Util.ReadableEnumName<ClothingData.ClothingSlot>(data.Slot);
			essenceText.text = Util.ReadableEnumName<ClothingData.ClothingEssence>(data.Essence);
		}
	}
}
