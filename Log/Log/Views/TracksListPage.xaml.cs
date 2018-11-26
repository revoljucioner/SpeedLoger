using Log.ViewModels;
using Xamarin.Forms;

namespace Log.Views
{
    public partial class TracksListPage : ContentPage
    {
        public TracksListPage()
        {
            InitializeComponent();
            BindingContext = new TracksListViewModel() { Navigation = this.Navigation };
        }
    }
}