using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class BattleScreen : MonoBehaviour {
	private const float DEFAULT_CONVEYOR_SPEED = 0.01f;
	private const float CONVEYOR_ITEM_PADDING = 20.0f;

	private bool started;
	private GlobalController globalController;
	private BattleController battleController;
	private ClothingArea clothingArea;
	private ClothingSlotSystem clothingSlotSystem;
	private IList<Button> conveyorItems;
	private Button nextItem;
	private RectTransform conveyorTransform;

	public float conveyorSpeed = DEFAULT_CONVEYOR_SPEED;
	public float timeLimit;
	public GameObject clothingAreaContainer;
	public GameObject itemSlotsPanel;
	public GameObject clothingConveyor;
	public Button conveyorItem;
	public Text timerText;
	public Text outfitScore;
	public Text targetScore;
	public Image shopkeeper;
	public float maxHeight;

	public void Start() {
		started = false;
		globalController = GlobalController.GetInstance();
		battleController = new BattleController(ShopController.GetInstance().ShopStyle, timeLimit);
		conveyorTransform = clothingConveyor.transform as RectTransform;
		conveyorItems = new List<Button>();
		nextItem = generateNextItem();

		if (maxHeight == 0) {
			maxHeight = conveyorTransform.rect.width;
		}

		clothingArea = clothingAreaContainer.GetComponentInChildren<ClothingArea>();
		clothingSlotSystem = itemSlotsPanel.GetComponentInChildren<ClothingSlotSystem>();
		clothingSlotSystem.Init(clothingArea, RemoveItem);

		targetScore.text = battleController.TargetScore.ToString();
		timerText.text = battleController.RemainingTime(0);
		Sprite shopkeeperSprite = battleController.GetShopkeeper();
		shopkeeper.sprite = shopkeeperSprite;
		shopkeeper.rectTransform.sizeDelta = new Vector2(shopkeeperSprite.rect.width, shopkeeperSprite.rect.height);
	}

	public void Update() {
		if (started) {
			if (nextItemCanFit()) {
				setupNextItem();
			}

			if (lastItemIsOutOfBounds()) {
				removeLastItem();
			}
		}
	}

	public void FixedUpdate() {
		if (started) {
			moveConveyorItems();
			updateTimer(Time.deltaTime);

			if (battleController.TimeOut()) {
				globalController.Back();
			}
		}
	}

	public void AcceptOutfit() {
		// TODO: Handle end of battle sequence
		globalController.Back();
	}

	public void RemoveItem(ClothingSelection activeSelection) {
		int score = battleController.RemoveItem(activeSelection.Clothing);
		outfitScore.text = score.ToString();
		clothingSlotSystem.UnsetActiveSlot();
	}

	public void StartBattle() {
		started = true;
	}

	private Button generateNextItem() {
		Button newItem = Instantiate(conveyorItem) as Button;
		ClothingData item = battleController.GenerateRandomItem();
		newItem.onClick.AddListener(() => { selectConveyorItem(newItem, item); });
		newItem.image.sprite = battleController.GetCurrentItemSprite();
		Util.ScaleImageToMaxDimensions(newItem.image, newItem.image.sprite, conveyorTransform.rect.width, maxHeight);
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
			item.transform.Translate(new Vector3(0.0f, -conveyorSpeed, 0.0f));
		}
	}

	private void selectConveyorItem(Button item, ClothingData data) {
		removeItem(item);
		clothingSlotSystem.UpdateActiveSlot(data);
		int score = battleController.UpdateOutfitScore(data);
		outfitScore.text = score.ToString();
	}

	private void updateTimer(float delta) {
		timerText.text = battleController.RemainingTime(delta);
	}
}
