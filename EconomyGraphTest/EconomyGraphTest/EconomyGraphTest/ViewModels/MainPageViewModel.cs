using EconomyGraph.Models;
using EconomyGraph.ViewModels;
using MvvmHelpers;
using Prism.Commands;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace EconomyGraphTest.ViewModels
{
    /// <summary>
    /// Quarterly GDP Values
    /// </summary>
    public class MainPageViewModel : BaseViewModel
    {
        private bool _showHideButton;
        public bool ShowHideButton { get { return _showHideButton; } set { SetProperty(ref _showHideButton, value); } }
        
        private bool _showButtons = true;
        public bool ShowButtons { get { return _showButtons; } set { SetProperty(ref _showButtons, value); } }
        
        private bool _horizontalBarGraph;
        public bool HorizontalBarGraph { get { return _horizontalBarGraph; } set { SetProperty(ref _horizontalBarGraph, value); } }

        private bool _lineGraph;
        public bool LineGraph { get { return _lineGraph; } set { SetProperty(ref _lineGraph, value); } }

        private bool _lineGraphWithFooters;
        public bool LineGraphWithFooters { get { return _lineGraphWithFooters; } set { SetProperty(ref _lineGraphWithFooters, value); } }


        private bool _lineGraphWithMultiLineHeader;
        public bool LineGraphWithMultiLineHeader { get { return _lineGraphWithMultiLineHeader; } set { SetProperty(ref _lineGraphWithMultiLineHeader, value); } }
        
        private bool _barGraph;
        public bool BarGraph { get { return _barGraph; } set { SetProperty(ref _barGraph, value); } }

        private bool _shadedLineGraph;
        public bool ShadedLineGraph { get { return _shadedLineGraph; } set { SetProperty(ref _shadedLineGraph, value); } }

        private bool _shadedBarGraph;
        public bool ShadedBarGraph { get { return _shadedBarGraph; } set { SetProperty(ref _shadedBarGraph, value); } }

        public ICommand ShowGraphCommand { get; }
        public ICommand HideGraphCommand { get; }
        public MainPageViewModel()
        {
            ShowGraphCommand = new DelegateCommand<object>(ShowGraph);
            HideGraphCommand = new DelegateCommand(HideGraph);

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
                    Bold = false,
                    //Color = SKColors.Black,
                    Text = "Quarterly GDP",
                    PointSize = 25,
                    TextAlignment = Xamarin.Forms.TextAlignment.Center
                },
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

            #region LineGraphWithFootersViewModel
            LineGraphWithFootersViewModel = new LineGraphViewModel
            {
                BackgroundColor = SKColors.AliceBlue,
                Title = new Label
                {
                    Bold = false,
                    //Color = SKColors.Black,
                    Text = "Quarterly GDP",
                    PointSize = 25,
                    TextAlignment = Xamarin.Forms.TextAlignment.Center
                },
                LeftFooter = new Footer
                {
                    //Color = SKColors.Red,
                    Text = "Left",
                    PointSize = 20
                },
                CenterFooter = new Footer
                {
                    Bold = true,
                    Color = SKColors.Green,
                    Text = "Center",
                    PointSize = 20
                },
                RightFooter = new Footer
                {
                    Color = SKColors.Blue,
                    Text = "Right",
                    PointSize = 20
                },
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

            #region LineGraphWithFootersViewModel
            LineGraphWithMultiLineHeaderViewModel = new LineGraphViewModel
            {
                BackgroundColor = SKColors.AliceBlue,
                Title = new Label
                {
                    Bold = true,
                    Color = SKColors.DarkGreen,
                    Text = "Line One\nLine Two\nLine Three",
                    PointSize = 20,
                    TextAlignment = Xamarin.Forms.TextAlignment.Center
                },
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
                    //Bold = true,
                    //Color = SKColors.Black,
                    Text = "This is a BAR GRAPH!",
                    PointSize = 25,
                    TextAlignment = Xamarin.Forms.TextAlignment.Center
                },
                HorizontalLines = true,
                OddRowHorizontalColor = SKColors.AntiqueWhite,
                //OddRowVerticalColor = SKColors.AntiqueWhite,
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
                BottomGraphValue = -6,
                TopGraphValue = 6,
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

            #region ShadedLineGraphViewModel
            ShadedLineGraphViewModel = new ShadedLineGraphViewModel
            {
                BackgroundColor = SKColors.AliceBlue,
                Title = new Label
                {
                    //Bold = true,
                    //Color = SKColors.Black,
                    Text = "Yearly GDP",
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
                //OddRowVerticalColor = SKColors.AntiqueWhite,
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
                HorizontalLabelPrecision = 0.5M,
                //BottomGraphValue = .05,
                //TopGraphValue = 0.3,
                DataGroups = new List<DataGroup>
                {
                    new DataGroup
                    {
                        Label = "2000",
                        DataPoints = new List<double>
                        {
                            4.1
                        },
                        EndDates = new List<DateTime>
                        {
                            new DateTime(2000, 12, 31)
                        }
                    },
                    new DataGroup
                    {
                        Label = "2001",
                        DataPoints = new List<double>
                        {
                            1
                        },
                        EndDates = new List<DateTime>
                        {
                            new DateTime(2001, 12, 31),
                        }
                    },
                    new DataGroup
                    {
                        Label = "2002",
                        DataPoints = new List<double>
                        {
                            1.8
                        },
                        EndDates = new List<DateTime>
                        {
                            new DateTime(2002, 12, 31),
                        }
                    },
                    new DataGroup
                    {
                        Label = "2003",
                        DataPoints = new List<double>
                        {
                            2.8
                        },
                        EndDates = new List<DateTime>
                        {
                            new DateTime(2003, 12, 31),
                        }
                    },
                    new DataGroup
                    {
                        Label = "2004",
                        DataPoints = new List<double>
                        {
                            3.8
                        },
                        EndDates = new List<DateTime>
                        {
                            new DateTime(2004, 12, 31),
                        }
                    },
                    new DataGroup
                    {
                        Label = "2005",
                        DataPoints = new List<double>
                        {
                            3.3
                        },
                        EndDates = new List<DateTime>
                        {
                            new DateTime(2005, 12, 31),
                        }
                    },
                    new DataGroup
                    {
                        Label = "2006",
                        DataPoints = new List<double>
                        {
                            2.9
                        },
                        EndDates = new List<DateTime>
                        {
                            new DateTime(2006, 12, 31),
                        }
                    },
                    new DataGroup
                    {
                        Label = "2007",
                        DataPoints = new List<double>
                        {
                            1.9
                        },
                        EndDates = new List<DateTime>
                        {
                            new DateTime(2007, 12, 31),
                        }
                    },
                    new DataGroup
                    {
                        Label = "2008",
                        DataPoints = new List<double>
                        {
                            -0.2
                        },
                        EndDates = new List<DateTime>
                        {
                            new DateTime(2008, 12, 31),
                        }
                    },
                    new DataGroup
                    {
                        Label = "2009",
                        DataPoints = new List<double>
                        {
                            -2.5
                        },
                        EndDates = new List<DateTime>
                        {
                            new DateTime(2009, 12, 31),
                        }
                    },
                },
                //DataGroups = new List<DataGroup>
                //{
                //    new DataGroup
                //    {
                //        Label = "90's",
                //        DataPoints = new List<double>
                //        {
                //            4.8
                //        },
                //        EndDates = new List<DateTime>
                //        {
                //            new DateTime(1999, 12, 31)
                //        }
                //    },
                //    new DataGroup
                //    {
                //        Label = "2000's",
                //        DataPoints = new List<double>
                //        {
                //            4.1, 1, 1.8, 2.8, 3.8, 3.3, 2.9, 1.9, -0.2, -2.5
                //        },
                //        EndDates = new List<DateTime>
                //        {
                //            new DateTime(2000, 12, 31),
                //            new DateTime(2001, 12, 31),
                //            new DateTime(2002, 12, 31),
                //            new DateTime(2003, 12, 31),
                //            new DateTime(2004, 12, 31),
                //            new DateTime(2005, 12, 31),
                //            new DateTime(2006, 12, 31),
                //            new DateTime(2007, 12, 31),
                //            new DateTime(2008, 12, 31),
                //            new DateTime(2009, 12, 31)
                //        }
                //    },
                //    new DataGroup
                //    {
                //        Label = "2010's",
                //        DataPoints = new List<double>
                //        {
                //            2.6, 1.6, 2.2, 1.8, 2.5, 2.9, 1.6, 2.4, 2.9, 2.3
                //        },
                //        EndDates = new List<DateTime>
                //        {
                //            new DateTime(2010, 12, 31),
                //            new DateTime(2011, 12, 31),
                //            new DateTime(2012, 12, 31),
                //            new DateTime(2013, 12, 31),
                //            new DateTime(2014, 12, 31),
                //            new DateTime(2015, 12, 31),
                //            new DateTime(2016, 12, 31),
                //            new DateTime(2017, 12, 31),
                //            new DateTime(2018, 12, 31),
                //            new DateTime(2019, 12, 31)
                //        }
                //    },
                //    new DataGroup
                //    {
                //        Label = "2020",
                //        DataPoints = new List<double>
                //        {
                //            -5.9
                //        },
                //        EndDates = new List<DateTime>
                //        {
                //            new DateTime(2020, 3, 31)
                //        }
                //    }
                //},
                ShadedAreaColor = SKColors.LightGray,
                StartDate = new DateTime(1999, 1, 1)
            };
            #endregion

            #region ShadedBarGraphViewModel
            ShadedBarGraphViewModel = new ShadedBarGraphViewModel
            {
                BackgroundColor = SKColors.AliceBlue,
                Title = new Label
                {
                    //Bold = true,
                    //Color = SKColors.Black,
                    Text = "Yearly GDP",
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
                //OddRowVerticalColor = SKColors.AntiqueWhite,
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
                HorizontalLabelPrecision = 3M,
                //BottomGraphValue = .05,
                //TopGraphValue = 0.3,
                /*
                DataGroups = new List<DataGroup>
                {
                    new DataGroup
                    {
                        Label = "2000",
                        DataPoints = new List<double>
                        {
                            4.1
                        },
                        EndDates = new List<DateTime>
                        {
                            new DateTime(2000, 12, 31)
                        }
                    },
                    new DataGroup
                    {
                        Label = "2001",
                        DataPoints = new List<double>
                        {
                            1
                        },
                        EndDates = new List<DateTime>
                        {
                            new DateTime(2001, 12, 31),
                        }
                    },
                    new DataGroup
                    {
                        Label = "2002",
                        DataPoints = new List<double>
                        {
                            1.8
                        },
                        EndDates = new List<DateTime>
                        {
                            new DateTime(2002, 12, 31),
                        }
                    },
                    new DataGroup
                    {
                        Label = "2003",
                        DataPoints = new List<double>
                        {
                            2.8
                        },
                        EndDates = new List<DateTime>
                        {
                            new DateTime(2003, 12, 31),
                        }
                    },
                    new DataGroup
                    {
                        Label = "2004",
                        DataPoints = new List<double>
                        {
                            3.8
                        },
                        EndDates = new List<DateTime>
                        {
                            new DateTime(2004, 12, 31),
                        }
                    },
                    new DataGroup
                    {
                        Label = "2005",
                        DataPoints = new List<double>
                        {
                            3.3
                        },
                        EndDates = new List<DateTime>
                        {
                            new DateTime(2005, 12, 31),
                        }
                    },
                    new DataGroup
                    {
                        Label = "2006",
                        DataPoints = new List<double>
                        {
                            2.9
                        },
                        EndDates = new List<DateTime>
                        {
                            new DateTime(2006, 12, 31),
                        }
                    },
                    new DataGroup
                    {
                        Label = "2007",
                        DataPoints = new List<double>
                        {
                            1.9
                        },
                        EndDates = new List<DateTime>
                        {
                            new DateTime(2007, 12, 31),
                        }
                    },
                    new DataGroup
                    {
                        Label = "2008",
                        DataPoints = new List<double>
                        {
                            -0.2
                        },
                        EndDates = new List<DateTime>
                        {
                            new DateTime(2008, 12, 31),
                        }
                    },
                    new DataGroup
                    {
                        Label = "2009",
                        DataPoints = new List<double>
                        {
                            -2.5
                        },
                        EndDates = new List<DateTime>
                        {
                            new DateTime(2009, 12, 31),
                        }
                    },
                },
                */
                DataGroups = new List<DataGroup>
                {
                    new DataGroup
                    {
                        Label = "90's",
                        DataPoints = new List<double>
                        {
                            4.8
                        },
                        EndDates = new List<DateTime>
                        {
                            new DateTime(1999, 12, 31)
                        }
                    },
                    new DataGroup
                    {
                        Label = "2000's",
                        DataPoints = new List<double>
                        {
                            4.1, 1, 1.8, 2.8, 3.8, 3.3, 2.9, 1.9, -0.2, -2.5
                        },
                        EndDates = new List<DateTime>
                        {
                            new DateTime(2000, 12, 31),
                            new DateTime(2001, 12, 31),
                            new DateTime(2002, 12, 31),
                            new DateTime(2003, 12, 31),
                            new DateTime(2004, 12, 31),
                            new DateTime(2005, 12, 31),
                            new DateTime(2006, 12, 31),
                            new DateTime(2007, 12, 31),
                            new DateTime(2008, 12, 31),
                            new DateTime(2009, 12, 31)
                        }
                    },
                    new DataGroup
                    {
                        Label = "2010's",
                        DataPoints = new List<double>
                        {
                            2.6, 1.6, 2.2, 1.8, 2.5, 2.9, 1.6, 2.4, 2.9, 2.3
                        },
                        EndDates = new List<DateTime>
                        {
                            new DateTime(2010, 12, 31),
                            new DateTime(2011, 12, 31),
                            new DateTime(2012, 12, 31),
                            new DateTime(2013, 12, 31),
                            new DateTime(2014, 12, 31),
                            new DateTime(2015, 12, 31),
                            new DateTime(2016, 12, 31),
                            new DateTime(2017, 12, 31),
                            new DateTime(2018, 12, 31),
                            new DateTime(2019, 12, 31)
                        }
                    },
                    new DataGroup
                    {
                        //Label = "2020",
                        DataPoints = new List<double>
                        {
                            -5.9
                        },
                        EndDates = new List<DateTime>
                        {
                            new DateTime(2020, 3, 31)
                        }
                    }
                },
                ShadedAreaColor = SKColors.LightGray,
                StartDate = new DateTime(1999, 1, 1)
            };
            #endregion
        }

        public HorizontalBarGraphViewModel HorizontalBarGraphViewModel { get; set; }
        public LineGraphViewModel LineGraphViewModel { get; set; }
        public LineGraphViewModel LineGraphWithFootersViewModel { get; set; }
        public LineGraphViewModel LineGraphWithMultiLineHeaderViewModel { get; set; }
        public BarGraphViewModel BarGraphViewModel { get; set; }
        public ShadedLineGraphViewModel ShadedLineGraphViewModel { get; set; }
        public ShadedBarGraphViewModel ShadedBarGraphViewModel { get; set; }

        public void ShowGraph(object buttonParameter)
        {
            ShowHideButton = true;
            switch ((string)buttonParameter)
            {
                case "HorizontalBarGraph":
                    HorizontalBarGraph = true;
                    break;
                case "LineGraph":
                    LineGraph = true;
                    break;
                case "LineGraphWithFooters":
                    LineGraphWithFooters = true;
                    break;
                case "LineGraphWithMultiLineHeader":
                    LineGraphWithMultiLineHeader = true;
                    break;
                case "BarGraph":
                    BarGraph = true;
                    break;
                case "ShadedLineGraph":
                    ShadedLineGraph = true;
                    break;
                case "ShadedBarGraph":
                    ShadedBarGraph = true;
                    break;
            }
            ShowButtons = false;
        }

        public void HideGraph()
        {
            ShowHideButton = false;
            ShowButtons = true; ;

            HorizontalBarGraph = false;
            LineGraph = false;
            LineGraphWithFooters = false;
            LineGraphWithMultiLineHeader = false;
            BarGraph = false;
            ShadedLineGraph = false;
            ShadedBarGraph = false;
        }
    }
}
