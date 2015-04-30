using System;
using NUnit.Framework;

[TestFixture]
public class StyleSynergyTest : SynergyTest<ClothingData.ClothingStyle, ClothingData.ClothingStyle> {

	[SetUp]
	public override void SetUp() {
		synergy = new StyleSynergy();
	}

	private ClothingData createData(ClothingData.ClothingStyle style) {
		ClothingData data = new ClothingData();
		data.Style = style;
		return data;
	}

	[Test]
	public void IsSynergeticReturnsFalseIfDataIsNull() {
		ClothingData left = createData(ClothingData.ClothingStyle.ATHLETIC);
		ClothingData right = createData(ClothingData.ClothingStyle.COSPLAY);
		Assert.IsFalse(synergy.IsSynergetic(null, right));
		Assert.IsFalse(synergy.IsSynergetic(left, null));
		Assert.IsFalse(synergy.IsSynergetic(null, null));
	}

	[Test]
	public void IsSynergeticReturnsTrueIfStylesAreCompatible() {
		ClothingData.ClothingStyle style = ClothingData.ClothingStyle.ATHLETIC;
		synergy.Tag = style;
		ClothingData.ClothingStyle[] compatibilities = {
			ClothingData.ClothingStyle.ATHLETIC,
			ClothingData.ClothingStyle.COSPLAY,
			ClothingData.ClothingStyle.FORMAL
		};
		synergy.Compatibilities = createCompatibilities(compatibilities);
		ClothingData left = createData(style);

		Assert.IsTrue(synergy.IsSynergetic(left, createData(compatibilities[1])));
		Assert.IsTrue(synergy.IsSynergetic(left, createData(compatibilities[0])));
		Assert.IsTrue(synergy.IsSynergetic(left, createData(compatibilities[2])));
	}

	[Test]
	public void IsSynergeticReturnsFalseIfStylesAreNotCompatible() {
		ClothingData.ClothingStyle style = ClothingData.ClothingStyle.HARDCORE;
		synergy.Tag = style;
		ClothingData.ClothingStyle[] compatibilities = {
			ClothingData.ClothingStyle.IDEALIST,
			ClothingData.ClothingStyle.HIPSTER
		};
		synergy.Compatibilities = createCompatibilities(compatibilities);
		ClothingData left = createData(style);

		Assert.IsFalse(synergy.IsSynergetic(left, createData(ClothingData.ClothingStyle.COSPLAY)));
		Assert.IsFalse(synergy.IsSynergetic(left, createData(ClothingData.ClothingStyle.ATHLETIC)));
		Assert.IsFalse(synergy.IsSynergetic(left, createData(ClothingData.ClothingStyle.FORMAL)));
	}

	[Test]
	public void IsSynergeticReturnsFalseIfLeftStyleDoesntMatch() {
		synergy.Tag = ClothingData.ClothingStyle.UNIFORM;
		ClothingData.ClothingStyle[] compatibilities = {
			ClothingData.ClothingStyle.IDEALIST,
			ClothingData.ClothingStyle.HIPSTER,
			ClothingData.ClothingStyle.UNIFORM,
			ClothingData.ClothingStyle.COSPLAY
		};
		synergy.Compatibilities = createCompatibilities(compatibilities);
		ClothingData left = createData(ClothingData.ClothingStyle.FORMAL);

		Assert.IsFalse(synergy.IsSynergetic(left, createData(compatibilities[0])));
		Assert.IsFalse(synergy.IsSynergetic(left, createData(compatibilities[1])));
		Assert.IsFalse(synergy.IsSynergetic(left, createData(compatibilities[2])));
		Assert.IsFalse(synergy.IsSynergetic(left, createData(compatibilities[3])));
	}

	[Test]
	public void GetPointsReturnsZeroIfStylesAreNotCompatible() {
		ClothingData.ClothingStyle style = ClothingData.ClothingStyle.HARDCORE;
		synergy.Tag = style;
		ClothingData.ClothingStyle[] compatibilities = {
			ClothingData.ClothingStyle.IDEALIST,
			ClothingData.ClothingStyle.HIPSTER,
			ClothingData.ClothingStyle.PREPPY
		};
		synergy.Compatibilities = createCompatibilities(compatibilities);
		ClothingData left = createData(style);

		Assert.AreEqual(0, synergy.GetPoints(left, createData(ClothingData.ClothingStyle.COSPLAY)));
		Assert.AreEqual(0, synergy.GetPoints(left, createData(ClothingData.ClothingStyle.ATHLETIC)));
		Assert.AreEqual(0, synergy.GetPoints(left, createData(ClothingData.ClothingStyle.FORMAL)));
		Assert.AreEqual(0, synergy.GetPoints(left, createData(ClothingData.ClothingStyle.HARDCORE)));
	}

	[Test]
	public void GetPointsReturnsZeroIfLeftStyleDoesntMatch() {
		synergy.Tag = ClothingData.ClothingStyle.HARDCORE;
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
		synergy.Tag = style;
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
	}
}
