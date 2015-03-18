using UnityEngine;
using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

public class ClothingManager {
	private static ClothingManager INSTANCE;
	private static string FILE = "data/clothing";
	
	private ClothingData[] clothingData;
    private IDictionary<ClothingData.ClothingStyle, List<ClothingData>> categories;

	private ClothingManager() {
		clothingData = Util.LoadXmlFile<ClothingData[]>(FILE);
		normalize();
        categorize();
	}

	public static ClothingManager GetInstance() {
		if (INSTANCE == null) {
			INSTANCE = new ClothingManager();
		}

		return INSTANCE;
	}

	private void normalize() {
		foreach (ClothingData datum in clothingData) {
			datum.Location.y = -datum.Location.y;
		}
	}

    private void categorize() {
        categories = new Dictionary<ClothingData.ClothingStyle, List<ClothingData>>();
        ClothingData.ClothingStyle[] styles = (ClothingData.ClothingStyle[])Enum.GetValues(typeof(ClothingData.ClothingStyle));
        foreach (ClothingData.ClothingStyle style in styles) {
            categories.Add(style, new List<ClothingData>());
        }

        foreach (ClothingData datum in clothingData) {
            categories[datum.Style].Add(datum);
        }
    }

    public ClothingData[] GetClothingData(ClothingData.ClothingStyle style) {
        if (style == ClothingData.ClothingStyle.NONE) {
            return clothingData;
        }

        return categories[style].ToArray();
    }
}
