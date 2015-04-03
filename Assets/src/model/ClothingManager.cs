using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class ClothingManager {
	private static ClothingManager INSTANCE;
	private static string FILE = "data/clothing";
	private static string PREFIX = "Assets/Resources/";
	private static int NEXT_ID = 0;
	
	private ClothingData[] clothingData;
    private IDictionary<ClothingData.ClothingStyle, List<ClothingData>> categories;

	private ClothingManager() {
		clothingData = Util.LoadXmlFile<ClothingData[]>(FILE);
		assignIds();
		validate();
		normalize();
        categorize();
	}

	public static ClothingManager GetInstance() {
		if (INSTANCE == null) {
			INSTANCE = new ClothingManager();
		}

		return INSTANCE;
	}

	private void assignIds() {
		foreach (ClothingData item in clothingData) {
			item.Id = NEXT_ID++;
		}
	}

	private void validate() {
		List<ClothingData> filtered = new List<ClothingData>();

		foreach (ClothingData item in clothingData) {
			string fullImagePath = string.Format("{0}{1}", item.Path, ".png");
			if (Resources.Load(item.Path) == null) {
				Debug.LogError(string.Format("Error while reading from file '{0}':\n" +
					"Clothing item '{1}' refers to image path '{2}', but image does not exist. This entry will be ignored",
					PREFIX + FILE + ".xml", item.Name, PREFIX + fullImagePath));
			} else {
				filtered.Add(item);
			}
		}

		clothingData = filtered.ToArray();
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
