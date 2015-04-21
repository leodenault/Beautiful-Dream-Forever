using System.Collections.Generic;

public class SynergyManager : ISynergyManager {
	private static SynergyManager INSTANCE;

	private List<ISynergy> synergies;

	private SynergyManager() {
		IList<ISynergy> essenceSynergies = new List<ISynergy>(Util.LoadXmlFile<EssenceSynergy[]>(EssenceSynergy.FILE));
		IList<ISynergy> styleSynergies = new List<ISynergy>(Util.LoadXmlFile<StyleSynergy[]>(StyleSynergy.FILE));
		IList<ISynergy> colourSynergies = new List<ISynergy>(Util.LoadXmlFile<ColourSynergy[]>(ColourSynergy.FILE));
		synergies = new List<ISynergy>(essenceSynergies.Count + styleSynergies.Count + colourSynergies.Count);
		synergies.AddRange(essenceSynergies);
		synergies.AddRange(styleSynergies);
		synergies.AddRange(colourSynergies);
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
