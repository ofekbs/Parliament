using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace Parliament
{
    class Server
    {
        static IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "jyom5GqRc4rsIYWZZXxWsxdON4whClzwd2p5Jh6l",
            BasePath = "https://parliament-ofekbs.firebaseio.com/"
        };
        static IFirebaseClient client;

        public static bool Connect()
        {
            client = new FireSharp.FirebaseClient(config);

            if (client != null)
                return true;

            else
                return false;
        }

        public static async void WriteData(dynamic data, string path)
        {
            SetResponse response = await client.SetTaskAsync(path, data);
        }

        public static async Task<T> ReadData<T>(string path)
        {
            FirebaseResponse response = await client.GetTaskAsync(path);

            return response.ResultAs<T>();
        }

        public static async void UpdateData(dynamic data, string path)
        {
            FirebaseResponse response = await client.UpdateTaskAsync(path, data);
        }

        public static async void DeleteData<T>(string path)
        {
            bool checker = await Server.IsExist<T>(path);

            if (checker)
            {
                FirebaseResponse response = await client.DeleteTaskAsync(path);
            }
        }

        public static async Task<bool> IsExist<T>(string path)
        {
            try
            {
                T res = await Server.ReadData<T>(path);
            }

            catch
            {
                return false;
            }

            return true;
        }
    }
}
