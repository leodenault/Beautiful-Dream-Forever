using UnityEngine;
using System.IO;
using System.Xml.Serialization;

public class SynergyManager {
	private static SynergyManager INSTANCE;
	private static string FILE = "data/essenceSynergies";

	private EssenceSynergy[] essenceSynergies;

	private SynergyManager() {
		essenceSynergies = Util.LoadXmlFile<EssenceSynergy[]>(FILE);
	}

	public static SynergyManager GetInstance() {
		if (INSTANCE == null) {
			INSTANCE = new SynergyManager();
		}

		return INSTANCE;
	}

	public ISynergy[] GetSynergies() {
		return essenceSynergies;
	}
}
