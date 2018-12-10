using System.IO;
using Newtonsoft.Json;

public class JSONDataFileService<T> : IDataFileService<T>
where T : class, new()
{
    string _dataFile;
    public JSONDataFileService(string dataFile)
    {
        _dataFile = dataFile;
    }
    public T GetDB()
    {
        T result = null;
        string dbFileName = _dataFile;
        if(!File.Exists(dbFileName)){
            T db = new T();
            SaveDB(db);
        }
        if(File.Exists(dbFileName)){
            string content = File.ReadAllText(dbFileName);
            result = JsonConvert.DeserializeObject<T>(content);
            if(result == null) result = new T();
        }
        return result;
    }

    public void SaveDB(T db)
    {
        string dbFileName = _dataFile;
        if(!File.Exists(dbFileName)){
            using (StreamWriter sw = File.CreateText(dbFileName)){};
        }
        JsonSerializer serializer = new JsonSerializer();
        serializer.NullValueHandling = NullValueHandling.Ignore;
        using (StreamWriter sw = new StreamWriter(dbFileName))
        using (JsonWriter writer = new JsonTextWriter(sw))
        {
            serializer.Serialize(writer, db);
        }
    }
}
