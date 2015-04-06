using UnityEngine;
using System.Collections.Generic;

public class CheatSequence : IEnumerable<KeyCode> {
	private IList<KeyCode> sequence;
	private int index;

	public CheatSequence() {
		sequence = new List<KeyCode>();
		index = 0;
	}

	public bool Activate() {
		if (Input.GetKeyDown(sequence[index])) {
			if (index == sequence.Count - 1) {
				index = 0;
				return true;
			} else {
				index++;
			}
		} else {
			index = 0;
		}

		return false;
	}

	public void Add(KeyCode code) {
		sequence.Add(code);
	}

	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
		return sequence.GetEnumerator();
	}

	public IEnumerator<KeyCode> GetEnumerator() {
		return sequence.GetEnumerator();
	}
}
