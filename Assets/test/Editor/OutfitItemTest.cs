using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class OutfitItemTest {
	private OutfitItem item;

	// Stubs
	private ISynergyManager managerStub;
	private ISynergy[] synergyStub;

	[SetUp]
	public void SetUp() {
		managerStub = Substitute.For<ISynergyManager>();
		synergyStub = new ISynergy[3];
		synergyStub[0] = Substitute.For<ISynergy>();
		synergyStub[1] = Substitute.For<ISynergy>();
		synergyStub[2] = Substitute.For<ISynergy>();

		managerStub.GetSynergies().Returns<ISynergy[]>(synergyStub);
		item = new OutfitItem(null, managerStub);
	}

	[Test]
	public void PointsShouldReturnZeroIfSlotNotFilled() {
		Assert.AreEqual(0, item.Points);
	}

	[Test]
	public void PointsShouldReturnTopMultiplierIfNoSynergies() {
		ClothingData itemStub = Substitute.For<ClothingData>();
		itemStub.Slot= ClothingData.ClothingSlot.TOP;
		synergyStub[0].IsSynergetic(Arg.Any<ClothingData>()).Returns<bool>(false);
		synergyStub[1].IsSynergetic(Arg.Any<ClothingData>()).Returns<bool>(false);
		synergyStub[2].IsSynergetic(Arg.Any<ClothingData>()).Returns<bool>(false);
		item.Item = itemStub;
		Assert.AreEqual(30, item.Points);
	}

	[Test]
	public void PointsShouldReturnSynergyWithMultiplierIfFilledAndSynergetic() {
		ClothingData itemStub = Substitute.For<ClothingData>();
		itemStub.Slot = ClothingData.ClothingSlot.WIG;
		synergyStub[0].IsSynergetic(Arg.Any<ClothingData>()).Returns<bool>(true);
		synergyStub[0].GetPoints().Returns<int>(3);
		synergyStub[1].IsSynergetic(Arg.Any<ClothingData>()).Returns<bool>(false);
		synergyStub[2].IsSynergetic(Arg.Any<ClothingData>()).Returns<bool>(true);
		synergyStub[2].GetPoints().Returns<int>(2);
		item.Item = itemStub;
		Assert.AreEqual(50, item.Points);
	}
}
