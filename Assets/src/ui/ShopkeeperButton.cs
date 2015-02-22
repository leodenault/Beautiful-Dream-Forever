using UnityEngine;
using System.Collections;

public class ShopkeeperButton : MonoBehaviour
{

    GlobalController controller;

    public void Start() {
        controller = GlobalController.GetInstance();
    }

    public void LoadAthleticDressing() {
        controller.Forward("Athletic Shop Dressingroom");
    }

}