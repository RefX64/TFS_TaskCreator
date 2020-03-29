using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TFS_TaskCreator.Models
{
    public static class Utilities
    {
        public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
    }

    public static class Enums
    {
        public enum WorkItemType
        {
            Unknown = 0,
            UserStory = 1,
            Task = 2,
            Bug = 3
        };
    }
}
