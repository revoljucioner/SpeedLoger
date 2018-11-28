using System;
using Log.Views;
using Xamarin.Forms;

namespace Log.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        #region activities

        private async void StackLayoutStartRecord_Clicked(object sender, EventArgs e)
        {
            App.PermissionsResolver.RequestLocationPermissions();
            App.PermissionsResolver.RequestPhonePermissions();
            await Navigation.PushAsync(new RecordPage(), true);
        }

        private async void StackLayoutOpenRecordslistPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TracksListPage(), true);
        }

        private async void StackLayoutStartSettings_Clicked(object sender, EventArgs e)
        {
            // TODO:
        }

        private async void StackLayoutStartExit_Clicked(object sender, EventArgs e)
        {
            // TODO:
        }

        #endregion
    }
}
