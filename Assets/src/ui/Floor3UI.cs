using UnityEngine;
using System.Collections;

public class Floor3UI : FloorUI {

	public void LoadIdealist()
	{
		controller.Forward("Idealist Shop");
	}

	public void LoadHardcore()
	{
		controller.Forward("Hardcore Shop");
	}

	public void LoadFloor2()
	{
		controller.Forward("Floor 2");
	}
}

