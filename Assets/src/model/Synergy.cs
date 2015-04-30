using System.Collections.Generic;

public class Compatibility<Right> {
	public Right Synergy;
	public int Points;
}

public abstract class Synergy<Left, Right> : ISynergy {

	protected IDictionary<Right, int> compatibilityPoints;

	public Left Tag;

	private Compatibility<Right>[] compatibilities;
	public Compatibility<Right>[] Compatibilities {
		get { return compatibilities; }
		set {
			compatibilities = value;
			compatibilityPoints = new Dictionary<Right, int>();
			foreach (Compatibility<Right> compatibility in compatibilities) {
				compatibilityPoints.Add(compatibility.Synergy, compatibility.Points);
			}
		}
	}

	public int GetPoints(ClothingData left, ClothingData right) {
		return IsSynergetic(left, right) ? compatibilityPoints[extractRightProperty(right)] : 0;
	}

	public bool IsSynergetic(ClothingData left, ClothingData right) {
		if (left != null && right != null && extractLeftProperty(left).Equals(Tag)) {
			foreach (Compatibility<Right> compatibility in compatibilities) {
				if (compatibility.Synergy.Equals(extractRightProperty(right))) {
					return true;
				}
			}
		}
		return false;
	}

	protected abstract Left extractLeftProperty(ClothingData left);
	protected abstract Right extractRightProperty(ClothingData right);
}
