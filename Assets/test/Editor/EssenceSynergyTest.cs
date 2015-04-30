using NUnit.Framework;

[TestFixture]
public class EssenceSynergyTest : SynergyTest<ClothingData.ClothingEssence, ClothingData.ClothingStyle> {

	[SetUp]
	public override void SetUp() {
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
		synergy.Tag = ClothingData.ClothingEssence.CLASSY;
		ClothingData.ClothingStyle[] compatibilities = {
			ClothingData.ClothingStyle.ATHLETIC,
			ClothingData.ClothingStyle.COSPLAY,
			ClothingData.ClothingStyle.FORMAL
		};
		synergy.Compatibilities = createCompatibilities(compatibilities);
		ClothingData left = createLeft(ClothingData.ClothingEssence.CLASSY);

		Assert.IsTrue(synergy.IsSynergetic(left, createRight(ClothingData.ClothingStyle.COSPLAY)));
		Assert.IsTrue(synergy.IsSynergetic(left, createRight(ClothingData.ClothingStyle.ATHLETIC)));
		Assert.IsTrue(synergy.IsSynergetic(left, createRight(ClothingData.ClothingStyle.FORMAL)));
	}

	[Test]
	public void IsSynergeticReturnsFalseIfEssenceDoesntMatch() {
		synergy.Tag  = ClothingData.ClothingEssence.COOL;
		ClothingData.ClothingStyle[] compatibilities = {
			ClothingData.ClothingStyle.IDEALIST,
			ClothingData.ClothingStyle.UNIFORM
		};
		synergy.Compatibilities = createCompatibilities(compatibilities);

		Assert.IsFalse(synergy.IsSynergetic(createLeft(ClothingData.ClothingEssence.CUTE), createRight(ClothingData.ClothingStyle.IDEALIST)));
		Assert.IsFalse(synergy.IsSynergetic(createLeft(ClothingData.ClothingEssence.CLASSY), createRight(ClothingData.ClothingStyle.UNIFORM)));
	}

	[Test]
	public void IsSynergeticReturnsFalseIfStyleDoesntMatch() {
		synergy.Tag  = ClothingData.ClothingEssence.CUTE;
		ClothingData.ClothingStyle[] compatibilities = {
			ClothingData.ClothingStyle.IDEALIST,
			ClothingData.ClothingStyle.UNIFORM,
			ClothingData.ClothingStyle.PREPPY,
			ClothingData.ClothingStyle.HIPSTER
		};
		synergy.Compatibilities = createCompatibilities(compatibilities);
		ClothingData left = createLeft(ClothingData.ClothingEssence.CUTE);

		Assert.IsFalse(synergy.IsSynergetic(left, createRight(ClothingData.ClothingStyle.ATHLETIC)));
		Assert.IsFalse(synergy.IsSynergetic(left, createRight(ClothingData.ClothingStyle.COSPLAY)));
		Assert.IsFalse(synergy.IsSynergetic(left, createRight(ClothingData.ClothingStyle.FORMAL)));
		Assert.IsFalse(synergy.IsSynergetic(left, createRight(ClothingData.ClothingStyle.HARDCORE)));
	}
}
