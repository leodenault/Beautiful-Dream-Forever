using System.Collections.Generic;

public class StyleSynergy : Synergy<ClothingData.ClothingStyle, ClothingData.ClothingStyle> {
	public static string FILE = "data/styleSynergies";

	protected override ClothingData.ClothingStyle extractLeftProperty(ClothingData left) {
		return left.Style;
	}

	protected override ClothingData.ClothingStyle extractRightProperty(ClothingData right) {
		return right.Style;
	}
}
