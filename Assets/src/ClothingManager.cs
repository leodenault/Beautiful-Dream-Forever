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
		FileStream input = new FileStream(file, FileMode.Open);
		clothingData = serializer.Deserialize(input) as ClothingData[];
		input.Close();
	}

    public string[] GetClothingImages() {
        string[] imageLocations = new string[clothingData.Length];
        for (int i = 0; i < clothingData.Length; i++) {
            imageLocations[i] = clothingData[i].Path;
        }
        return imageLocations;
    }
}
