using System;
using System.Collections.Generic;

public class ShopperData {

	private class MoneyRange {
		public int Min;
		public int Max;

		public MoneyRange(int min, int max) {
			Min = min;
			Max = max;
		}
	}

	private static int BASE_SCORE = 750;
	private static int SCORE_JUMP = 250;
	private static IDictionary<int, MoneyRange> FLOOR_RANGES = new Dictionary<int, MoneyRange>() {
		{ 1, new MoneyRange(6, 10) },
		{ 2, new MoneyRange(12, 20)},
		{ 3, new MoneyRange(14, 30) },
		{ 4, new MoneyRange(27, 50) }
	};

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
		Random random = new Random();
		MoneyRange range = FLOOR_RANGES[floor];
		return random.Next(range.Min, range.Max);
	}
}
