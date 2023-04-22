using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Save
{
    public static class JsonSaveLoad
    {
        private static readonly string saveFilePath = Path.Combine(Application.persistentDataPath, "save.json");

        public static async UniTask SaveDataAsync(UserData userData)
        {
            string json = JsonUtility.ToJson(userData);
            using (StreamWriter writer = new StreamWriter(saveFilePath))
            {
                await writer.WriteAsync(json);
            }
        }

        public static async UniTask<UserData> LoadDataAsync()
        {
            if (!File.Exists(saveFilePath))
            {
                return null;
            }

            using (StreamReader reader = new StreamReader(saveFilePath))
            {
                string json = await reader.ReadToEndAsync();
                return JsonUtility.FromJson<UserData>(json);
            }
        }
    }
}