using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Wardrobe : MonoBehaviour {
    private bool isEquipped;

    public Button equipButton;
    public Sprite equipImage;
    public Sprite unequipImage;

    public void Start()
    {
        isEquipped = false;
    }

    public void Equip(){
        isEquipped = !isEquipped;

        if (isEquipped)
        {
            equipButton.image.sprite = unequipImage;
        }
        else
        {
            equipButton.image.sprite = equipImage;
        }
        
    }
}
