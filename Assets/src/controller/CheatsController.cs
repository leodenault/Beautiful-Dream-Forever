using System;
using System.Collections.Generic;
using UnityEngine;

public class CheatsController {

	private static CheatsController INSTANCE;
	private delegate void CheatOperation();
	private static CheatSequence fillInventorySequence = new CheatSequence {
		KeyCode.UpArrow,
		KeyCode.UpArrow,
		KeyCode.DownArrow,
		KeyCode.DownArrow,
		KeyCode.LeftArrow,
		KeyCode.RightArrow,
		KeyCode.LeftArrow,
		KeyCode.RightArrow,
		KeyCode.B,
		KeyCode.A
	};
	private static CheatSequence clearInventorySequence = new CheatSequence {
		KeyCode.A,
		KeyCode.B,
		KeyCode.RightArrow,
		KeyCode.LeftArrow,
		KeyCode.RightArrow,
		KeyCode.LeftArrow,
		KeyCode.DownArrow,
		KeyCode.DownArrow,
		KeyCode.UpArrow,
		KeyCode.UpArrow
	};
	private IDictionary<CheatSequence, CheatOperation> operations = new Dictionary<CheatSequence, CheatOperation> {
		{ fillInventorySequence, fillInventory },
		{ clearInventorySequence, clearInventory }
	};

	private CheatsController() {}

	public static CheatsController GetInstance() {
		if (INSTANCE == null) {
			INSTANCE = new CheatsController();
		}

		return INSTANCE;
	}
	
	public void ActivateCheats() {
		foreach (CheatSequence sequence in operations.Keys) {
			if (sequence.Activate()) {
				operations[sequence]();
			}
		}
	}

	private static void fillInventory() {
		Protagonist.GetInstance().Inventory.Items = ClothingManager.GetInstance().GetClothingData();
	}

	private static void clearInventory() {
		Protagonist.GetInstance().Inventory.Clear();
	}
}
