public class EssenceSynergy : ISynergy {
	public ClothingData.ClothingEssence Essence;
	public ClothingData.ClothingStyle[] Styles;
	public int Points;

	public int GetPoints() {
		return Points;
	}

	public bool IsSynergetic(ClothingData data) {
		if (data != null && data.Essence == Essence) {
			foreach (ClothingData.ClothingStyle style in Styles) {
				if (data.Style == style) {
					return true;
				}
			}
		}

		return false;
	}
}
