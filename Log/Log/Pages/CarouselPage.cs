using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Log.Pages
{
    public class CarouselPage2 : ContentPage
    {
        public DotButtonsLayout dotLayout;
        public CarouselView carousel;

        public class Details
        {
            //public int position { get; set; }
            //public string Header { get; set; }
            //public string Content { get; set; }
            public Button obscollection { get; set; }
        }
        public class values
        {
            public string value { get; set; }
        }
        public CarouselPage2()
        {
            Button aaaa = new Button();
            aaaa.Text = "sdsdsds";

            Button bbbb = new Button();
            bbbb.Text = "sdsdsds";

            Button cccc = new Button();
            cccc.Text = "sdsdsds";

            ObservableCollection<Details> collection = new ObservableCollection<Details>{
                new Details{ obscollection = aaaa},
                new Details{obscollection = bbbb },
                new Details{obscollection = cccc},

                //new Details{ obscollection = aaaa, Header="Lorem Ipsum", Content="Lorem ipsum dolor sit amet, conselectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore." },
                //new Details{obscollection = bbbb, Header="Tempor Labore", Content="Eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad mimim veni." },
                //new Details{obscollection = cccc, Header="Aliquip Veniam", Content="Ut anim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."},
        };

            BackgroundColor = Color.FromHex("#FFFFFF");

            StackLayout body = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            carousel = new CarouselView()
            {
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };


            DataTemplate template = new DataTemplate(() =>
            {

                var stacksample = new StackLayout()
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Color.Aqua
                };

                ListView lstview = new ListView()
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    SeparatorVisibility = SeparatorVisibility.Default,
                    ItemTemplate = new DataTemplate((typeof(cell))),
                    BackgroundColor = Color.Aqua
                };
                lstview.SetBinding(ListView.ItemsSourceProperty, "obscollection");

                lstview.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
                {
                    if (e.SelectedItem == null)
                    {
                        return;
                    }

                    var item = e.SelectedItem as values;

                    DisplayAlert("Item Selected", item.value, "Ok");

                    ((ListView)sender).SelectedItem = null;

                };


                stacksample.Children.Add(lstview);

                return stacksample;
            });

            carousel.ItemTemplate = template;

            carousel.PositionSelected += pageChanged;

            carousel.ItemsSource = collection;

            dotLayout = new DotButtonsLayout(collection.Count, Color.Red, 15);

            foreach (DotButton dot in dotLayout.dots)

                dot.Clicked += dotClicked;

            body.Children.Add(carousel);

            body.Children.Add(dotLayout);

            StackLayout stack = new StackLayout()
            {
                Children = { body },
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White
            };

            Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);

            Content = stack;

        }
        private void pageChanged(object sender, SelectedPositionChangedEventArgs e)
        {
            var position = (int)(e.SelectedPosition);
            for (int i = 0; i < dotLayout.dots.Length; i++)
                if (position == i)
                {
                    dotLayout.dots[i].Opacity = 1;
                }
                else
                {
                    dotLayout.dots[i].Opacity = 0.2;
                }
        }

        private void dotClicked(object sender)
        {
            var button = (DotButton)sender;
            int index = button.index;
            carousel.Position = index;
        }
    }

    public class DotButtonsLayout : StackLayout
    {
        public DotButton[] dots;
        public DotButtonsLayout(int dotCount, Color dotColor, int dotSize)
        {

            dots = new DotButton[dotCount];

            Orientation = StackOrientation.Horizontal;
            VerticalOptions = LayoutOptions.Center;
            HorizontalOptions = LayoutOptions.Center;
            Spacing = 20;

            for (int i = 0; i < dotCount; i++)
            {
                dots[i] = new DotButton
                {
                    HeightRequest = dotSize,
                    WidthRequest = dotSize,
                    BackgroundColor = dotColor,
                    Opacity = 0.2
                };
                dots[i].index = i;
                dots[i].layout = this;
                Children.Add(dots[i]);
            }
            dots[0].Opacity = 1;
        }
    }


    public class DotButton : BoxView
    {
        public int index;
        public DotButtonsLayout layout;
        public event ClickHandler Clicked;
        public delegate void ClickHandler(DotButton sender);
        public DotButton()
        {
            var clickCheck = new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    if (Clicked != null)
                    {
                        Clicked(this);
                    }
                })
            };
            GestureRecognizers.Add(clickCheck);
        }
    }

    public class cell : ViewCell
    {
        public cell()
        {
            Label headerLabel = new Label()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                TextColor = Color.Black
            };
            headerLabel.SetBinding(Label.TextProperty, "value");

            StackLayout stack = new StackLayout()
            {
                Children = { headerLabel },
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Aqua
            };

            View = stack;

        }

    }

}

