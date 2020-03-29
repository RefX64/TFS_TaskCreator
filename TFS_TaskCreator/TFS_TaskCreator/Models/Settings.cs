using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFS_TaskCreator.Models
{
    public class Settings
    {
        public string PersonalAccessToken { get; set; }
        public string URI { get; set; }
        public string ProjectName { get; set; }
        public string CustomDepartment { get; set; } // If you have any company specific fields

        public TFS_Item TFSDefaults { get; set; }

        public void WriteCurrentSettings()
        {
            using (StreamWriter writer = new StreamWriter("settings.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(writer, this);
            }
        }

        public static Settings LoadSettings()
        {
            if (!File.Exists("settings.json")) { return null; }

            try
            {
                using (StreamReader reader = new StreamReader("settings.json"))
                {
                    string json = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<Settings>(json);
                }
            }
            catch (Exception)
            {
#if DEBUG
                throw;
#endif
                return null;
            }
        }
    }
}
