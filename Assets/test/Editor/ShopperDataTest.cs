using NUnit.Framework;

[TestFixture]
public class ShopperDataTest {

	private ShopperData shopper;

	[Test]
	public void GenerateTargetScoreShouldReturnProperScoreForFloor() {
		ShopperData[] shoppers = new ShopperData[4];
		for (int i = 0; i < shoppers.Length; i++) {
			shopper = new ShopperData(i + 1);
			Assert.AreEqual(1000 + (i * 400), shopper.GenerateTargetScore());
		}
	}
}
