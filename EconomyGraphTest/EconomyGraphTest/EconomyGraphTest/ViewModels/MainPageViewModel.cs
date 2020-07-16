﻿using EconomyGraph.Models;
using EconomyGraph.ViewModels;
using SkiaSharp;
using System.Collections.Generic;

namespace EconomyGraphTest.ViewModels
{
    /// <summary>
    /// Quarterly GDP Values
    /// </summary>
    public class MainPageViewModel
    {
        public MainPageViewModel()
        {
            #region HorizontalBarGraphViewModel
            HorizontalBarGraphViewModel = new HorizontalBarGraphViewModel
            {
                BackgroundColor = SKColors.AliceBlue,
                Title = new Label
                {
                    Bold = true,
                    Color = SKColors.Black,
                    Text = "Do you support or oppose 'defund the police'?",
                    PointSize = 20,
                    TextAlignment = Xamarin.Forms.TextAlignment.Start
                },
                SubTitle = new Label
                {
                    Bold = false,
                    Color = SKColors.Gray,
                    Text = "Among all Americans",
                    PointSize = 20,
                    TextAlignment = Xamarin.Forms.TextAlignment.Start
                },
                LeftFooter = new Label
                {
                    Bold = false,
                    Color = SKColors.Gray,
                    Text = "Source: June 2020 ABC/Ipsos poll",
                    PointSize = 20,
                    TextAlignment = Xamarin.Forms.TextAlignment.Start
                },
                RightFooter = new Label
                {
                    Bold = false,
                    Color = SKColors.Gray,
                    Text = "THE WASHINGTON POST",
                    PointSize = 20,
                    TextAlignment = Xamarin.Forms.TextAlignment.Start
                }
            };
            HorizontalBarGraphViewModel.DataPoints.Add(new DataPoint
            {
                Color = SKColors.Blue,
                TextColor = SKColors.Gray,
                Label = "Oppose",
                Bold = true,
                PointSize = 20,
                Value = 64
            });
            HorizontalBarGraphViewModel.DataPoints.Add(new DataPoint
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
                Title = new Label
                {
                    Bold = true,
                    //Color = SKColors.Black,
                    Text = "Quarterly GDP",
                    PointSize = 25,
                    TextAlignment = Xamarin.Forms.TextAlignment.Center
                },
                //LeftFooter = new Footer
                //{
                //    //Color = SKColors.Red,
                //    Text = "Left",
                //    PointSize = 20
                //},
                //CenterFooter = new Footer
                //{
                //    Bold = true,
                //    Color = SKColors.Green,
                //    Text = "Center",
                //    PointSize = 20
                //},
                //RightFooter = new Footer
                //{
                //    Color = SKColors.Blue,
                //    Text = "Right",
                //    PointSize = 20
                //},
                HorizontalLines = true,
                //OddRowHorizontalColor = SKColors.AntiqueWhite,
                OddRowVerticalColor = SKColors.AntiqueWhite,
                VerticalLines = true,
                XLabelAlignment = Xamarin.Forms.TextAlignment.Center,
                XLabelColor = SKColors.Black,
                XLabelPointSize = 20,
                YFirstLabelFormat = "{0:F}%",
                YLabelFormat = "{0:F}",
                YLabelAlignment = Xamarin.Forms.TextAlignment.Start,
                YLabelColor = SKColors.Black,
                YLabelPointSize = 20,
                LineColor = SKColors.Red,
                HorizontalLabelPrecision = 1M,
                //BottomGraphValue = .05,
                //TopGraphValue = 0.3,
                DataGroups = new List<DataGroup>
                {
                    new DataGroup
                    {
                        Label = "2013",
                        DataPoints = new List<double>
                        {
                            3.6,0.5,3.2,3.2
                        }
                    },
                    new DataGroup
                    {
                        Label = "2014",
                        DataPoints = new List<double>
                        {
                            //1.1,5.5,5,2.3
                            -1.1,5.5,5,2.3
                        }
                    },
                    new DataGroup
                    {
                        Label = "2015",
                        DataPoints = new List<double>
                        {
                            3.2,3,1.3,0.1
                        }
                    },
                    new DataGroup
                    {
                        Label = "2016",
                        DataPoints = new List<double>
                        {
                            2,1.9,2.2,2
                        }
                    },
                    new DataGroup
                    {
                        Label = "2017",
                        DataPoints = new List<double>
                        {
                            2.3,2.2,3.2,3.5
                        }
                    },
                    new DataGroup
                    {
                        Label = "2018",
                        DataPoints = new List<double>
                        {
                            2.5,3.5,2.9,1.1
                        }
                    },
                    new DataGroup
                    {
                        Label = "2019",
                        DataPoints = new List<double>
                        {
                            3.1,2,2.1,2.1
                        }
                    },
                    new DataGroup
                    {
                        DataPoints = new List<double>
                        {
                            -4.8
                        }
                    }
                }
            };
            #endregion

            #region BarGraphViewModel
            BarGraphViewModel = new BarGraphViewModel
            {
                BackgroundColor = SKColors.AliceBlue,
                Title = new Label
                {
                    Bold = true,
                    //Color = SKColors.Black,
                    Text = "This is a BAR GRAPH!",
                    PointSize = 25,
                    TextAlignment = Xamarin.Forms.TextAlignment.Center
                },
                //LeftFooter = new Footer
                //{
                //    //Color = SKColors.Red,
                //    Text = "Left",
                //    PointSize = 20
                //},
                //CenterFooter = new Footer
                //{
                //    Bold = true,
                //    Color = SKColors.Green,
                //    Text = "Center",
                //    PointSize = 20
                //},
                //RightFooter = new Footer
                //{
                //    Color = SKColors.Blue,
                //    Text = "Right",
                //    PointSize = 20
                //},
                HorizontalLines = true,
                //OddRowHorizontalColor = SKColors.AntiqueWhite,
                OddRowVerticalColor = SKColors.AntiqueWhite,
                VerticalLines = true,
                XLabelAlignment = Xamarin.Forms.TextAlignment.Center,
                XLabelColor = SKColors.Black,
                XLabelPointSize = 20,
                YFirstLabelFormat = "{0:F}%",
                YLabelFormat = "{0:F}",
                YLabelAlignment = Xamarin.Forms.TextAlignment.Start,
                YLabelColor = SKColors.Black,
                YLabelPointSize = 20,
                LineColor = SKColors.Red,
                HorizontalLabelPrecision = 1M,
                //BottomGraphValue = .05,
                //TopGraphValue = 0.3,
                DataGroups = new List<DataGroup>
                {
                    new DataGroup
                    {
                        Label = "2013",
                        DataPoints = new List<double>
                        {
                            3.6,0.5,3.2,3.2
                        }
                    },
                    new DataGroup
                    {
                        Label = "2014",
                        DataPoints = new List<double>
                        {
                            -1.1,5.5,5,2.3
                        }
                    },
                    new DataGroup
                    {
                        Label = "2015",
                        DataPoints = new List<double>
                        {
                            3.2,3,1.3,0.1
                        }
                    },
                    new DataGroup
                    {
                        Label = "2016",
                        DataPoints = new List<double>
                        {
                            2,1.9,2.2,2
                        }
                    },
                    new DataGroup
                    {
                        Label = "2017",
                        DataPoints = new List<double>
                        {
                            2.3,2.2,3.2,3.5
                        }
                    },
                    new DataGroup
                    {
                        Label = "2018",
                        DataPoints = new List<double>
                        {
                            2.5,3.5,2.9,1.1
                        }
                    },
                    new DataGroup
                    {
                        Label = "2019",
                        DataPoints = new List<double>
                        {
                            3.1,2,2.1,2.1
                        }
                    },
                    new DataGroup
                    {
                        DataPoints = new List<double>
                        {
                            -4.8
                        }
                    }
                }
            };
            #endregion
        }

        public HorizontalBarGraphViewModel HorizontalBarGraphViewModel { get; set; }
        public LineGraphViewModel LineGraphViewModel { get; set; }
        public BarGraphViewModel BarGraphViewModel { get; set; }
    }
}
