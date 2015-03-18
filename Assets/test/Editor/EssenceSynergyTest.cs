using NUnit.Framework;

[TestFixture]
public class EssenceSynergyTest {

	private EssenceSynergy synergy;

	[SetUp]
	public void SetUp() {
		synergy = new EssenceSynergy();
	}

	private ClothingData createClothingData(ClothingData.ClothingEssence essence, ClothingData.ClothingStyle style) {
		ClothingData data = new ClothingData();
		data.Essence = essence;
		data.Style = style;
		return data;
	}

	[Test]
	public void IsSynergeticReturnsFalseIfDataIsNull() {
		Assert.IsFalse(synergy.IsSynergetic(null));
	}

	[Test]
	public void IsSynergeticReturnsTrueIfEssenceAndStyleMatch() {
		synergy.Essence = ClothingData.ClothingEssence.CLASSY;
		synergy.Styles = new ClothingData.ClothingStyle[3];
		synergy.Styles[0] = ClothingData.ClothingStyle.ATHLETIC;
		synergy.Styles[1] = ClothingData.ClothingStyle.COSPLAY;
		synergy.Styles[2] = ClothingData.ClothingStyle.FORMAL;

		Assert.IsTrue(synergy.IsSynergetic(createClothingData(ClothingData.ClothingEssence.CLASSY, ClothingData.ClothingStyle.COSPLAY)));
		Assert.IsTrue(synergy.IsSynergetic(createClothingData(ClothingData.ClothingEssence.CLASSY, ClothingData.ClothingStyle.FORMAL)));
		Assert.IsTrue(synergy.IsSynergetic(createClothingData(ClothingData.ClothingEssence.CLASSY, ClothingData.ClothingStyle.ATHLETIC)));
	}

	[Test]
	public void IsSynergeticReturnsFalseIfEssenceDoesntMatch() {
		synergy.Essence = ClothingData.ClothingEssence.COOL;
		synergy.Styles = new ClothingData.ClothingStyle[2];
		synergy.Styles[0] = ClothingData.ClothingStyle.IDEALIST;
		synergy.Styles[1] = ClothingData.ClothingStyle.UNIFORM;

		Assert.IsFalse(synergy.IsSynergetic(createClothingData(ClothingData.ClothingEssence.CUTE, ClothingData.ClothingStyle.IDEALIST)));
		Assert.IsFalse(synergy.IsSynergetic(createClothingData(ClothingData.ClothingEssence.CLASSY, ClothingData.ClothingStyle.UNIFORM)));
	}

	[Test]
	public void IsSynergeticReturnsFalseIfStyleDoesntMatch() {
		synergy.Essence = ClothingData.ClothingEssence.CUTE;
		synergy.Styles = new ClothingData.ClothingStyle[4];
		synergy.Styles[0] = ClothingData.ClothingStyle.IDEALIST;
		synergy.Styles[1] = ClothingData.ClothingStyle.UNIFORM;
		synergy.Styles[2] = ClothingData.ClothingStyle.PREPPY;
		synergy.Styles[3] = ClothingData.ClothingStyle.HIPSTER;

		Assert.IsFalse(synergy.IsSynergetic(createClothingData(ClothingData.ClothingEssence.CUTE, ClothingData.ClothingStyle.ATHLETIC)));
		Assert.IsFalse(synergy.IsSynergetic(createClothingData(ClothingData.ClothingEssence.CUTE, ClothingData.ClothingStyle.COSPLAY)));
		Assert.IsFalse(synergy.IsSynergetic(createClothingData(ClothingData.ClothingEssence.CUTE, ClothingData.ClothingStyle.FORMAL)));
		Assert.IsFalse(synergy.IsSynergetic(createClothingData(ClothingData.ClothingEssence.CUTE, ClothingData.ClothingStyle.HARDCORE)));
	}
}
