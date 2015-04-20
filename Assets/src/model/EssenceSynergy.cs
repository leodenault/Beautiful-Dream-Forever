public class EssenceSynergy : ISynergy {
	private static string FILE = "data/essenceSynergies";

	public ClothingData.ClothingEssence Essence;
	public ClothingData.ClothingStyle[] Styles;
	public int Points;

	public int GetPoints(ClothingData left, ClothingData right) {
		return IsSynergetic(left, right) ? Points : 0;
	}

	public bool IsSynergetic(ClothingData left, ClothingData right) {
		if (left != null && right != null && left.Essence == Essence) {
			foreach (ClothingData.ClothingStyle style in Styles) {
				if (right.Style == style) {
					return true;
				}
			}
		}

		return false;
	}

	public static string FileName() {
		return FILE;
	}
}
