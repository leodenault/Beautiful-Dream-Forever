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
	public void MultipleCallsToGetPointsGivesSameResult() {
		synergyStubs[0].GetPoints(Arg.Any<ClothingData>(), Arg.Any<ClothingData>()).Returns<int>(3);
		outfit.SetItem(Substitute.For<ClothingData>());
		int points = outfit.GetPoints();
		Assert.AreEqual(points, outfit.GetPoints());
	}
}
