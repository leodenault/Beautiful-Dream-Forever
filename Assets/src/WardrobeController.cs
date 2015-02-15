using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WardrobeController {
    private static WardrobeController INSTANCE;

    private ClothingManager manager;

    private WardrobeController() {
        this.manager = new ClothingManager("Assets/Resources/data/clothing.xml");
    }

    public static WardrobeController GetInstance() {
        if (INSTANCE == null) {
            INSTANCE = new WardrobeController();
        }

        return INSTANCE;
    }

    public void AssignClothingBackrounds(Button[] wardrobeButtons) {
        string[] images = manager.GetClothingImages();

        for (int i = 0; i < wardrobeButtons.Length && i < images.Length; i++) {
            Image[] buttonImages = wardrobeButtons[i].GetComponentsInChildren<Image>();

            if (buttonImages.Length > 1) {
                Image image = buttonImages[1];
                Sprite sprite = Resources.Load<Sprite>(images[i]);
                image.sprite = sprite;

                float spriteHeight = sprite.rect.height;
                float imageHeight = image.rectTransform.sizeDelta.y;
                float scale = imageHeight / spriteHeight;

                image.rectTransform.sizeDelta = new Vector2(sprite.rect.width * scale, image.rectTransform.sizeDelta.y);
                image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }
    }
}
