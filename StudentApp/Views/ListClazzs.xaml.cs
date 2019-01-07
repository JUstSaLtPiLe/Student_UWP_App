using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StudentApp.Entities;
using StudentApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace StudentApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListClazzs : Page
    {
        private ObservableCollection<Entities.Clazz> listAllClazzes;
        internal ObservableCollection<Entities.Clazz> ListAllClazzes { get => listAllClazzes; set => listAllClazzes = value; }
        public ListClazzs()
        {
            this.InitializeComponent();
            Get_List_Clazz();
        }
        private async void Get_List_Clazz()
        {
            this.listAllClazzes = new ObservableCollection<Entities.Clazz>();
            var response = await APIHandle.Get_Subjects();
            var responseContent = await response.Content.ReadAsStringAsync();
            var array = JArray.Parse(responseContent);
            foreach (var obj in array)
            {
                Entities.Clazz clazz = obj.ToObject<Entities.Clazz>();
                this.listAllClazzes.Add(clazz);
            }
        }

        private void StackPanel_Tap(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            StackPanel sp = sender as StackPanel;
            Entities.Clazz clazz = sp.Tag as Entities.Clazz;
            this.Frame.Navigate(typeof(Clazz));
        }
    }
}
