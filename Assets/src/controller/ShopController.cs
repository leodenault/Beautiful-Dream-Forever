public class ShopController {

	private static ShopController INSTANCE;

	private ClothingData.ClothingStyle shopStyle;
	public ClothingData.ClothingStyle ShopStyle {
		get { return shopStyle; }
		set { shopStyle = value; }
	}

	private ShopController() {
		shopStyle = ClothingData.ClothingStyle.NONE;
	}

	public static ShopController GetInstance() {
		if (INSTANCE == null) {
			INSTANCE = new ShopController();
		}

		return INSTANCE;
	}
}
