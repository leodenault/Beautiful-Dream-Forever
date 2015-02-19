using UnityEngine;
using System.IO;
using System.Xml.Serialization;

public class ClothingManager {
	private string file;
	private ClothingData[] clothingData;

	public ClothingManager(string file) {
		this.file = file;
		load();
	}

	private void load() {
		XmlSerializer serializer = new XmlSerializer(typeof(ClothingData[]));
		TextAsset data = Resources.Load<TextAsset>(file);
		byte[] bytes = data.bytes;

		MemoryStream stream = new MemoryStream();
		stream.Write(bytes, 0, bytes.Length);
		stream.Seek(0, SeekOrigin.Begin);

		clothingData = serializer.Deserialize(stream) as ClothingData[];
		stream.Close();
	}

	public string[] GetClothingImages() {
		string[] imageLocations = new string[clothingData.Length];
		for (int i = 0; i < clothingData.Length; i++) {
			imageLocations[i] = clothingData[i].Path;
		}
		return imageLocations;
	}
}
