using SkiaSharp;
using System.Collections.Generic;
using Xamarin.Forms;

namespace EconomyGraph.ViewModels
{
    public class MainPageViewModel
    {
        public MainPageViewModel()
        {
            #region HorizontalBarGraphViewModel
            HorizontalBarGraphViewModel = new HorizontalBarGraphViewModel
            {
                BackgroundColor = SKColors.AliceBlue,
                Title = new Models.Label
                {
                    Bold = true,
                    Color = SKColors.Black,
                    Text = "Do you support or oppose 'defund the police'?",
                    PointSize = 20,
                    TextAlignment = TextAlignment.Start
                },
                SubTitle = new Models.Label
                {
                    Bold = false,
                    Color = SKColors.Gray,
                    Text = "Among all Americans",
                    PointSize = 20,
                    TextAlignment = TextAlignment.Start
                },
                LeftFooter = new Models.Label
                {
                    Bold = false,
                    Color = SKColors.Gray,
                    Text = "Source: June 2020 ABC/Ipsos poll",
                    PointSize = 20,
                    TextAlignment = TextAlignment.Start
                },
                RightFooter = new Models.Label
                {
                    Bold = false,
                    Color = SKColors.Gray,
                    Text = "THE WASHINGTON POST",
                    PointSize = 20,
                    TextAlignment = TextAlignment.Start
                }
            };
            HorizontalBarGraphViewModel.DataPoints.Add(new Models.DataPoint
            {
                Color = SKColors.Blue,
                TextColor = SKColors.Gray,
                Label = "Oppose",
                Bold = true,
                PointSize = 20,
                Value = 64
            });
            HorizontalBarGraphViewModel.DataPoints.Add(new Models.DataPoint
            {
                Color = SKColors.Gray,
                TextColor = SKColors.Gray,
                Label = "Support",
                Bold = false,
                PointSize = 20,
                Value = 34
            });
            #endregion

            #region LineGraphViewModel
            LineGraphViewModel = new LineGraphViewModel
            {
                BackgroundColor = SKColors.AliceBlue,
                Title = new Models.Label
                {
                    Bold = true,
                    Color = SKColors.Black,
                    Text = "This is the graph title",
                    PointSize = 25,
                    TextAlignment = TextAlignment.Center
                },
                //BottomGraphValue
                HorizontalLines = true,
                //OddRowHorizontalColor = SKColors.AntiqueWhite,
                //OddRowVerticalColor = SKColors.AntiqueWhite,
                VerticalLines = true,
                XLabelAlignment = TextAlignment.Center,
                XLabelColor = SKColors.Black,
                XLabelPointSize = 20,
                YFirstLabelFormat = "{0:F}%",
                YLabelFormat = "{0:F}",
                YLabelAlignment = TextAlignment.Start,
                YLabelColor = SKColors.Black,
                YLabelPointSize = 20,
                LineColor = SKColors.Red,
                HorizontalLabelPrecision = 0.05M,
                BottomGraphValue = .05,
                DataGroups = new List<Models.DataGroup>
                {
                    new Models.DataGroup
                    {
                        Label = "May",
                        DataPoints = new List<double>
                        {
                            0.1,0.1,0.09,0.08,0.1,0.1,0.09,0.1,0.1,0.09,0.09,0.1,0.09,0.08,0.09,0.09,0.1,0.11,0.14,0.13
                        }
                    },
                    new Models.DataGroup
                    {
                        Label = "June",
                        DataPoints = new List<double>
                        {
                            0.12,0.12,0.12,0.13,0.13,0.15,0.14,0.13,0.14
                        }
                    }
                }
            };
            #endregion
        }

        public HorizontalBarGraphViewModel HorizontalBarGraphViewModel { get; set; }
        public LineGraphViewModel LineGraphViewModel { get; set; }
    }
}
