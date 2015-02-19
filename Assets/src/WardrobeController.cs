using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class WardrobeController {
    private static WardrobeController INSTANCE;
    private static float ITEM_WIDTH = 42.0f;
    private static float ITEM_HEIGHT = 35.0f;
    private static float SCALE_MODIFIER = 0.85f;

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

    private float computeScale(float width1, float width2, float height1, float height2) {
        float wScale = width1 / width2;
        float hScale = height1 / height2;

        return Math.Min(wScale, hScale) * SCALE_MODIFIER;
    }

    public void AssignClothingBackrounds(Button[] wardrobeButtons) {
        string[] images = manager.GetClothingImages();

        for (int i = 0; i < wardrobeButtons.Length && i < images.Length; i++) {
            Image[] buttonImages = wardrobeButtons[i].GetComponentsInChildren<Image>();

            if (buttonImages.Length > 1) {
                Image image = buttonImages[1];
                Sprite sprite = Resources.Load<Sprite>(images[i]);
                image.sprite = sprite;

                float spriteWidth = sprite.rect.width;
                float spriteHeight = sprite.rect.height;
                float scale = computeScale(ITEM_WIDTH, spriteWidth, ITEM_HEIGHT, spriteHeight);

                image.rectTransform.sizeDelta = new Vector2(spriteWidth * scale, spriteHeight * scale);
                image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }
    }
}
