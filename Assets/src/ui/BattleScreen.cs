using UnityEngine;

class BattleScreen : MonoBehaviour {
	private ClothingArea clothingArea;
	private ClothingSlotSystem clothingSlotSystem;
	
	public GameObject clothingAreaContainer;
	public GameObject itemSlotsPanel;

	public void Start() {
		clothingArea = clothingAreaContainer.GetComponentInChildren<ClothingArea>();
		clothingSlotSystem = itemSlotsPanel.GetComponentInChildren<ClothingSlotSystem>();
		clothingSlotSystem.Init(clothingArea, null);
	}
}
