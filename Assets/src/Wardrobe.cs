using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Wardrobe : MonoBehaviour {

    public Button equipButton;
    public Sprite unequipImage;

    public void Equip() {
        equipButton.image.sprite = unequipImage;
    }
}
