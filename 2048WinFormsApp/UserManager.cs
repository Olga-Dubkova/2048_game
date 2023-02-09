using Newtonsoft.Json;
using System.Collections.Generic;

namespace _2048WinFormsApp
{
    public class UserManager
    {
        public static string path = "results.json";
        public static List<User> GetAll()
        {
            if (FileProvider.Exists(path))
            {
                var jsonData = FileProvider.GetValue(path);
                var userResults = JsonConvert.DeserializeObject<List<User>>(jsonData);
                return userResults;
            }
            return new List<User>();
        }
        public static void Add(User newUser)
        {
            var users = GetAll();
            users.Add(newUser);

            var jsonData = JsonConvert.SerializeObject(users);
            FileProvider.Replace(path, jsonData);
            
        }
    }
}
