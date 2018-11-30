using System;
using System.Linq;
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

            AddGestureRecognizer(StackLayoutStartRecord, StackLayoutStartRecord_Clicked);
            AddGestureRecognizer(StackLayoutOpenRecordslistPage, StackLayoutOpenRecordslistPage_Clicked);
            AddGestureRecognizer(StackLayoutStartSettings, StackLayoutStartSettings_Clicked);
            AddGestureRecognizer(StackLayoutExit, StackLayoutExit_Clicked);
        }

        private void AddGestureRecognizer(StackLayout stackLayout, Action action)
        {
            stackLayout.GestureRecognizers.Add(
                new TapGestureRecognizer()
                {
                    Command = new Command(action)
                });
        }

        private async void StackLayoutStartRecord_Clicked(object sender, EventArgs e)
        {
            StackLayoutStartRecord_Clicked();
        }

        private async void StackLayoutOpenRecordslistPage_Clicked(object sender, EventArgs e)
        {
            StackLayoutOpenRecordslistPage_Clicked();

        }

        private async void StackLayoutStartSettings_Clicked(object sender, EventArgs e)
        {
            StackLayoutStartSettings_Clicked();

        }

        private async void StackLayoutExit_Clicked(object sender, EventArgs e)
        {
            StackLayoutExit_Clicked();

        }

        private async void StackLayoutStartRecord_Clicked()
        {
            var allPermissions = App.PermissionsStorage.PermissionsLocation.ToList();
            allPermissions.AddRange(App.PermissionsStorage.PermissionsPhone.ToList());
            var allPermissionsArray = allPermissions.ToArray();

            if (!App.PermissionsResolver.IsAllPermissionsChecked(allPermissionsArray))
            {
                App.PermissionsResolver.SetPermissions(allPermissionsArray, true);
                await DisplayAlert("Allow Permissions", "Before starting recording you must allow GPS and Phone Permissions! Application collect data about cell signal quality just to improve the quality of service.", "OK");
            }
            else
            {
                await Navigation.PushAsync(new RecordPage(), true);
            }
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
    }
}
