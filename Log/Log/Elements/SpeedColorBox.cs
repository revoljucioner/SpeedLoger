using Xamarin.Forms;

namespace Log.Elements
{
    public class SpeedColorBox : StackLayout
    {
        public SpeedColorBox(Color color, string text)
        {
            Orientation = StackOrientation.Horizontal;
            Padding = new Thickness (5, 0, 0, 5);

            var colorBox = new BoxView
            {
                Color = color,
                WidthRequest = 20,
                HeightRequest = 20
            };

            var speedLabel = new Label {FontSize = 15, Text = text};

            Children.Add(colorBox);
            Children.Add(speedLabel);
        }
    }
}