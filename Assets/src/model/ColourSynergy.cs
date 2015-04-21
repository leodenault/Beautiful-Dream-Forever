using System.Collections.Generic;

public class ColourSynergy : Synergy<ClothingData.ClothingStyle, ClothingData.ClothingColour> {
	public static string FILE = "data/colourSynergies";

	protected override ClothingData.ClothingStyle extractLeftProperty(ClothingData left) {
		return left.Style;
	}

	protected override ClothingData.ClothingColour extractRightProperty(ClothingData right) {
		return right.Colour;
	}
}
