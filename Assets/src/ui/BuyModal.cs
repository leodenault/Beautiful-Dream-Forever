using UnityEngine;
using UnityEngine.UI;

public class BuyModal : MonoBehaviour {

	private ClothingSystemController controller;
	public ClothingSystemController Controller {
		set { controller = value; }
	}

	private float itemWidth;
	private float itemHeight;
	private ClothingData data;
	private BuyCallback callback;

	public delegate void BuyCallback();

	public string pricePrefix;
	public string ownedPrefix;
	public string purchasableMessage;
	public string notEnoughMessage;
	public Image item;
	public Text purchaseMessage;
	public Text priceText;
	public Text ownedText;
	public Button acceptButton;
	public GameObject buyDialog;
	public GameObject successfulPurchase;

	public void Show(ClothingRepresentation selection, BuyCallback callback) {
		this.callback = callback;

		buyDialog.gameObject.SetActive(true);
		successfulPurchase.gameObject.SetActive(false);

		initializeItemDimensions();
		Sprite sprite = selection.Sprite;
		item.sprite = sprite;
		Util.ScaleImageToMaxDimensions(item, sprite, itemWidth, itemHeight);

		data = selection.Clothing;
		int price = data.Price;
		int balance = controller.GetBalance();
		
		priceText.text = string.Format("{0}: {1}", pricePrefix, price);
		ownedText.text = string.Format("{0}: {1}", ownedPrefix, balance);

		if (controller.Purchasable(data)) {
			purchaseMessage.text = purchasableMessage;
			acceptButton.interactable = true;
		} else {
			purchaseMessage.text = notEnoughMessage;
			acceptButton.interactable = false;
		}
	}

	public void Buy() {
		if (controller.Buy(data)) {
			callback();
			buyDialog.gameObject.SetActive(false);
			successfulPurchase.gameObject.SetActive(true);
		} else {
			Debug.LogError("Accept button for purchasing item should have been disabled");
		}
	}

	// Since Unity doesn't guarantee calling Start before SetItem, we have to run this
	// magic or else the dimensions will be 0,0
	private void initializeItemDimensions() {
		if (itemWidth == 0 && itemHeight == 0) {
			itemWidth = item.rectTransform.rect.width;
			itemHeight = item.rectTransform.rect.height;
		}
	}
}
