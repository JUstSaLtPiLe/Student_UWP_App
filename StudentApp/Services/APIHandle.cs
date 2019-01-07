using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace StudentApp.Services
{
    class APIHandle
    {
        private static String Clazz_Url = "https://localhost:44320/api/studentResourcesAPI/ClazzsIndex";
        private static String ClazzDetail_Url = "https://localhost:44320/api/studentResourcesAPI/ClazzDetails";
        private static String Subject_Url = "https://localhost:44320/api/studentResourcesAPI/SubjectsIndex";
        private static String Account_Url = "https://localhost:44320/api/studentResourcesAPI/AccountsIndex";

        public static async Task<string> Check_Token()
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            try
            {
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFile file = await storageFolder.GetFileAsync("token.txt");
                string token = await FileIO.ReadTextAsync(file);
                return token;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async static Task<HttpResponseMessage> Get_Member_Infor()
        {
            string token = await Check_Token();

            HttpClient httpClient = new HttpClient();
            var content = new StringContent("");
            var response = httpClient.PostAsync(Account_Url + token, content);
            return response.Result;
        }

        public async static Task<HttpResponseMessage> Get_Clazzes()
        {
            string token = await Check_Token();

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            var response = httpClient.GetAsync(Clazz_Url).Result;
            return response;
        }
    }
}
