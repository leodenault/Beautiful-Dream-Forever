using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Wardrobe : MonoBehaviour {
    private bool isEquipped;
    private WardrobeController controller;

    public Button equipButton;
    public Sprite equipImage;
    public Sprite unequipImage;

    public GameObject pageTilePanel;

    public void Start()
    {
        isEquipped = false;
        controller = WardrobeController.GetInstance();
        Button[] pageTiles = pageTilePanel.GetComponentsInChildren<Button>();
        controller.AssignClothingBackrounds(pageTiles);
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
