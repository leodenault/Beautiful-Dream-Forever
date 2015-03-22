using UnityEngine;
using System.Collections;

public class Floor3UI : MonoBehaviour
{

	GlobalController controller;

	public void Start()
	{
		controller = GlobalController.GetInstance();
	}

	public void LoadIdealist()
	{
		controller.Forward("Idealist Shop");
	}

	public void LoadHardcore()
	{
		controller.Forward("Hardcore Shop");
	}

	public void LoadFloor4()
	{
		controller.Forward("Floor 4");
	}

	public void LoadFloor2()
	{
		controller.Forward("Floor 2");
	}
}

