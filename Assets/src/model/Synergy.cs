using System.Collections.Generic;

public abstract class Synergy<Left, Right> : ISynergy {
	public class Compatibility {
		public Right Synergy;
		public int Points;
	}

	protected IDictionary<Right, int> compatibilityPoints;

	public Left Tag;

	protected Compatibility[] compatibilities;
	public Compatibility[] Compatibilities {
		get { return compatibilities; }
		set {
			compatibilities = value;
			compatibilityPoints = new Dictionary<Right, int>();
			foreach (Compatibility compatibility in compatibilities) {
				compatibilityPoints.Add(compatibility.Synergy, compatibility.Points);
			}
		}
	}

	public int GetPoints(ClothingData left, ClothingData right) {
		return IsSynergetic(left, right) ? compatibilityPoints[extractRightProperty(right)] : 0;
	}

	public bool IsSynergetic(ClothingData left, ClothingData right) {
		if (left != null && right != null && extractLeftProperty(left).Equals(Tag)) {
			foreach (Compatibility compatibility in compatibilities) {
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
