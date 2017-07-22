using System;

public abstract class SynergyTest<Left, Right> {

	protected Synergy<Left, Right> synergy;

	public abstract void SetUp();

	protected Compatibility<Right>[] createCompatibilities(Right[] rights) {
		Compatibility<Right>[] compatibilities = new Compatibility<Right>[rights.Length];
		for (int i = 0; i < rights.Length; i++) {
			compatibilities[i] = new Compatibility<Right>();
			compatibilities[i].Synergy = rights[i];
		}
		return compatibilities;
	}

	protected Compatibility<Right>[] createCompatibilities(Right[] rights, int[] points) {
		if (rights.Length != points.Length) {
			throw new InvalidOperationException("Synergies and points must have the same length");
		}

		Compatibility<Right>[] compatibilities = new Compatibility<Right>[rights.Length];
		for (int i = 0; i < rights.Length; i++) {
			compatibilities[i] = new Compatibility<Right>();
			compatibilities[i].Synergy = rights[i];
			compatibilities[i].Points = points[i];
		}
		return compatibilities;
	}
}
