using UnityEngine;
using UnityEngine.UI;

public class PrizeButton : MonoBehaviour {
	public Button button;

	private ClothingData data;
	public ClothingData Data {
		set {
			data = value;
			Sprite sprite = Resources.Load<Sprite>(data.Path);
			button.image.sprite = sprite;
			RectTransform rectTransform = ((RectTransform)transform.parent);
			Util.ScaleImageToMaxDimensions(
				button.image, sprite, rectTransform.rect.width / 3, rectTransform.rect.height);
		}
	}
}
