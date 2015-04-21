using System;
using NUnit.Framework;

[TestFixture]
public class SynergyTest<Left, Right> {

/*	private ISynergy synergy;

	private ClothingData createData(Left left) {
		ClothingData data = new ClothingData();
		assignLeftValues(data);
		return data;
	}

	private ClothingData createData(Right right) {
		ClothingData data = new ClothingData();
		assignRightValues(data);
		return data;
	}

	private Synergy<Left, Right>.Compatibility[] createCompatibilities(Right[] rights) {
		Synergy<Left, Right>.Compatibility[] compatibilities = new Synergy<Left, Right>.Compatibility[rights.Length];
		for (int i = 0; i < rights.Length; i++) {
			compatibilities[i] = new Synergy<Left, Right>.Compatibility();
			compatibilities[i].Synergy = rights[i];
		}
		return compatibilities;
	}

	private Synergy<Left, Right>.Compatibility[] createCompatibilities(Right[] rights, int[] points) {
		if (rights.Length != points.Length) {
			throw new InvalidOperationException("Synergies and points must have the same length");
		}

		Synergy<Left, Right>.Compatibility[] compatibilities = new Synergy<Left, Right>.Compatibility[rights.Length];
		for (int i = 0; i < rights.Length; i++) {
			compatibilities[i] = new Synergy<Left, Right>.Compatibility();
			compatibilities[i].Synergy = rights[i];
			compatibilities[i].Points = points[i];
		}
		return compatibilities;
	}

	protected abstract void assignLeftValues(ClothingData data);
	protected abstract void assignRightValues(ClothingData data);

	[Test]
	public void IsSynergeticReturnsFalseIfDataIsNull() {
		ClothingData left = createLeft(ClothingData.ClothingStyle.ATHLETIC);
		ClothingData right = createRight(ClothingData.ClothingColour.GREEN2);
		Assert.IsFalse(synergy.IsSynergetic(null, right));
		Assert.IsFalse(synergy.IsSynergetic(left, null));
		Assert.IsFalse(synergy.IsSynergetic(null, null));
	}

	[Test]
	public void IsSynergeticReturnsTrueIfStylesAreCompatible() {
		ClothingData.ClothingStyle style = ClothingData.ClothingStyle.ATHLETIC;
		synergy.Style = style;
		ClothingData.ClothingStyle[] compatibilities = {
			ClothingData.ClothingStyle.ATHLETIC,
			ClothingData.ClothingStyle.COSPLAY,
			ClothingData.ClothingStyle.FORMAL
		};
		synergy.Compatibilities = createCompatibilities(compatibilities);
		ClothingData left = createLeft(style);

		Assert.IsTrue(synergy.IsSynergetic(left, createRight(compatibilities[1])));
		Assert.IsTrue(synergy.IsSynergetic(left, createRight(compatibilities[0])));
		Assert.IsTrue(synergy.IsSynergetic(left, createRight(compatibilities[2])));
	}

	[Test]
	public void IsSynergeticReturnsFalseIfStylesAreNotCompatible() {
		ClothingData.ClothingStyle style = ClothingData.ClothingStyle.HARDCORE;
		synergy.Style = style;
		ClothingData.ClothingStyle[] compatibilities = {
			ClothingData.ClothingStyle.IDEALIST,
			ClothingData.ClothingStyle.HIPSTER
		};
		synergy.Compatibilities = createCompatibilities(compatibilities);
		ClothingData left = createData(style);

		Assert.IsFalse(synergy.IsSynergetic(left, createRight(ClothingData.ClothingStyle.COSPLAY)));
		Assert.IsFalse(synergy.IsSynergetic(left, createRight(ClothingData.ClothingStyle.ATHLETIC)));
		Assert.IsFalse(synergy.IsSynergetic(left, createRight(ClothingData.ClothingStyle.FORMAL)));
	}

	[Test]
	public void IsSynergeticReturnsFalseIfLeftStyleDoesntMatch() {
		synergy.Style = ClothingData.ClothingStyle.UNIFORM;
		ClothingData.ClothingStyle[] compatibilities = {
			ClothingData.ClothingStyle.IDEALIST,
			ClothingData.ClothingStyle.HIPSTER,
			ClothingData.ClothingStyle.UNIFORM,
			ClothingData.ClothingStyle.COSPLAY
		};
		synergy.Compatibilities = createCompatibilities(compatibilities);
		ClothingData left = createData(ClothingData.ClothingStyle.FORMAL);

		Assert.IsFalse(synergy.IsSynergetic(left, createRight(compatibilities[0])));
		Assert.IsFalse(synergy.IsSynergetic(left, createRight(compatibilities[1])));
		Assert.IsFalse(synergy.IsSynergetic(left, createRight(compatibilities[2])));
		Assert.IsFalse(synergy.IsSynergetic(left, createRight(compatibilities[3])));
	}

	[Test]
	public void GetPointsReturnsZeroIfStylesAreNotCompatible() {
		ClothingData.ClothingStyle style = ClothingData.ClothingStyle.HARDCORE;
		synergy.Style = style;
		ClothingData.ClothingStyle[] compatibilities = {
			ClothingData.ClothingStyle.IDEALIST,
			ClothingData.ClothingStyle.HIPSTER,
			ClothingData.ClothingStyle.PREPPY
		};
		synergy.Compatibilities = createCompatibilities(compatibilities);
		ClothingData left = createData(style);

		Assert.AreEqual(0, synergy.GetPoints(left, createRight(ClothingData.ClothingStyle.COSPLAY)));
		Assert.AreEqual(0, synergy.GetPoints(left, createRight(ClothingData.ClothingStyle.ATHLETIC)));
		Assert.AreEqual(0, synergy.GetPoints(left, createRight(ClothingData.ClothingStyle.FORMAL)));
		Assert.AreEqual(0, synergy.GetPoints(left, createRight(ClothingData.ClothingStyle.HARDCORE)));
	}

	[Test]
	public void GetPointsReturnsZeroIfLeftStyleDoesntMatch() {
		synergy.Style = ClothingData.ClothingStyle.HARDCORE;
		ClothingData.ClothingStyle[] compatibilities = {
			ClothingData.ClothingStyle.IDEALIST,
			ClothingData.ClothingStyle.HIPSTER,
			ClothingData.ClothingStyle.UNIFORM,
			ClothingData.ClothingStyle.COSPLAY
		};
		synergy.Compatibilities = createCompatibilities(compatibilities);
		ClothingData left = createData(ClothingData.ClothingStyle.IDEALIST);

		Assert.AreEqual(0, synergy.GetPoints(left, createData(compatibilities[0])));
		Assert.AreEqual(0, synergy.GetPoints(left, createData(compatibilities[1])));
		Assert.AreEqual(0, synergy.GetPoints(left, createData(compatibilities[2])));
		Assert.AreEqual(0, synergy.GetPoints(left, createData(compatibilities[3])));
	}

	[Test]
	public void GetPointsReturnsPointsForCompatibilityIfCompatible() {
		ClothingData.ClothingStyle style = ClothingData.ClothingStyle.ATHLETIC;
		synergy.Style = style;
		ClothingData.ClothingStyle[] compatibilities = {
			ClothingData.ClothingStyle.ATHLETIC,
			ClothingData.ClothingStyle.COSPLAY,
			ClothingData.ClothingStyle.FORMAL
		};
		int[] points = { 3, 2, 3 };
		synergy.Compatibilities = createCompatibilities(compatibilities, points);
		ClothingData left = createData(style);

		Assert.AreEqual(2, synergy.GetPoints(left, createData(compatibilities[1])));
		Assert.AreEqual(3, synergy.GetPoints(left, createData(compatibilities[0])));
		Assert.AreEqual(3, synergy.GetPoints(left, createData(compatibilities[2])));
	}*/
}
