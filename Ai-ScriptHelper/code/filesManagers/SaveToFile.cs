using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveToFile
{
    public static byte[] ConvertObjectToBytes(object _objectToConvert)
    {
        byte[] serializedBytes;

        using (MemoryStream memoryStream = new MemoryStream())
        {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, _objectToConvert);

            serializedBytes = memoryStream.ToArray();
        }

        return serializedBytes;
    }

    public static object ConvertBytesToObject(byte[] _bytesToConvert)
    {
        object deserializedObject = null;

        using (MemoryStream memoryStream = new MemoryStream(_bytesToConvert))
        {
            // Créez une instance de BinaryFormatter pour désérialiser les bytes en un objet
            IFormatter formatter = new BinaryFormatter();
            deserializedObject = formatter.Deserialize(memoryStream);
        }

        return deserializedObject;
    }

    public static byte[] LoadFile(string _filePath)
    {
        if (File.Exists(_filePath))
        {
            return File.ReadAllBytes(_filePath);
        }
        else
        {
            return new byte[0];
        }
    }

    public static void SaveFile(byte[] _dataToSave, string _filePath)
    {
        File.WriteAllBytes(_filePath, _dataToSave);
    }
}