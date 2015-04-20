using NUnit.Framework;

[TestFixture]
public class EssenceSynergyTest {

	private EssenceSynergy synergy;

	[SetUp]
	public void SetUp() {
		synergy = new EssenceSynergy();
	}

	private ClothingData createLeft(ClothingData.ClothingEssence essence) {
		ClothingData data = new ClothingData();
		data.Essence = essence;
		return data;
	}

	private ClothingData createRight(ClothingData.ClothingStyle style) {
		ClothingData data = new ClothingData();
		data.Style = style;
		return data;
	}

	[Test]
	public void IsSynergeticReturnsFalseIfDataIsNull() {
		ClothingData left = createLeft(ClothingData.ClothingEssence.CLASSY);
		ClothingData right = createRight(ClothingData.ClothingStyle.COSPLAY);
		Assert.IsFalse(synergy.IsSynergetic(null, right));
		Assert.IsFalse(synergy.IsSynergetic(left, null));
		Assert.IsFalse(synergy.IsSynergetic(null, null));
	}

	[Test]
	public void IsSynergeticReturnsTrueIfEssenceAndStyleMatch() {
		synergy.Essence = ClothingData.ClothingEssence.CLASSY;
		synergy.Styles = new ClothingData.ClothingStyle[3];
		synergy.Styles[0] = ClothingData.ClothingStyle.ATHLETIC;
		synergy.Styles[1] = ClothingData.ClothingStyle.COSPLAY;
		synergy.Styles[2] = ClothingData.ClothingStyle.FORMAL;
		ClothingData left = createLeft(ClothingData.ClothingEssence.CLASSY);

		Assert.IsTrue(synergy.IsSynergetic(left, createRight(ClothingData.ClothingStyle.COSPLAY)));
		Assert.IsTrue(synergy.IsSynergetic(left, createRight(ClothingData.ClothingStyle.ATHLETIC)));
		Assert.IsTrue(synergy.IsSynergetic(left, createRight(ClothingData.ClothingStyle.FORMAL)));
	}

	[Test]
	public void IsSynergeticReturnsFalseIfEssenceDoesntMatch() {
		synergy.Essence = ClothingData.ClothingEssence.COOL;
		synergy.Styles = new ClothingData.ClothingStyle[2];
		synergy.Styles[0] = ClothingData.ClothingStyle.IDEALIST;
		synergy.Styles[1] = ClothingData.ClothingStyle.UNIFORM;

		Assert.IsFalse(synergy.IsSynergetic(createLeft(ClothingData.ClothingEssence.CUTE), createRight(ClothingData.ClothingStyle.IDEALIST)));
		Assert.IsFalse(synergy.IsSynergetic(createLeft(ClothingData.ClothingEssence.CLASSY), createRight(ClothingData.ClothingStyle.UNIFORM)));
	}

	[Test]
	public void IsSynergeticReturnsFalseIfStyleDoesntMatch() {
		synergy.Essence = ClothingData.ClothingEssence.CUTE;
		synergy.Styles = new ClothingData.ClothingStyle[4];
		synergy.Styles[0] = ClothingData.ClothingStyle.IDEALIST;
		synergy.Styles[1] = ClothingData.ClothingStyle.UNIFORM;
		synergy.Styles[2] = ClothingData.ClothingStyle.PREPPY;
		synergy.Styles[3] = ClothingData.ClothingStyle.HIPSTER;
		ClothingData left = createLeft(ClothingData.ClothingEssence.CUTE);

		Assert.IsFalse(synergy.IsSynergetic(left, createRight(ClothingData.ClothingStyle.ATHLETIC)));
		Assert.IsFalse(synergy.IsSynergetic(left, createRight(ClothingData.ClothingStyle.COSPLAY)));
		Assert.IsFalse(synergy.IsSynergetic(left, createRight(ClothingData.ClothingStyle.FORMAL)));
		Assert.IsFalse(synergy.IsSynergetic(left, createRight(ClothingData.ClothingStyle.HARDCORE)));
	}
}
