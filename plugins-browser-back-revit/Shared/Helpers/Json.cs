using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;

namespace plugins_browser_back_revit
{
    public static class Json
    {
        public static string evironmentFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string mainFolderPath => $"{evironmentFolderPath}\\plugins-browser-back-revit";

        static JsonSerializerSettings settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            CheckAdditionalContent = true,
            Formatting = Formatting.Indented
        };

        public static T Deserialize<T>(string json) where T : new()
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json, settings);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: Failed to deserialize json string into a valid object___{ex.Message}");
            }
        }

        public static T Read<T>(string path) where T : new()
        {
            string jsonFilePath = $"{mainFolderPath}\\{path}.json";

            T result = default(T);
            try
            {
                if (File.Exists(jsonFilePath))
                {
                    using (var reader = File.OpenText(jsonFilePath))
                    {
                        string fileText = reader.ReadToEnd();
                        var deserializedObject = Deserialize<T>(fileText);
                        if (deserializedObject != null)
                        {
                            result = deserializedObject;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return result;
        }
    }
}
