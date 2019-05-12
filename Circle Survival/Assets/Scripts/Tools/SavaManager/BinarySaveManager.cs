using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CircleSurvival
{
    public class BinarySaveManager : ISaveManager
    {
        private readonly string filePath;

        public BinarySaveManager(string path)
        {
            filePath = path;  //Application.persistentDataPath + "/saveData.dat";
        }

        public void Save(SaveData data)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(filePath);
            bf.Serialize(file, data);
            file.Close();
        }

        public SaveData Load()
        {
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(filePath))
            {
                FileStream file = File.Open(filePath, FileMode.Open);
                SaveData data = bf.Deserialize(file) as SaveData;
                file.Close();
                return data;
            }
            else
            {
                SaveData data = new SaveData();
                Save(data);
                return data;
            }
        }
    }
}
