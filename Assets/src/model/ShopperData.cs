using System;

public class ShopperData {

	private static int BASE_SCORE = 1000;
	private static int SCORE_JUMP = 400;

	private int floor;

	public ShopperData(int floor) {
		if (floor < 1) {
			throw new ArgumentException("Floor must be a positive non-zero integer");
		}

		this.floor = floor;
	}

	public int GenerateTargetScore() {
		return BASE_SCORE + ((floor - 1) * SCORE_JUMP);
	}

	public int GenerateMoneyDrop() {
		int square = floor * floor;
		int min = square - (int)Math.Floor(square / 8.0f);
		int gap = floor * 2;
		Random random = new Random();
		return random.Next(min, min + gap + 1);
	}
}
