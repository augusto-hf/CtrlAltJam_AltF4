using System.IO;
using System;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Utils {
    public static class Data 
    {
        public static void LoadSave<T>(string filePathSave, ref T data, string fileDefault)
        {
            if (CheckIfExistSave(filePathSave)) 
            {
                string json = File.ReadAllText(filePathSave);
                data = JsonConvert.DeserializeObject<T>(json); 
                return;
            }

            data = LoadDefaultSave(fileDefault, data);

        }

        public static void ResetFileSaved<T>(string fileDefault, ref T data, string filePath)
        {
            if (CheckIfExistSave(filePath))
            {
                File.Delete(filePath);
            }

            data = LoadDefaultSave(fileDefault, data);
        }

        private static T LoadDefaultSave<T>(string path, T data)
        {
            TextAsset jsonTextAsset = Resources.Load<TextAsset>(path); 
            string json = jsonTextAsset.text; 
            data = JsonConvert.DeserializeObject<T>(json); 

            return data;
        }

        public static bool CheckIfExistSave(string filePath)
        {
            if (File.Exists(filePath)) 
            {
                return true;
            }

            return false;
        }

        public static void Save<T>(string filePathSave, ref T data)
        {
            string json = JsonUtility.ToJson(data, true); 
            File.WriteAllText(filePathSave, json); 
        }


        public static bool FindKeyInArrayData<T>(string key, T data)
        {
            if (data is IEnumerable<string> stringArray)
            {
                return stringArray.Contains(key);
            }

            return false;
        }


    }

    /* Separe por classes os metodos em comuns, a base é essa. coloque só metodos bem simples.
    public static class otherClass 
    {
        public static metodo()
        {
            
        }
    }*/
}