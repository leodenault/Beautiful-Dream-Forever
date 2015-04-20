using System.Collections.Generic;

public class StyleSynergy : ISynergy {
	public class Compatibility {
		public ClothingData.ClothingStyle Style;
		public int Points;
	}

	private static string FILE = "data/styleSynergies";

	private IDictionary<ClothingData.ClothingStyle, int> compatibilityPoints;
	
	public ClothingData.ClothingStyle Style;
	
	private Compatibility[] compatibilities;
	public Compatibility[] Compatibilities {
		get { return compatibilities; }
		set {
			compatibilities = value;
			compatibilityPoints = new Dictionary<ClothingData.ClothingStyle, int>();
			foreach (Compatibility compatibility in compatibilities) {
				compatibilityPoints.Add(compatibility.Style, compatibility.Points);
			}
		}
	}

	public int GetPoints(ClothingData left, ClothingData right) {
		return IsSynergetic(left, right) ? compatibilityPoints[right.Style] : 0;
	}

	public bool IsSynergetic(ClothingData left, ClothingData right) {
		if (left != null && right != null && left.Style == Style) {
			foreach (Compatibility compatibility in compatibilities) {
				if (compatibility.Style == right.Style) {
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
