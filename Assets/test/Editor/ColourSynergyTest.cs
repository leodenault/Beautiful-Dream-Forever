using NUnit.Framework;

[TestFixture]
public class ColourSynergyTest : SynergyTest<ClothingData.ClothingStyle, ClothingData.ClothingColour> {

	[SetUp]
	public override void SetUp() {
		synergy = new ColourSynergy();
	}

	private ClothingData createData(ClothingData.ClothingStyle left) {
		ClothingData data = new ClothingData();
		data.Style = left;
		return data;
	}

	private ClothingData createData(ClothingData.ClothingColour right) {
		ClothingData data = new ClothingData();
		data.Colour = right;
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
	public void IsSynergeticReturnsTrueIfStyleAndColourAreCompatible() {
		ClothingData.ClothingStyle style = ClothingData.ClothingStyle.ATHLETIC;
		synergy.Tag = style;
		ClothingData.ClothingColour[] compatibilities = {
			ClothingData.ClothingColour.BLUE,
			ClothingData.ClothingColour.INDIGO,
			ClothingData.ClothingColour.PINK
		};
		synergy.Compatibilities = createCompatibilities(compatibilities);
		ClothingData left = createData(style);

		Assert.IsTrue(synergy.IsSynergetic(left, createData(compatibilities[1])));
		Assert.IsTrue(synergy.IsSynergetic(left, createData(compatibilities[0])));
		Assert.IsTrue(synergy.IsSynergetic(left, createData(compatibilities[2])));
	}

	[Test]
	public void IsSynergeticReturnsFalseIfStyleAndColourAreNotCompatible() {
		ClothingData.ClothingStyle style = ClothingData.ClothingStyle.HARDCORE;
		synergy.Tag = style;
		ClothingData.ClothingColour[] compatibilities = {
			ClothingData.ClothingColour.ORANGE2,
			ClothingData.ClothingColour.RED
		};
		synergy.Compatibilities = createCompatibilities(compatibilities);
		ClothingData left = createData(style);

		Assert.IsFalse(synergy.IsSynergetic(left, createData(ClothingData.ClothingColour.YELLOW)));
		Assert.IsFalse(synergy.IsSynergetic(left, createData(ClothingData.ClothingColour.TEAL)));
		Assert.IsFalse(synergy.IsSynergetic(left, createData(ClothingData.ClothingColour.GREEN1)));
	}

	[Test]
	public void IsSynergeticReturnsFalseIfStyleDoesntMatch() {
		synergy.Tag = ClothingData.ClothingStyle.UNIFORM;
		ClothingData.ClothingColour[] compatibilities = {
			ClothingData.ClothingColour.GREEN1,
			ClothingData.ClothingColour.GREEN2,
			ClothingData.ClothingColour.INDIGO,
			ClothingData.ClothingColour.ORANGE1
		};
		synergy.Compatibilities = createCompatibilities(compatibilities);
		ClothingData left = createData(ClothingData.ClothingStyle.FORMAL);

		Assert.IsFalse(synergy.IsSynergetic(left, createData(compatibilities[0])));
		Assert.IsFalse(synergy.IsSynergetic(left, createData(compatibilities[1])));
		Assert.IsFalse(synergy.IsSynergetic(left, createData(compatibilities[2])));
		Assert.IsFalse(synergy.IsSynergetic(left, createData(compatibilities[3])));
	}

	[Test]
	public void IsSynergeticReturnsFalseIfColourIsNone() {
		ClothingData.ClothingStyle style = ClothingData.ClothingStyle.HARDCORE;
		synergy.Tag = style;
		ClothingData.ClothingColour[] compatibilities = {
			ClothingData.ClothingColour.ORANGE2
		};
		synergy.Compatibilities = createCompatibilities(compatibilities);
		ClothingData left = createData(style);

		Assert.IsFalse(synergy.IsSynergetic(left, createData(ClothingData.ClothingColour.NONE)));
	}

	[Test]
	public void GetPointsReturnsZeroIfStyleAndColourAreNotCompatible() {
		ClothingData.ClothingStyle style = ClothingData.ClothingStyle.HARDCORE;
		synergy.Tag = style;
		ClothingData.ClothingColour[] compatibilities = {
			ClothingData.ClothingColour.ORANGE1,
			ClothingData.ClothingColour.ORANGE2,
			ClothingData.ClothingColour.PINK
		};
		synergy.Compatibilities = createCompatibilities(compatibilities);
		ClothingData left = createData(style);

		Assert.AreEqual(0, synergy.GetPoints(left, createData(ClothingData.ClothingColour.RED)));
		Assert.AreEqual(0, synergy.GetPoints(left, createData(ClothingData.ClothingColour.TEAL)));
		Assert.AreEqual(0, synergy.GetPoints(left, createData(ClothingData.ClothingColour.YELLOW)));
		Assert.AreEqual(0, synergy.GetPoints(left, createData(ClothingData.ClothingColour.BLUE)));
	}

	[Test]
	public void GetPointsReturnsZeroIfStyleDoesntMatch() {
		synergy.Tag = ClothingData.ClothingStyle.HARDCORE;
		ClothingData.ClothingColour[] compatibilities = {
			ClothingData.ClothingColour.GREEN1,
			ClothingData.ClothingColour.GREEN2,
			ClothingData.ClothingColour.INDIGO,
			ClothingData.ClothingColour.ORANGE1
		};
		synergy.Compatibilities = createCompatibilities(compatibilities);
		ClothingData left = createData(ClothingData.ClothingStyle.IDEALIST);

		Assert.AreEqual(0, synergy.GetPoints(left, createData(compatibilities[0])));
		Assert.AreEqual(0, synergy.GetPoints(left, createData(compatibilities[1])));
		Assert.AreEqual(0, synergy.GetPoints(left, createData(compatibilities[2])));
		Assert.AreEqual(0, synergy.GetPoints(left, createData(compatibilities[3])));
	}

	[Test]
	public void GetPointsReturnsZeroIfColourIsNone() {
		ClothingData.ClothingStyle style = ClothingData.ClothingStyle.HARDCORE;
		synergy.Tag = style;
		ClothingData.ClothingColour[] compatibilities = {
			ClothingData.ClothingColour.PINK
		};
		synergy.Compatibilities = createCompatibilities(compatibilities);
		ClothingData left = createData(style);

		Assert.AreEqual(0, synergy.GetPoints(left, createData(ClothingData.ClothingColour.NONE)));
	}

	[Test]
	public void GetPointsReturnsPointsForCompatibilityIfCompatible() {
		ClothingData.ClothingStyle style = ClothingData.ClothingStyle.ATHLETIC;
		synergy.Tag = style;
		ClothingData.ClothingColour[] compatibilities = {
			ClothingData.ClothingColour.PURPLE,
			ClothingData.ClothingColour.RED,
			ClothingData.ClothingColour.TEAL
		};
		int[] points = { 3, 2, 3 };
		synergy.Compatibilities = createCompatibilities(compatibilities, points);
		ClothingData left = createData(style);

		Assert.AreEqual(2, synergy.GetPoints(left, createData(compatibilities[1])));
		Assert.AreEqual(3, synergy.GetPoints(left, createData(compatibilities[0])));
		Assert.AreEqual(3, synergy.GetPoints(left, createData(compatibilities[2])));
	}
}
