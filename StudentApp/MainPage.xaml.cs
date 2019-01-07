using Newtonsoft.Json;
using StudentApp.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace StudentApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private static string API_LOGIN = "https://localhost:44320/api/StudentResourcesAPI/Login";

        public MainPage()
        {
            this.InitializeComponent();
        }

        public bool Validate_Login()
        {
            var val = true;
            var rollNumber = this.RollNumber.Text;
            var passwordText = this.Password.Password;
            if (rollNumber == "")
            {
                this.rollNumber.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                this.rollNumber.Text = "Roll number can't be empty";
                val = false;
            }
            else
            {
                this.rollNumber.Text = "";
            }
            if (passwordText == "")
            {
                this.password.Text = "Password can't be empty";
                this.password.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                val = false;
            }
            else
            {
                this.password.Text = "";
            }

            return val;
        }

        private async void LoginForm_Loaded(object sender, RoutedEventArgs e)
        {
            string rootPath = ApplicationData.Current.LocalFolder.Path;
            string filePath = Path.Combine(rootPath, "loginStatus.txt");
            if (!System.IO.File.Exists(filePath))
            {

            }
            else
            {
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFile file_loginStatus = await storageFolder.GetFileAsync("loginStatus.txt");
                string loginStatus = await FileIO.ReadTextAsync(file_loginStatus);
                if (loginStatus == "loggedIn")
                {
                    this.Frame.Navigate(typeof(SplitView));
                }
            }

        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Validate_Login())
                {
                    Account account = new Account();
                    account.RollNumber = this.RollNumber.Text;
                    account.Password = this.Password.Password;

                    // Get token
                    HttpClient httpClient = new HttpClient();
                    StringContent content = new StringContent(JsonConvert.SerializeObject(account), System.Text.Encoding.UTF8, "application/json");
                    var response = httpClient.PostAsync(API_LOGIN, content).Result;
                    var responseContent = await response.Content.ReadAsStringAsync();
                    //Debug.WriteLine(responseContent);
                    TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

                    // save token
                    StorageFolder folder = ApplicationData.Current.LocalFolder;
                    StorageFile file_token = await folder.CreateFileAsync("token.txt", CreationCollisionOption.ReplaceExisting);
                    await FileIO.WriteTextAsync(file_token, responseContent);
                    StorageFile file_loginStatus = await folder.CreateFileAsync("loginStatus.txt", CreationCollisionOption.ReplaceExisting);
                    await FileIO.WriteTextAsync(file_loginStatus, "loggedIn");
                    this.Frame.Navigate(typeof(SplitView));
                }
            } catch (AggregateException ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
