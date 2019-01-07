using Newtonsoft.Json;
using StudentApp.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private static String Student_Url = "https://localhost:44320/api/studentResourcesAPI/StudentDetails";
        private static string Grade_Url = "";

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

        public async static Task<HttpResponseMessage> Get_Student_Infor()
        {
            string token = await Check_Token();

            HttpClient httpClient = new HttpClient();
            var content = new StringContent("");
            var response = httpClient.PostAsync(Student_Url + token, content);
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

        public async static Task<HttpResponseMessage> Get_Grade()
        {
            string token = await Check_Token();

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            var response = await httpClient.GetAsync(Grade_Url);
            return response;
        }

        public async static Task<HttpResponseMessage> Get_Subjects()
        {
            //StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            //StorageFile file = await storageFolder.GetFileAsync("token.txt");
            //string token = await FileIO.ReadTextAsync(file);
            string token = await Check_Token();

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            var response = httpClient.GetAsync(Subject_Url);
            return response.Result;
        }

        //public async static Task<HttpResponseMessage> Edit_General_Infor(Account account)
        //{
        //    string token = await Check_Token();

        //    HttpClient httpClient = new HttpClient();
        //    httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
        //    var content = new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, "application/json");
        //}
        //public async static Task<HttpResponseMessage> Get_Clazz_Infor()
        //{
        //    string token = await Check_Token();

        //    HttpClient httpClient = new HttpClient();
        //    httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
        //}
    }
}
