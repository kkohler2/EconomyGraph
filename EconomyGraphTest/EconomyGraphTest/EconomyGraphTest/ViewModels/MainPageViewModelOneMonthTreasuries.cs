//using EconomyGraph.Models;
//using EconomyGraph.ViewModels;
//using SkiaSharp;
//using System.Collections.Generic;

//namespace EconomyGraphTest.ViewModels
//{
//    public class MainPageViewModelOneMonthTreasuries
//    {
//        public MainPageViewModelOneMonthTreasuries()
//        {
//            #region HorizontalBarGraphViewModel
//            HorizontalBarGraphViewModel = new HorizontalBarGraphViewModel
//            {
//                BackgroundColor = SKColors.AliceBlue,
//                Title = new Label
//                {
//                    Color = SKColors.Black,
//                    Text = "Do you support or oppose 'defund the police'?",
//                    PointSize = 20,
//                    TextAlignment = Xamarin.Forms.TextAlignment.Start
//                },
//                SubTitle = new Label
//                {
//                    Bold = false,
//                    Color = SKColors.Gray,
//                    Text = "Among all Americans",
//                    PointSize = 20,
//                    TextAlignment = Xamarin.Forms.TextAlignment.Start
//                },
//                LeftFooter = new Label
//                {
//                    Bold = false,
//                    Color = SKColors.Gray,
//                    Text = "Source: June 2020 ABC/Ipsos poll",
//                    PointSize = 20,
//                    TextAlignment = Xamarin.Forms.TextAlignment.Start
//                },
//                RightFooter = new Label
//                {
//                    Bold = false,
//                    Color = SKColors.Gray,
//                    Text = "THE WASHINGTON POST",
//                    PointSize = 20,
//                    TextAlignment = Xamarin.Forms.TextAlignment.Start
//                }
//            };
//            HorizontalBarGraphViewModel.DataPoints.Add(new HorizontalBarGraphDataPoint
//            {
//                Color = SKColors.Blue,
//                TextColor = SKColors.Gray,
//                Label = "Oppose",
//                Bold = true,
//                PointSize = 20,
//                Value = 64
//            });
//            HorizontalBarGraphViewModel.DataPoints.Add(new HorizontalBarGraphDataPoint
//            {
//                Color = SKColors.Gray,
//                TextColor = SKColors.Gray,
//                Label = "Support",
//                Bold = false,
//                PointSize = 20,
//                Value = 34
//            });
//            #endregion

//            #region LineGraphViewModel
//            LineGraphViewModel = new LineGraphViewModel
//            {
//                BackgroundColor = SKColors.AliceBlue,
//                Title = new Label
//                {
//                    //Color = SKColors.Black,
//                    Text = "This is the graph title\nThis is the second line",
//                    PointSize = 25,
//                    TextAlignment = Xamarin.Forms.TextAlignment.Center
//                },
//                LeftFooter = new Footer
//                {
//                    //Color = SKColors.Red,
//                    Text = "Left",
//                    PointSize = 20
//                },
//                CenterFooter = new Footer
//                {
//                    Color = SKColors.Green,
//                    Text = "Center",
//                    PointSize = 20
//                },
//                RightFooter = new Footer
//                {
//                    Color = SKColors.Blue,
//                    Text = "Right",
//                    PointSize = 20
//                },
//                HorizontalLineColor = SKColors.Black,
//                //OddRowHorizontalColor = SKColors.AntiqueWhite,
//                //OddRowVerticalColor = SKColors.AntiqueWhite,
//                VerticalLineColor = SKColors.Black,
//                XLabelAlignment = Xamarin.Forms.TextAlignment.Center,
//                XLabelColor = SKColors.Black,
//                XLabelPointSize = 20,
//                YFirstLabelFormat = "{0:F}%",
//                YLabelFormat = "{0:F}",
//                YLabelAlignment = Xamarin.Forms.TextAlignment.Start,
//                YLabelColor = SKColors.Black,
//                YLabelPointSize = 20,
//                LineColor = SKColors.Red,
//                HorizontalLabelPrecision = 0.05M,
//                BottomGraphValue = .05,
//                //TopGraphValue = 0.3,
//                DataGroups = new List<DataGroup>
//                {
//                    new DataGroup
//                    {
//                        Label = "May",
//                        DataPoints = new List<double>
//                        {
//                            0.1,0.1,0.09,0.08,0.1,0.1,0.09,0.1,0.1,0.09,0.09,0.1,0.09,0.08,0.09,0.09,0.1,0.11,0.14,0.13
//                        }
//                    },
//                    new DataGroup
//                    {
//                        Label = "June",
//                        DataPoints = new List<double>
//                        {
//                            0.12,0.12,0.12,0.13,0.13,0.15,0.14,0.13,0.14
//                        }
//                    }
//                }
//            };
//            #endregion

//            #region BarGraphViewModel
//            BarGraphViewModel = new BarGraphViewModel
//            {
//                BackgroundColor = SKColors.AliceBlue,
//                Title = new Label
//                {
//                    //Color = SKColors.Black,
//                    Text = "This is a BAR GRAPH!",
//                    PointSize = 25,
//                    TextAlignment = Xamarin.Forms.TextAlignment.Center
//                },
//                //LeftFooter = new Footer
//                //{
//                //    //Color = SKColors.Red,
//                //    Text = "Left",
//                //    PointSize = 20
//                //},
//                //CenterFooter = new Footer
//                //{
//                //    Bold = true,
//                //    Color = SKColors.Green,
//                //    Text = "Center",
//                //    PointSize = 20
//                //},
//                //RightFooter = new Footer
//                //{
//                //    Color = SKColors.Blue,
//                //    Text = "Right",
//                //    PointSize = 20
//                //},
//                HorizontalLineColor = SKColors.Black,
//                //OddRowHorizontalColor = SKColors.AntiqueWhite,
//                //OddRowVerticalColor = SKColors.AntiqueWhite,
//                VerticalLineColor = SKColors.Black,
//                XLabelAlignment = Xamarin.Forms.TextAlignment.Center,
//                XLabelColor = SKColors.Black,
//                XLabelPointSize = 20,
//                YFirstLabelFormat = "{0:F}%",
//                YLabelFormat = "{0:F}",
//                YLabelAlignment = Xamarin.Forms.TextAlignment.Start,
//                YLabelColor = SKColors.Black,
//                YLabelPointSize = 20,
//                LineColor = SKColors.Red,
//                HorizontalLabelPrecision = 0.05M,
//                BottomGraphValue = .05,
//                //TopGraphValue = 0.3,
//                DataGroups = new List<DataGroup>
//                {
//                    new DataGroup
//                    {
//                        Label = "May",
//                        DataPoints = new List<double>
//                        {
//                            0.1,0.1,0.09,0.08,0.1,0.1,0.09,0.1,0.1,0.09,0.09,0.1,0.09,0.08,0.09,0.09,0.1,0.11,0.14,0.13
//                        }
//                    },
//                    new DataGroup
//                    {
//                        Label = "June",
//                        DataPoints = new List<double>
//                        {
//                            0.12,0.12,0.12,0.13,0.13,0.15,0.14,0.13,0.14
//                        }
//                    }
//                }
//            };
//            #endregion
//        }

//        public HorizontalBarGraphViewModel HorizontalBarGraphViewModel { get; set; }
//        public LineGraphViewModel LineGraphViewModel { get; set; }
//        public BarGraphViewModel BarGraphViewModel { get; set; }
//    }
//}
