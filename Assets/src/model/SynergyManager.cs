using UnityEngine;
using System.Collections.Generic;

public class SynergyManager : ISynergyManager {
	private static SynergyManager INSTANCE;

	private List<ISynergy> synergies;

	private SynergyManager() {
		IList<ISynergy> essenceSynergies = new List<ISynergy>(Util.LoadXmlFile<EssenceSynergy[]>(EssenceSynergy.FileName()));
		IList<ISynergy> styleSynergies = new List<ISynergy>(Util.LoadXmlFile<StyleSynergy[]>(StyleSynergy.FileName()));
		synergies = new List<ISynergy>(essenceSynergies.Count + styleSynergies.Count);
		synergies.AddRange(essenceSynergies);
		synergies.AddRange(styleSynergies);
	}

	public static SynergyManager GetInstance() {
		if (INSTANCE == null) {
			INSTANCE = new SynergyManager();
		}

		return INSTANCE;
	}

	public ISynergy[] GetSynergies() {
		return synergies.ToArray();
	}
}
