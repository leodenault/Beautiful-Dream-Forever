using System;

class Protagonist {
	private int balance;
	public int Balance {
		get { return balance; }
	}

	private Outfit outfit;

	public Protagonist() {
		balance = 0;
		outfit = new Outfit();
	}

	public bool modifyBalance(int difference) {
		if (difference + balance < 0) {
			return false;
		}

		balance += difference;
		return true;
	}
}
