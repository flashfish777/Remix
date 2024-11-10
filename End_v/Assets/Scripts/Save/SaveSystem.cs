using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SaveSystemTutorial
{
    public static class SaveSystem //使用时需要在类前加上[System.Serializable], 类中的私有参数需要写[SerializeField]
    {
        public static void SaveByPlayerPrefs(string key, object data)
        {
            var json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
        }

        public static string LoadFromPlayerPrefs(string key)
        {
            return PlayerPrefs.GetString(key);
        }

        #region Json
        public static void SaveByJson(string fileName, object data)
        {
            var json = JsonUtility.ToJson(data);
            var path = Path.Combine(Application.persistentDataPath, fileName);
            try
            {
                File.WriteAllText(path, json);
                Debug.Log(json);
            }
            catch (System.Exception e)
            {
                Debug.Log(e.ToString());
            }
        }

        public static T ReadFromJson<T>(string fileName)
        {
            var path = Path.Combine(Application.persistentDataPath, fileName);
            if (!File.Exists(path)) return default;
            var json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<T>(json);
            return data;
        }

        public static void DeleteSaveFile(string fileName)
        {
            var path = Path.Combine(Application.persistentDataPath, fileName);
            File.Delete(path);
        }
        #endregion
    }
}
