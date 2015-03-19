using UnityEngine;
using System.Collections;

public class TempPreppyShopkeeperButton : MonoBehaviour
{

    GlobalController controller;

    public void Start()
    {
        controller = GlobalController.GetInstance();
    }

    public void LoadPreppyDressing()
    {
        controller.Forward("Preppy Shop Dressingroom");
    }

}