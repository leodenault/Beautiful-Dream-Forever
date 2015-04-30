public class EssenceSynergy : Synergy<ClothingData.ClothingEssence, ClothingData.ClothingStyle> {
	public static string FILE = "data/essenceSynergies";

	protected override ClothingData.ClothingEssence extractLeftProperty(ClothingData left) {
		return left.Essence;
	}

	protected override ClothingData.ClothingStyle extractRightProperty(ClothingData right) {
		return right.Style;
	}
}
