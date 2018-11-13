using System;
using Xamarin.Forms;

namespace Log.Pages
{
	public partial class MainPageCarousel2 : CarouselPage
	{
		public MainPageCarousel2()
		{
			InitializeComponent ();
		}

	    #region activities

	    private async void StackLayoutStartRecord_Clicked(object sender, EventArgs e)
	    {
            await Navigation.PushAsync(new RecordPage(), true);
        }

	    private async void StackLayoutOpenRecordslistPage_Clicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new RecordsListPage(), true);
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
