using System.Linq;
using Log.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Log.Pages
{
	public partial class MapPage : ContentPage
	{
		public MapPage (Track track)
		{
			InitializeComponent();

			customMap.SnappedPointsList = JsonConvert.DeserializeObject<SnappedPoint[]>(track.SnappedPointsArraySerialize).ToList();  

			customMap.MoveToRegion (MapSpan.FromCenterAndRadius (customMap.SnappedPointsList.First().Position, Distance.FromMiles (1.0)));
		}
	}
}