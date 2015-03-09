using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class BattleScreen : MonoBehaviour {
	private const float CONVEYOR_SPEED = 0.01f;
	private const float CONVEYOR_ITEM_PADDING = 20.0f;

	private BattleController controller;
	private ClothingArea clothingArea;
	private ClothingSlotSystem clothingSlotSystem;
	private IList<Button> conveyorItems;
	private Button nextItem;
	private RectTransform conveyorTransform;
	
	public GameObject clothingAreaContainer;
	public GameObject itemSlotsPanel;
	public GameObject clothingConveyor;
	public Button conveyorItem;

	public void Start() {
		controller = new BattleController();
		conveyorTransform = clothingConveyor.transform as RectTransform;
		conveyorItems = new List<Button>();
		nextItem = generateNextItem();

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

	private Button generateNextItem() {
		Button newItem = Instantiate(conveyorItem) as Button;
		ClothingData item = controller.GenerateRandomItem();
		newItem.onClick.AddListener(() => { selectConveyorItem(newItem, item); });
		newItem.image.sprite = controller.GetCurrentItemSprite();
		Util.ScaleImageToMaxDimensions(newItem.image, newItem.image.sprite, conveyorTransform.rect.width, conveyorTransform.rect.height);
		return newItem;
	}

	private void setupNextItem() {
		nextItem.transform.SetParent(conveyorTransform);
		nextItem.image.rectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		nextItem.transform.localPosition = new Vector3(0.0f, nextItem.image.rectTransform.rect.height, 0.0f);
		conveyorItems.Insert(0, nextItem);
		nextItem = generateNextItem();
	}

	private bool nextItemCanFit() {
		if (conveyorItems.Count == 0) {
			return true;
		}
		else {
			Button firstItem = conveyorItems[0];
			return firstItem.transform.localPosition.y < -CONVEYOR_ITEM_PADDING;
		}
	}

	private bool lastItemIsOutOfBounds() {
		Button lastItem = conveyorItems[conveyorItems.Count - 1];
		return lastItem.transform.localPosition.y < -conveyorTransform.rect.height;
	}

	private void removeLastItem() {
		int index = conveyorItems.Count - 1;
		Button item = conveyorItems[index];
		removeItem(item);
	}

	private void removeItem(Button item) {
		conveyorItems.Remove(item);
		Destroy(item.gameObject);
	}

	private void moveConveyorItems() {
		foreach (Button item in conveyorItems) {
			item.transform.Translate(new Vector3(0.0f, -CONVEYOR_SPEED, 0.0f));
		}
	}

	private void selectConveyorItem(Button item, ClothingData data) {
		removeItem(item);
		clothingSlotSystem.UpdateActiveSlot(data);
	}
}
