using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class OutfitTest {

	private Outfit outfit;
	private ISynergyManager synergyManagerStub;
	private ISynergy[] synergyStubs;

	[SetUp]
	public void SetUp() {
		synergyManagerStub = Substitute.For<ISynergyManager>();
		synergyStubs = new ISynergy[3];
		for (int i = 0; i < synergyStubs.Length; i++) {
			synergyStubs[i] = Substitute.For<ISynergy>();
		}
		synergyManagerStub.GetSynergies().Returns<ISynergy[]>(synergyStubs);
		outfit = new Outfit(synergyManagerStub);
	}

	[Test]
	public void GetPointsShouldReturnZeroForEmptyOutfit() {
		Assert.AreEqual(0, outfit.GetPoints());
	}

	[Test]
	public void GetPointsShouldReturnCorrectNumberOfPointsWhenSlotsFilled() {
		ClothingData wig = Substitute.For<ClothingData>();
		ClothingData top = Substitute.For<ClothingData>();
		ClothingData bottom = Substitute.For<ClothingData>();
		wig.Slot = ClothingData.ClothingSlot.WIG;
		top.Slot = ClothingData.ClothingSlot.TOP;
		bottom.Slot = ClothingData.ClothingSlot.BOTTOM;
		outfit.SetItem(wig);
		outfit.SetItem(top);
		outfit.SetItem(bottom);
		Assert.AreEqual(70, outfit.GetPoints());
	}

	[Test]
	public void GetPointsShouldReturnCorrectNumberOfPointsWhenSlotsFilledWithSynergies() {
		ClothingData wig = Substitute.For<ClothingData>();
		ClothingData top = Substitute.For<ClothingData>();
		ClothingData bottom = Substitute.For<ClothingData>();
		wig.Slot = ClothingData.ClothingSlot.WIG;
		top.Slot = ClothingData.ClothingSlot.TOP;
		bottom.Slot = ClothingData.ClothingSlot.BOTTOM;
		outfit.SetItem(wig);
		outfit.SetItem(top);
		outfit.SetItem(bottom);
		synergyStubs[0].GetPoints(wig, bottom).Returns<int>(2);
		synergyStubs[1].GetPoints(top, bottom).Returns<int>(3);
		synergyStubs[2].GetPoints(bottom, top).Returns<int>(1);
		Assert.AreEqual(210, outfit.GetPoints());
	}

	[Test]
	public void GetPointsShouldReturnMultipliedPointsForFullSetAndSingleEssenceForTopBottom() {
		ClothingData wig = Substitute.For<ClothingData>();
		ClothingData top = Substitute.For<ClothingData>();
		ClothingData bottom = Substitute.For<ClothingData>();
		ClothingData shoes = Substitute.For<ClothingData>();
		ClothingData accessory = Substitute.For<ClothingData>();
		wig.Slot = ClothingData.ClothingSlot.WIG;
		top.Slot = ClothingData.ClothingSlot.TOP;
		bottom.Slot = ClothingData.ClothingSlot.BOTTOM;
		shoes.Slot = ClothingData.ClothingSlot.SHOES;
		accessory.Slot = ClothingData.ClothingSlot.ACCESSORY;
		outfit.SetItem(wig);
		outfit.SetItem(top);
		outfit.SetItem(bottom);
		outfit.SetItem(shoes);
		outfit.SetItem(accessory);
		synergyStubs[1].GetPoints(top, bottom).Returns<int>(2);
		Assert.AreEqual(255, outfit.GetPoints());
	}

	[Test]
	public void GetPointsShouldReturnMultipliedPointsForFullSetAndSingleEssenceForDresses() {
		ClothingData wig = Substitute.For<ClothingData>();
		ClothingData dress = Substitute.For<ClothingData>();
		ClothingData shoes = Substitute.For<ClothingData>();
		ClothingData accessory = Substitute.For<ClothingData>();
		wig.Slot = ClothingData.ClothingSlot.WIG;
		dress.Slot = ClothingData.ClothingSlot.DRESS;
		shoes.Slot = ClothingData.ClothingSlot.SHOES;
		accessory.Slot = ClothingData.ClothingSlot.ACCESSORY;
		outfit.SetItem(wig);
		outfit.SetItem(dress);
		outfit.SetItem(shoes);
		outfit.SetItem(accessory);
		synergyStubs[1].GetPoints(accessory, wig).Returns<int>(3);
		Assert.AreEqual(207, outfit.GetPoints());
	}

	[Test]
	public void MultipleCallsToGetPointsGivesSameResult() {
		synergyStubs[0].GetPoints(Arg.Any<ClothingData>(), Arg.Any<ClothingData>()).Returns<int>(3);
		outfit.SetItem(Substitute.For<ClothingData>());
		int points = outfit.GetPoints();
		Assert.AreEqual(points, outfit.GetPoints());
	}
}
