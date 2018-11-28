using System;
using Log.DependenciesOS;
using Log.Views;
using Xamarin.Forms;

namespace Log.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {


            InitializeComponent();

            StackLayoutStartRecord.GestureRecognizers.Add(
                new TapGestureRecognizer()
                {
                    Command = new Command(StackLayoutStartRecord_Clicked)
                });

            StackLayoutOpenRecordslistPage.GestureRecognizers.Add(
                new TapGestureRecognizer()
                {
                    Command = new Command(StackLayoutOpenRecordslistPage_Clicked)
                });

            StackLayoutStartSettings.GestureRecognizers.Add(
                new TapGestureRecognizer()
                {
                    Command = new Command(StackLayoutStartSettings_Clicked)
                });

            StackLayoutExit.GestureRecognizers.Add(
                new TapGestureRecognizer()
                {
                    Command = new Command(StackLayoutExit_Clicked)
                });
        }

        #region activities

        private async void StackLayoutStartRecord_Clicked()
        {
            App.PermissionsResolver.RequestLocationPermissions();
            App.PermissionsResolver.RequestPhonePermissions();
            await Navigation.PushAsync(new RecordPage(), true);
        }

        private async void StackLayoutOpenRecordslistPage_Clicked()
        {
            await Navigation.PushAsync(new TracksListPage(), true);
        }

        private async void StackLayoutStartSettings_Clicked()
        {
            // TODO:
        }

        private async void StackLayoutExit_Clicked()
        {
            DependencyService.Get<ICloseApplication>().TerminateApplication();
        }

        #endregion
    }
}
