using System;

class Protagonist {
	private static Protagonist INSTANCE;
	private static int MAX_MONEY = 99;

	private int balance;
	public int Balance {
		get { return balance; }
	}

	private Inventory inventory;
	public Inventory Inventory {
		get { return inventory; }
	}

	private Outfit outfit;

	private Protagonist() {
		balance = 0;
		outfit = new Outfit();
		inventory = new Inventory();
	}

	public static Protagonist GetInstance() {
		if (INSTANCE == null) {
			INSTANCE = new Protagonist();
		}

		return INSTANCE;
	}

	public bool modifyBalance(int difference) {
		if (difference + balance < 0) {
			return false;
		}

		balance = Math.Min(balance + difference, MAX_MONEY);
		return true;
	}

	public bool CanPurchase(int price) {
		return (price >= 0) && balance - price >= 0;
	}
}
