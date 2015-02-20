using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopMenu : MonoBehaviour {

    public GameObject showMenu;

    public void ShowMenu()
    {
        showMenu.SetActive(!showMenu.activeSelf);
        Debug.Log("Creeper Creeper Creeper");
    }


}
