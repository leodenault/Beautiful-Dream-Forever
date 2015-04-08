using UnityEngine;
using System.Collections;

public class ShopkeeperButton : MonoBehaviour
{
	private static string DRESSINGROOM_SCENE_SUFFIX = " Shop Dressingroom";

    private GlobalController controller;

	public ClothingData.ClothingStyle shopStyle;

    public void Start() {
        controller = GlobalController.GetInstance();
    }

    public void LoadDressingRoom() {
		string shopType = Util.ConvertStyleEnumToReadable(shopStyle);
        controller.Forward(string.Format("{0}{1}", shopType, DRESSINGROOM_SCENE_SUFFIX));
    }

}