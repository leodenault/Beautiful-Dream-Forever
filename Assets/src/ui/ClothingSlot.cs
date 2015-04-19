using UnityEngine;
using UnityEngine.UI;

public class ClothingSlot : MonoBehaviour {

	private int layer = int.MaxValue;
	public int Layer {
		get { return layer; }
	}
	
	public Image image;

	public void SetSlot(ClothingData data) {
		Sprite sprite = Resources.Load<Sprite>(data.Path);
		image.sprite = sprite;
		image.rectTransform.sizeDelta = new Vector2(sprite.rect.width, sprite.rect.height);
		image.rectTransform.localPosition = data.Location;
		image.gameObject.SetActive(true);
		layer = data.Layer;
	}

	public void Clear() {
		image.sprite = null;
		image.gameObject.SetActive(false);
	}
}
