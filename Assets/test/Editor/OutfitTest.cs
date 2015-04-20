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
		synergyStubs[0].IsSynergetic(wig, bottom).Returns<bool>(true);
		synergyStubs[1].IsSynergetic(top, bottom).Returns<bool>(true);
		synergyStubs[2].IsSynergetic(bottom, top).Returns<bool>(true);
		synergyStubs[0].GetPoints().Returns<int>(2);
		synergyStubs[1].GetPoints().Returns<int>(3);
		synergyStubs[2].GetPoints().Returns<int>(1);
		Assert.AreEqual(210, outfit.GetPoints());
	}

	[Test]
	public void MultipleCallsToGetPointsGivesSameResult() {
		synergyStubs[0].IsSynergetic(Arg.Any<ClothingData>(), Arg.Any<ClothingData>()).Returns<bool>(true);
		synergyStubs[1].IsSynergetic(Arg.Any<ClothingData>(), Arg.Any<ClothingData>()).Returns<bool>(false);
		synergyStubs[2].IsSynergetic(Arg.Any<ClothingData>(), Arg.Any<ClothingData>()).Returns<bool>(false);
		synergyStubs[0].GetPoints().Returns<int>(3);
		outfit.SetItem(Substitute.For<ClothingData>());
		int points = outfit.GetPoints();
		Assert.AreEqual(points, outfit.GetPoints());
	}
}
