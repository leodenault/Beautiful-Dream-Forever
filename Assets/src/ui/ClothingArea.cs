using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class ClothingArea : MonoBehaviour {
	private IDictionary<ClothingData.ClothingSlot, Image> slotImageDictionary;
	public IDictionary<ClothingData.ClothingSlot, Image> SlotImageDictionary {
		get {
			if (slotImageDictionary == null) {
				slotImageDictionary = new Dictionary<ClothingData.ClothingSlot, Image>();
				Image[] images = GetComponentsInChildren<Image>(true);
				ClothingData.ClothingSlot[] slots = (ClothingData.ClothingSlot[])Enum.GetValues(typeof(ClothingData.ClothingSlot));

				for (int i = 0; i < slots.Length && i < images.Length - 1; i++) {
					ClothingData.ClothingSlot slot = slots[i];
					Image image = images[i + 1];
					slotImageDictionary.Add(slot, image);
				}
			}

			return slotImageDictionary;
		}
	}
}
