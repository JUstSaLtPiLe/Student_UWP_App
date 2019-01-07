using Newtonsoft.Json;
using StudentApp.Entities;
using StudentApp.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace StudentApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GeneralInformation : Page
    {
        public GeneralInformation()
        {
            this.InitializeComponent();
            Get_General_Infor();
        }

        public async void Get_General_Infor()
        {
            var file = await ApplicationData.Current.LocalFolder.TryGetItemAsync("token.txt");
            if (file != null)
            {
                var response = await APIHandle.Get_Student_Infor();
                var responseContent = await response.Content.ReadAsStringAsync();

                Entities.GeneralInformation genInfo = JsonConvert.DeserializeObject<Entities.GeneralInformation>(responseContent);
                this.Email.Text = genInfo.Email;
                this.Name.Text = genInfo.Name;
                this.Gender.Text = genInfo.Gender.ToString();
                //this.Birthday = genInfo.Dob.ToString(); loi chua get duoc
                this.Phone.Text = genInfo.Phone;
                this.Address.Text = genInfo.Address;

                this.EditFullName.Text = genInfo.Email;
                this.EditPhone.Text = genInfo.Phone;
                this.EditAddress.Text = genInfo.Address;
            }
        }

        private async void BtnLogOut_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ContentDialog deleteFileDialog = new ContentDialog
            {
                Title = "Warning!",
                Content = "Do you want to log out?",
                PrimaryButtonText = "Log out",
                CloseButtonText = "Quit"
            };

            ContentDialogResult result = await deleteFileDialog.ShowAsync();

            // Delete the file if the user clicked the primary button.
            /// Otherwise, do nothing.
            if (result == ContentDialogResult.Primary)
            {
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFile file = await storageFolder.GetFileAsync("token.txt");
                await file.DeleteAsync(StorageDeleteOption.PermanentDelete);

                var rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(MainPage), null, new EntranceNavigationTransitionInfo());
            }
            else
            {
                // The user clicked the CLoseButton, pressed ESC, Gamepad B, or the system back button.
                // Do nothing.
            }

        }

        public bool Validate_Edit()
        {
            var val = true;
            var fullName = this.EditFullName.Text;
            var password = this.EditPassword.Password;
            var phone = this.EditPhone.Text;
            var address = this.EditAddress.Text;
            if (fullName == "" || password == "" || phone == "" || address == "")
            {
                this.Error.Text = "You have to fill in all the fields.";
                val = false;
            }
            return val;
        }

        public async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (Validate_Edit())
            {
            }
        }
    }
}
