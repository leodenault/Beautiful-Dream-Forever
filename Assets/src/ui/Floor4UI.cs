using UnityEngine;
using System.Collections;

public class Floor4UI : FloorUI
{

	public void LoadFormal()
	{
		controller.Forward("Formal Shop");
	}

	public void LoadCosplay()
	{
		controller.Forward("Cosplay Shop");
	}

	public void LoadFloor3()
	{
		controller.Forward("Floor 3");
	}
}
