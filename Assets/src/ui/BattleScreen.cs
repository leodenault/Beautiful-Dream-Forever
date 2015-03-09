using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

class BattleScreen : MonoBehaviour {
	private const float CONVEYOR_SPEED = 0.01f;

	private ClothingArea clothingArea;
	private ClothingSlotSystem clothingSlotSystem;
	private ClothingData[] clothingPool;
	private Sprite[] clothingPoolImages;
	private IList<Button> conveyorItems;
	private Button nextItem;
	private RectTransform conveyorTransform;
	
	public GameObject clothingAreaContainer;
	public GameObject itemSlotsPanel;
	public GameObject clothingConveyor;
	public Button conveyorItem;

	public void Start() {
		// TODO: Use a controller to access the correct clothing
		conveyorTransform = clothingConveyor.transform as RectTransform;
		clothingPool = (new ClothingManager("data/clothing")).GetClothingData(ClothingData.ClothingStyle.NONE);
		loadClothingPoolImages();
		conveyorItems = new List<Button>();
		nextItem = generateRandomItem();

		clothingArea = clothingAreaContainer.GetComponentInChildren<ClothingArea>();
		clothingSlotSystem = itemSlotsPanel.GetComponentInChildren<ClothingSlotSystem>();
		clothingSlotSystem.Init(clothingArea, null);
	}

	public void Update() {
		if (nextItemCanFit()) {
			setupNextItem();
		}

		if (lastItemIsOutOfBounds()) {
			removeLastItem();
		}
	}

	public void FixedUpdate() {
		moveConveyorItems();
	}

	private void loadClothingPoolImages() {
		clothingPoolImages = new Sprite[clothingPool.Length];
		for (int i = 0; i < clothingPool.Length; i++) {
			clothingPoolImages[i] = Resources.Load<Sprite>(clothingPool[i].Path);
		}
	}

	// TODO: Insert actual randomizer logic
	private Button generateRandomItem() {
		Button newItem = Instantiate(conveyorItem) as Button;
		newItem.image.sprite = clothingPoolImages[0];
		Util.ScaleImageToMaxDimensions(newItem.image, newItem.image.sprite, conveyorTransform.rect.width, conveyorTransform.rect.height, false);
		newItem.onClick.AddListener(() => { clothingSlotSystem.UpdateActiveSlot(clothingPool[0]); });
		return newItem;
	}

	private void setupNextItem() {
		nextItem.transform.SetParent(conveyorTransform);
		nextItem.image.rectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		nextItem.transform.localPosition = new Vector3(0.0f, nextItem.image.rectTransform.rect.height, 0.0f);
		conveyorItems.Insert(0, nextItem);
		nextItem = generateRandomItem();
	}

	private bool nextItemCanFit() {
		if (conveyorItems.Count == 0) {
			return true;
		}
		else {
			Button firstItem = conveyorItems[0];
			return firstItem.transform.localPosition.y < 0;
		}
	}

	private bool lastItemIsOutOfBounds() {
		Button lastItem = conveyorItems[conveyorItems.Count - 1];
		return lastItem.transform.localPosition.y < -conveyorTransform.rect.height;
	}

	private void removeLastItem() {
		int index = conveyorItems.Count - 1;
		Button item = conveyorItems[index];
		conveyorItems.RemoveAt(index);
		Destroy(item.gameObject);

	}

	private void moveConveyorItems() {
		foreach (Button item in conveyorItems) {
			item.transform.Translate(new Vector3(0.0f, -CONVEYOR_SPEED, 0.0f));
		}
	}
}
