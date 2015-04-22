using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class BattleScreen : MonoBehaviour {
	private const float DEFAULT_CONVEYOR_SPEED = 0.01f;
	private const float CONVEYOR_ITEM_PADDING = 20.0f;
	private const float CONVEYOR_BUTTON_PADDING = 1.5f;

	private bool running;
	private BattleController controller;
	private ClothingArea clothingArea;
	private ClothingSlotSystem clothingSlotSystem;
	private IList<Button> conveyorItems;
	private Button nextItem;
	private RectTransform conveyorTransform;
	private BattleResultsPanel resultsPanel;

	public float conveyorSpeed = DEFAULT_CONVEYOR_SPEED;
	public float timeLimit;
	public GameObject clothingAreaContainer;
	public GameObject itemSlotsPanel;
	public GameObject clothingConveyor;
	public GameObject results;
	public Button conveyorItem;
	public Text battleBlurb;
	public Text timerText;
	public Text outfitScore;
	public Text targetScore;
	public Image shopkeeper;
	public float maxHeight;

	public void Start() {
		running = false;
		controller = new BattleController(timeLimit);
		conveyorTransform = clothingConveyor.transform as RectTransform;
		conveyorItems = new List<Button>();
		nextItem = generateNextItem();

		if (maxHeight == 0) {
			maxHeight = conveyorTransform.rect.width;
		}

		clothingArea = clothingAreaContainer.GetComponentInChildren<ClothingArea>();
		clothingSlotSystem = itemSlotsPanel.GetComponentInChildren<ClothingSlotSystem>();
		clothingSlotSystem.Init(clothingArea, RemoveItem);

		battleBlurb.text = controller.GetBattleBlurb();
		targetScore.text = controller.TargetScore.ToString();
		timerText.text = controller.RemainingTime(0);
		Sprite shopkeeperSprite = controller.GetOpponent();
		shopkeeper.sprite = shopkeeperSprite;
		shopkeeper.rectTransform.sizeDelta = new Vector2(shopkeeperSprite.rect.width, shopkeeperSprite.rect.height);
		resultsPanel = results.GetComponent<BattleResultsPanel>();
		resultsPanel.Controller = controller;
	}

	public void Update() {
		if (running) {
			if (nextItemCanFit()) {
				setupNextItem();
			}

			if (lastItemIsOutOfBounds()) {
				removeLastItem();
			}
		}
	}

	public void FixedUpdate() {
		if (running) {
			moveConveyorItems();
			updateTimer(Time.deltaTime);

			if (controller.TimeOut()) {
				endBattle();
			}
		}
	}

	public void AcceptOutfit() {
		endBattle();
	}

	public void RemoveItem(ClothingSelection activeSelection) {
		int score = controller.RemoveItem(activeSelection.Clothing);
		outfitScore.text = score.ToString();
		clothingSlotSystem.UnsetActiveSlot();
	}

	public void StartBattle() {
		running = true;
	}

	public void Exit() {
		GlobalController.GetInstance().Back();
	}

	private Button generateNextItem() {
		Button newItem = Instantiate(conveyorItem) as Button;
		ClothingData item = controller.GenerateRandomItem();
		newItem.onClick.AddListener(() => { selectConveyorItem(newItem, item); });
		newItem.image.sprite = controller.GetCurrentItemSprite();
		Util.ScaleImageToMaxDimensions(newItem.image, newItem.image.sprite, conveyorTransform.rect.width, maxHeight);
		((RectTransform)newItem.transform).sizeDelta = new Vector2(conveyorTransform.rect.width,
			Math.Min(newItem.image.rectTransform.rect.height * CONVEYOR_BUTTON_PADDING, maxHeight));
		return newItem;
	}

	private void setupNextItem() {
		nextItem.transform.SetParent(conveyorTransform);
		nextItem.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		nextItem.transform.localPosition = new Vector3(0.0f, ((RectTransform)nextItem.transform).sizeDelta.y, 0.0f);
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
		int score = controller.UpdateOutfitScore(data);
		outfitScore.text = score.ToString();
	}

	private void updateTimer(float delta) {
		timerText.text = controller.RemainingTime(delta);
	}

	private void endBattle() {
		running = false;
		resultsPanel.EndBattle();
	}
}
