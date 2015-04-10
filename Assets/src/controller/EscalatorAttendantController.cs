using System.Collections.Generic;

public class EscalatorAttendantController {
	private static EscalatorAttendantController INSTANCE;

	private IDictionary<string, bool> floorAnswers;

	private EscalatorAttendantController() {
		floorAnswers = new Dictionary<string, bool>();
	}

	public static EscalatorAttendantController GetInstance() {
		if (INSTANCE == null) {
			INSTANCE = new EscalatorAttendantController();
		}

		return INSTANCE;
	}

	public void AddAnswer(string upperFloor, bool answer) {
		floorAnswers.Add(upperFloor, answer);
	}

	public bool IsAnswered(string upperFloor) {
		return floorAnswers.ContainsKey(upperFloor);
	}
}
