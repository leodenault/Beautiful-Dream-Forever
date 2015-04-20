public class EssenceSynergy : ISynergy {
	public ClothingData.ClothingEssence Essence;
	public ClothingData.ClothingStyle[] Styles;
	public int Points;

	public int GetPoints() {
		return Points;
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
}
