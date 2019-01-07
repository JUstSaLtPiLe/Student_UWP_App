using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public sealed partial class SplitView : Page
    {
        public SplitView()
        {
            this.InitializeComponent();
        }

        public void OnActiveAsync(object sender, RoutedEventArgs e)
        {
            this.MainFrame.Navigate(typeof(Views.ListClazz));
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.MySplitView.IsPaneOpen = !this.MySplitView.IsPaneOpen;
            if (!this.MySplitView.IsPaneOpen)
            {
                this.StackIcon.Margin = new Thickness(10, 50, 0, 0);
                this.MainFrame.Margin = new Thickness(0, 0, 0, 0);
            }
            else
            {
                this.StackIcon.Margin = new Thickness(50, 50, 0, 0);
                this.MainFrame.Margin = new Thickness(250, 0, 0, 0);
            }
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            switch (radio.Tag.ToString())
            {
                case "Search":
                    this.MySplitView.IsPaneOpen = true;
                    this.search_box.Focus(FocusState.Programmatic);
                    break;
                case "Home":
                    this.MainFrame.Navigate(typeof(Views.ListClazz));
                    break;
                case "Account":
                    this.MainFrame.Navigate(typeof(GeneralInformation));
                    break;
                default:
                    break;
            }

        }

        private void MySplitView_PaneClosed(Windows.UI.Xaml.Controls.SplitView sender, object args)
        {
            this.MainFrame.Margin = new Thickness(0, 0, 0, 0);
        }

        private void search_box_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {

        }
    }
}
