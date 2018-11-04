using System;
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

        private async void ButtonOpenRecordPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecordPage(), true);
        }

        private async void ButtonOpenRecordsListPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecordsListPage(), true);
        }

        #endregion
    }
}
