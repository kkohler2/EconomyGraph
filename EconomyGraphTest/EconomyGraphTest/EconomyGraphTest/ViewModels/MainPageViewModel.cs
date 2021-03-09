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

        private bool _lineGraphLeftLabel;
        public bool LineGraphLeftLabel { get { return _lineGraphLeftLabel; } set { SetProperty(ref _lineGraphLeftLabel, value); } }

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

        private bool _pointGraph;
        public bool PointGraph { get { return _pointGraph; } set { SetProperty(ref _pointGraph, value); } }

        private bool _multiLineGraph;
        public bool MultiLineGraph { get { return _multiLineGraph; } set { SetProperty(ref _multiLineGraph, value); } }
        
        private bool _shadedMultiLineGraph;
        public bool ShadedMultiLineGraph { get { return _shadedMultiLineGraph; } set { SetProperty(ref _shadedMultiLineGraph, value); } }
        
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
            HorizontalBarGraphViewModel.DataPoints.Add(new HorizontalBarGraphDataPoint
            {
                Color = SKColors.Blue,
                TextColor = SKColors.Gray,
                Label = "Oppose",
                Bold = true,
                PointSize = 20,
                Value = 64
            });
            HorizontalBarGraphViewModel.DataPoints.Add(new HorizontalBarGraphDataPoint
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
                HorizontalLinesStartAtEdge = true,
                HorizontalLineColor = SKColors.Black,
                //OddRowHorizontalColor = SKColors.AntiqueWhite,
                OddRowVerticalColor = SKColors.AntiqueWhite,
                //VerticalLeftAxisColor = SKColors.Black,
                VerticalLineColor = SKColors.Black,
                XLabelAlignment = Xamarin.Forms.TextAlignment.Center,
                XLabelColor = SKColors.Black,
                XLabelPointSize = 20,
                XLabelRotation = LabelRotation.Angle,
                YFirstLabelFormat = "{0:F}%",
                YLabelFormat = "{0:F}",
                YLabelAlignment = Xamarin.Forms.TextAlignment.Start,
                YLabelColor = SKColors.Black,
                YLabelPointSize = 20,
                LineColor = SKColors.Red,
                VerticalLabelPrecision = 1M,
                //BottomGraphValue = .05,
                //TopGraphValue = 0.3,
                DataGroups = new List<DataGroup>
                {
                    new DataGroup
                    {
                        Label = "2013\nLine 2!",
                        DataPoints = new List<IDataPoint>
                        {
                            new LineDataPoint{Value=3.6, IndicatorLine = true},
                            new LineDataPoint
                            {
                                Value=0.5,
                                CircleType = CircleType.Donut,
                                CircleRadius = 10,
                                IndicatorLine = true
                            },
                            new LineDataPoint{Value=3.2, IndicatorLine = true },
                            new LineDataPoint{Value=3.2, IndicatorLine = true}
                        }
                    },
                    new DataGroup
                    {
                        Label = "2014",
                        DataPoints = new List<IDataPoint>
                        {
                            new LineDataPoint{Value=-1.1},
                            new LineDataPoint{Value=5.5},
                            new LineDataPoint{Value=2.3},
                        }
                    },
                    new DataGroup
                    {
                        Label = "2015",
                        DataPoints = new List<IDataPoint>
                        {
                            new LineDataPoint
                            {
                                Value=3.2,
                                Label = new DataPointLabel
                                {
                                    Color = SKColors.Blue,
                                    Bold = true,
                                    PointSize = 15,
                                    Text = "Q1",
                                    XOffSet = -5,
                                    YOffSet = -5
                                }
                            },
                            new LineDataPoint{Value=3},
                            new LineDataPoint{Value=1.3},
                            new LineDataPoint{Value=0.1}
                        }
                    },
                    new DataGroup
                    {
                        Label = "2016",
                        DataPoints = new List<IDataPoint>
                        {
                            new LineDataPoint{Value=2},
                            new LineDataPoint{Value=1.9},
                            new LineDataPoint{Value=2.2},
                            new LineDataPoint{Value=2}
                        }
                    },
                    new DataGroup
                    {
                        Label = "2017",
                        DataPoints = new List<IDataPoint>
                        {
                            new LineDataPoint{Value=2.3},
                            new LineDataPoint{Value=2.2},
                            new LineDataPoint{Value=3.2},
                            new LineDataPoint{Value=3.5}
                        }
                    },
                    new DataGroup
                    {
                        Label = "2018",
                        DataPoints = new List<IDataPoint>
                        {
                            new LineDataPoint{Value=2.5},
                            new LineDataPoint{Value=3.5},
                            new LineDataPoint{Value=2.9},
                            new LineDataPoint{Value=1.1}
                        }
                    },
                    new DataGroup
                    {
                        Label = "2019",
                        DataPoints = new List<IDataPoint>
                        {
                            new LineDataPoint{Value=3.1},
                            new LineDataPoint{Value=2},
                            new LineDataPoint{Value=2.1},
                            new LineDataPoint{Value=2.1}
                        }
                    },
                    new DataGroup
                    {
                        DataPoints = new List<IDataPoint>
                        {
                            new LineDataPoint{Value=-4.8}
                        }
                    }
                }
            };
            #endregion

            #region LineGraphLeftLabelViewModel
            LineGraphLeftLabelViewModel = new LineGraphViewModel
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
                LeftLabel = new Label
                {
                    Bold = false,
                    Color = SKColors.DarkBlue,
                    Text = "Left side text",
                    PointSize = 25,
                    TextAlignment = Xamarin.Forms.TextAlignment.Center
                },
                HorizontalLinesStartAtEdge = true,
                HorizontalLineColor = SKColors.Black,
                OddRowHorizontalColor = SKColors.AntiqueWhite,
                VerticalLineColor = SKColors.Black,
                XLabelAlignment = Xamarin.Forms.TextAlignment.Center,
                XLabelColor = SKColors.Black,
                XLabelPointSize = 20,
                YFirstLabelFormat = "{0:F}%",
                YLabelFormat = "{0:F}",
                YLabelAlignment = Xamarin.Forms.TextAlignment.Start,
                YLabelColor = SKColors.Black,
                YLabelPointSize = 20,
                LineColor = SKColors.Red,
                VerticalLabelPrecision = 1M,
                //BottomGraphValue = .05,
                //TopGraphValue = 0.3,
                DataGroups = new List<DataGroup>
                {
                    new DataGroup
                    {
                        Label = "2013",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=3.6},
                            new DataPoint{Value=0.5},
                            new DataPoint{Value=3.2},
                            new DataPoint{Value=3.2}
                        }
                    },
                    new DataGroup
                    {
                        Label = "2014",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=-1.1},
                            new DataPoint{Value=5.5},
                            new DataPoint{Value=5},
                            new DataPoint{Value=2.3}
                        }
                    },
                    new DataGroup
                    {
                        Label = "2015",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=3.2},
                            new DataPoint{Value=3},
                            new DataPoint{Value=1.3},
                            new DataPoint{Value=0.1}
                        }
                    },
                    new DataGroup
                    {
                        Label = "2016",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=2},
                            new DataPoint{Value=1.9},
                            new DataPoint{Value=2.2},
                            new DataPoint{Value=2},
                        }
                    },
                    new DataGroup
                    {
                        Label = "2017",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=2.3},
                            new DataPoint{Value=2.2},
                            new DataPoint{Value=3.2},
                            new DataPoint{Value=3.5}
                        }
                    },
                    new DataGroup
                    {
                        Label = "2018",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=2.5},
                            new DataPoint{Value=3.5},
                            new DataPoint{Value=2.9},
                            new DataPoint{Value=1.1}
                        }
                    },
                    new DataGroup
                    {
                        Label = "2019",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=3.1},
                            new DataPoint{Value=2},
                            new DataPoint{Value=2.1},
                            new DataPoint{Value=2.1}
                        }
                    },
                    new DataGroup
                    {
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=-4.8}
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
                HorizontalLinesStartAtEdge = true,
                HorizontalLineColor = SKColors.Black,
                //OddRowHorizontalColor = SKColors.AntiqueWhite,
                OddRowVerticalColor = SKColors.AntiqueWhite,
                VerticalLineColor = SKColors.Black,
                XLabelAlignment = Xamarin.Forms.TextAlignment.Center,
                XLabelColor = SKColors.Black,
                XLabelPointSize = 20,
                YFirstLabelFormat = "{0:F}%",
                YLabelFormat = "{0:F}",
                YLabelAlignment = Xamarin.Forms.TextAlignment.Start,
                YLabelColor = SKColors.Black,
                YLabelPointSize = 20,
                LineColor = SKColors.Red,
                VerticalLabelPrecision = 1M,
                //BottomGraphValue = .05,
                //TopGraphValue = 0.3,
                DataGroups = new List<DataGroup>
                {
                    new DataGroup
                    {
                        Label = "2013",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=3.6},
                            new DataPoint{Value=0.5},
                            new DataPoint{Value=3.2},
                            new DataPoint{Value=3.2}
                        }
                    },
                    new DataGroup
                    {
                        Label = "2014",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=-1.1},
                            new DataPoint{Value=5.5},
                            new DataPoint{Value=5},
                            new DataPoint{Value=2.3}
                        }
                    },
                    new DataGroup
                    {
                        Label = "2015",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=3.2},
                            new DataPoint{Value=3},
                            new DataPoint{Value=1.3},
                            new DataPoint{Value=0.1}
                        }
                    },
                    new DataGroup
                    {
                        Label = "2016",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=2},
                            new DataPoint{Value=1.9},
                            new DataPoint{Value=2.2},
                            new DataPoint{Value=2},
                        }
                    },
                    new DataGroup
                    {
                        Label = "2017",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=2.3},
                            new DataPoint{Value=2.2},
                            new DataPoint{Value=3.2},
                            new DataPoint{Value=3.5}
                        }
                    },
                    new DataGroup
                    {
                        Label = "2018",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=2.5},
                            new DataPoint{Value=3.5},
                            new DataPoint{Value=2.9},
                            new DataPoint{Value=1.1}
                        }
                    },
                    new DataGroup
                    {
                        Label = "2019",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=3.1},
                            new DataPoint{Value=2},
                            new DataPoint{Value=2.1},
                            new DataPoint{Value=2.1}
                        }
                    },
                    new DataGroup
                    {
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=-4.8}
                        }
                    }
                }
            };
            #endregion

            #region LineGraphWithMultiLineHeaderViewModel
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
                HorizontalLinesStartAtEdge = true,
                HorizontalLineColor = SKColors.Black,
                //OddRowHorizontalColor = SKColors.AntiqueWhite,
                OddRowVerticalColor = SKColors.AntiqueWhite,
                VerticalLineColor = SKColors.Black,
                XLabelAlignment = Xamarin.Forms.TextAlignment.Center,
                XLabelColor = SKColors.Black,
                XLabelPointSize = 20,
                YFirstLabelFormat = "{0:F}%",
                YLabelFormat = "{0:F}",
                YLabelAlignment = Xamarin.Forms.TextAlignment.Start,
                YLabelColor = SKColors.Black,
                YLabelPointSize = 20,
                LineColor = SKColors.Red,
                VerticalLabelPrecision = 1M,
                //BottomGraphValue = .05,
                //TopGraphValue = 0.3,
                DataGroups = new List<DataGroup>
                {
                    new DataGroup
                    {
                        Label = "2013",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=3.6},
                            new DataPoint{Value=0.5},
                            new DataPoint{Value=3.2},
                            new DataPoint{Value=3.2}
                        }
                    },
                    new DataGroup
                    {
                        Label = "2014",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=-1.1},
                            new DataPoint{Value=5.5},
                            new DataPoint{Value=5},
                            new DataPoint{Value=2.3}
                        }
                    },
                    new DataGroup
                    {
                        Label = "2015",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=3.2},
                            new DataPoint{Value=3},
                            new DataPoint{Value=1.3},
                            new DataPoint{Value=0.1}
                        }
                    },
                    new DataGroup
                    {
                        Label = "2016",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=2},
                            new DataPoint{Value=1.9},
                            new DataPoint{Value=2.2},
                            new DataPoint{Value=2},
                        }
                    },
                    new DataGroup
                    {
                        Label = "2017",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=2.3},
                            new DataPoint{Value=2.2},
                            new DataPoint{Value=3.2},
                            new DataPoint{Value=3.5}
                        }
                    },
                    new DataGroup
                    {
                        Label = "2018",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=2.5},
                            new DataPoint{Value=3.5},
                            new DataPoint{Value=2.9},
                            new DataPoint{Value=1.1}
                        }
                    },
                    new DataGroup
                    {
                        Label = "2019",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=3.1},
                            new DataPoint{Value=2},
                            new DataPoint{Value=2.1},
                            new DataPoint{Value=2.1}
                        }
                    },
                    new DataGroup
                    {
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=-4.8}
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
                HorizontalLinesStartAtEdge = true,
                HorizontalLineColor = SKColors.Black,
                OddRowHorizontalColor = SKColors.AntiqueWhite,
                //OddRowVerticalColor = SKColors.AntiqueWhite,
                VerticalLineColor = SKColors.Black,
                XLabelAlignment = Xamarin.Forms.TextAlignment.Center,
                XLabelColor = SKColors.Black,
                XLabelPointSize = 20,
                YFirstLabelFormat = "{0:F}%",
                YLabelFormat = "{0:F}",
                YLabelAlignment = Xamarin.Forms.TextAlignment.Start,
                YLabelColor = SKColors.Black,
                YLabelPointSize = 20,
                LineColor = SKColors.Red,
                VerticalLabelPrecision = 1M,
                BottomGraphValue = -6,
                TopGraphValue = 6,
                DataGroups = new List<DataGroup>
                {
                    new DataGroup
                    {
                        Label = "2013",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=3.6},
                            new DataPoint{Value=0.5},
                            new DataPoint{Value=3.2},
                            new DataPoint{Value=3.2,IndicatorLine = true }
                        }
                    },
                    new DataGroup
                    {
                        Label = "2014",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint
                            {
                                Value=-1.1,
                                Label = new DataPointLabel
                                {
                                    Color = SKColors.Green,
                                    Bold = true,
                                    PointSize = 15,
                                    Text = "Q1",
                                    XOffSet = 3,
                                    YOffSet = 15
                                }
                            },
                            new DataPoint{Value=5.5},
                            new DataPoint{Value=5},
                            new DataPoint{Value=2.3}
                        }
                    },
                    new DataGroup
                    {
                        Label = "2015",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint
                            {
                                Value=3.2,
                                Label = new DataPointLabel
                                {
                                    Color = SKColors.Green,
                                    Bold = true,
                                    PointSize = 15,
                                    Text = "Q1",
                                    XOffSet = 3,
                                    YOffSet = -5
                                }
                            },
                            new DataPoint{Value=3},
                            new DataPoint{Value=1.3},
                            new DataPoint{Value=0.1}
                        }
                    },
                    new DataGroup
                    {
                        Label = "2016",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=2},
                            new DataPoint{Value=1.9},
                            new DataPoint{Value=2.2},
                            new DataPoint{Value=2},
                        }
                    },
                    new DataGroup
                    {
                        Label = "2017",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=2.3},
                            new DataPoint{Value=2.2},
                            new DataPoint{Value=3.2},
                            new DataPoint{Value=3.5}
                        }
                    },
                    new DataGroup
                    {
                        Label = "2018",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=2.5},
                            new DataPoint{Value=3.5},
                            new DataPoint{Value=2.9},
                            new DataPoint{Value=1.1}
                        }
                    },
                    new DataGroup
                    {
                        Label = "2019",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=3.1},
                            new DataPoint{Value=2},
                            new DataPoint{Value=2.1},
                            new DataPoint{Value=2.1}
                        }
                    },
                    new DataGroup
                    {
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value=-4.8}
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
                    Text = "Monthly Housing Starts",
                    PointSize = 25,
                    TextAlignment = Xamarin.Forms.TextAlignment.Center
                },
                LeftLabel = new Label
                {
                    Bold = false,
                    Color = SKColors.Black,
                    Text = "Thousands of Units",
                    PointSize = 15,
                    TextAlignment = Xamarin.Forms.TextAlignment.Center
                },
                HorizontalLinesStartAtEdge = true,
                HorizontalLineColor = SKColors.LightGray,
                //OddRowHorizontalColor = SKColors.AntiqueWhite,
                //OddRowVerticalColor = SKColors.AntiqueWhite,
                //VerticalLineColor = SKColors.Black,
                XLabelAlignment = Xamarin.Forms.TextAlignment.Start,
                XLabelColor = SKColors.Black,
                XLabelPointSize = 15,
                YFirstLabelFormat = "{0}",
                YLabelFormat = "{0}",
                YLabelAlignment = Xamarin.Forms.TextAlignment.Start,
                YLabelColor = SKColors.Black,
                YLabelPointSize = 15,
                LineColor = SKColors.Blue,
                VerticalLabelPrecision = 100M,
                BottomGraphValue = 800,
                TopGraphValue = 1900,
                DataGroups = new List<DataGroup>
                {
                    new DataGroup
                    {
                        Label = null,
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value = 1460,EndDate=new DateTime(1960,1,29)},
                            new DataPoint{Value = 1503,EndDate=new DateTime(1960,2,29)},
                            new DataPoint{Value = 1109,EndDate=new DateTime(1960,3,29)},
                            new DataPoint{Value = 1289,EndDate=new DateTime(1960,4,29)},
                            new DataPoint{Value = 1271,EndDate=new DateTime(1960,5,29)},
                            new DataPoint{Value = 1247,EndDate=new DateTime(1960,6,29)},
                            new DataPoint{Value = 1197,EndDate=new DateTime(1960,7,29)},
                            new DataPoint{Value = 1344,EndDate=new DateTime(1960,8,29)},
                            new DataPoint{Value = 1097,EndDate=new DateTime(1960,9,29)},
                            new DataPoint{Value = 1246,EndDate=new DateTime(1960,10,29)},
                            new DataPoint{Value = 1246,EndDate=new DateTime(1960,11,29)},
                            new DataPoint{Value = 1063,EndDate=new DateTime(1960,12,29)}
                        }
                    },
                    new DataGroup
                    {
                        Label = "1961",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value = 1183,EndDate=new DateTime(1961,1,28),IndicatorLine=true},
                            new DataPoint{Value = 1226,EndDate=new DateTime(1961,2,28)},
                            new DataPoint{Value = 1312,EndDate=new DateTime(1961,3,28)},
                            new DataPoint{Value = 1166,EndDate=new DateTime(1961,4,28)},
                            new DataPoint{Value = 1228,EndDate=new DateTime(1961,5,28)},
                            new DataPoint{Value = 1382,EndDate=new DateTime(1961,6,28)},
                            new DataPoint{Value = 1335,EndDate=new DateTime(1961,7,28)},
                            new DataPoint{Value = 1312,EndDate=new DateTime(1961,8,28)},
                            new DataPoint{Value = 1429,EndDate=new DateTime(1961,9,28)},
                            new DataPoint{Value = 1415,EndDate=new DateTime(1961,10,28)},
                            new DataPoint{Value = 1385,EndDate=new DateTime(1961,11,28)},
                            new DataPoint{Value = 1365,EndDate=new DateTime(1961,12,28)}
                        }
                    },
                    new DataGroup
                    {
                        Label = "1962",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value = 1361,EndDate=new DateTime(1962,1,28),IndicatorLine=true},
                            new DataPoint{Value = 1278,EndDate=new DateTime(1962,2,28)},
                            new DataPoint{Value = 1443,EndDate=new DateTime(1962,3,28)},
                            new DataPoint{Value = 1524,EndDate=new DateTime(1962,4,28)},
                            new DataPoint{Value = 1483,EndDate=new DateTime(1962,5,28)},
                            new DataPoint{Value = 1404,EndDate=new DateTime(1962,6,28)},
                            new DataPoint{Value = 1450,EndDate=new DateTime(1962,7,28)},
                            new DataPoint{Value = 1517,EndDate=new DateTime(1962,8,28)},
                            new DataPoint{Value = 1324,EndDate=new DateTime(1962,9,28)},
                            new DataPoint{Value = 1533,EndDate=new DateTime(1962,10,28)},
                            new DataPoint{Value = 1622,EndDate=new DateTime(1962,11,28)},
                            new DataPoint{Value = 1564,EndDate=new DateTime(1962,12,28)}
                        }
                    },
                    new DataGroup
                    {
                        Label = "1963",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value = 1244,EndDate=new DateTime(1963,1,28),IndicatorLine=true},
                            new DataPoint{Value = 1456,EndDate=new DateTime(1963,2,28)},
                            new DataPoint{Value = 1534,EndDate=new DateTime(1963,3,28)},
                            new DataPoint{Value = 1689,EndDate=new DateTime(1963,4,28)},
                            new DataPoint{Value = 1641,EndDate=new DateTime(1963,5,28)},
                            new DataPoint{Value = 1588,EndDate=new DateTime(1963,6,28)},
                            new DataPoint{Value = 1614,EndDate=new DateTime(1963,7,28)},
                            new DataPoint{Value = 1639,EndDate=new DateTime(1963,8,28)},
                            new DataPoint{Value = 1763,EndDate=new DateTime(1963,9,28)},
                            new DataPoint{Value = 1779,EndDate=new DateTime(1963,10,28)},
                            new DataPoint{Value = 1622,EndDate=new DateTime(1963,11,28)},
                            new DataPoint{Value = 1491,EndDate=new DateTime(1963,12,28)}
                        }
                    },
                    new DataGroup
                    {
                        Label = "1964",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value = 1603,EndDate=new DateTime(1964,1,29),IndicatorLine=true},
                            new DataPoint{Value = 1820,EndDate=new DateTime(1964,2,29)},
                            new DataPoint{Value = 1517,EndDate=new DateTime(1964,3,29)},
                            new DataPoint{Value = 1448,EndDate=new DateTime(1964,4,29)},
                            new DataPoint{Value = 1467,EndDate=new DateTime(1964,5,29)},
                            new DataPoint{Value = 1550,EndDate=new DateTime(1964,6,29)},
                            new DataPoint{Value = 1562,EndDate=new DateTime(1964,7,29)},
                            new DataPoint{Value = 1569,EndDate=new DateTime(1964,8,29)},
                            new DataPoint{Value = 1455,EndDate=new DateTime(1964,9,29)},
                            new DataPoint{Value = 1524,EndDate=new DateTime(1964,10,29)},
                            new DataPoint{Value = 1486,EndDate=new DateTime(1964,11,29)},
                            new DataPoint{Value = 1484,EndDate=new DateTime(1964,12,29)},
                        }
                    },
                    new DataGroup
                    {
                        Label = "1965",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value = 1361,EndDate=new DateTime(1965,1,28),IndicatorLine=true},
                            new DataPoint{Value = 1433,EndDate=new DateTime(1965,2,28)},
                            new DataPoint{Value = 1423,EndDate=new DateTime(1965,3,28)},
                            new DataPoint{Value = 1438,EndDate=new DateTime(1965,4,28)},
                            new DataPoint{Value = 1478,EndDate=new DateTime(1965,5,28)},
                            new DataPoint{Value = 1488,EndDate=new DateTime(1965,6,28)},
                            new DataPoint{Value = 1529,EndDate=new DateTime(1965,7,28)},
                            new DataPoint{Value = 1432,EndDate=new DateTime(1965,8,28)},
                            new DataPoint{Value = 1482,EndDate=new DateTime(1965,9,28)},
                            new DataPoint{Value = 1452,EndDate=new DateTime(1965,10,28)},
                            new DataPoint{Value = 1460,EndDate=new DateTime(1965,11,28)},
                            new DataPoint{Value = 1656,EndDate=new DateTime(1965,12,28)}
                        }
                    },
                    new DataGroup
                    {
                        Label = "1966",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value = 1370,EndDate=new DateTime(1966,1,28),IndicatorLine=true},
                            new DataPoint{Value = 1378,EndDate=new DateTime(1966,2,28)},
                            new DataPoint{Value = 1394,EndDate=new DateTime(1966,3,28)},
                            new DataPoint{Value = 1352,EndDate=new DateTime(1966,4,28)},
                            new DataPoint{Value = 1265,EndDate=new DateTime(1966,5,28)},
                            new DataPoint{Value = 1194,EndDate=new DateTime(1966,6,28)},
                            new DataPoint{Value = 1086,EndDate=new DateTime(1966,7,28)},
                            new DataPoint{Value = 1119,EndDate=new DateTime(1966,8,28)},
                            new DataPoint{Value = 1046,EndDate=new DateTime(1966,9,28)},
                            new DataPoint{Value = 843,EndDate=new DateTime(1966,10,28)},
                            new DataPoint{Value = 961,EndDate=new DateTime(1966,11,28)},
                            new DataPoint{Value = 990,EndDate=new DateTime(1966,12,28)}
                        }
                    },
                    new DataGroup
                    {
                        Label = "1967",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value = 1067,EndDate=new DateTime(1967,1,28),IndicatorLine=true},
                            new DataPoint{Value = 1123,EndDate=new DateTime(1967,2,28)},
                            new DataPoint{Value = 1056,EndDate=new DateTime(1967,3,28)},
                            new DataPoint{Value = 1091,EndDate=new DateTime(1967,4,28)},
                            new DataPoint{Value = 1304,EndDate=new DateTime(1967,5,28)},
                            new DataPoint{Value = 1248,EndDate=new DateTime(1967,6,28)},
                            new DataPoint{Value = 1364,EndDate=new DateTime(1967,7,28)},
                            new DataPoint{Value = 1407,EndDate=new DateTime(1967,8,28)},
                            new DataPoint{Value = 1421,EndDate=new DateTime(1967,9,28)},
                            new DataPoint{Value = 1491,EndDate=new DateTime(1967,10,28)},
                            new DataPoint{Value = 1538,EndDate=new DateTime(1967,11,28)},
                            new DataPoint{Value = 1308,EndDate=new DateTime(1967,12,28)}
                        }
                    },
                    new DataGroup
                    {
                        Label = "1968",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value = 1380,EndDate=new DateTime(1968,1,29),IndicatorLine=true},
                            new DataPoint{Value = 1520,EndDate=new DateTime(1968,2,29)},
                            new DataPoint{Value = 1466,EndDate=new DateTime(1968,3,29)},
                            new DataPoint{Value = 1554,EndDate=new DateTime(1968,4,29)},
                            new DataPoint{Value = 1408,EndDate=new DateTime(1968,5,29)},
                            new DataPoint{Value = 1405,EndDate=new DateTime(1968,6,29)},
                            new DataPoint{Value = 1512,EndDate=new DateTime(1968,7,29)},
                            new DataPoint{Value = 1495,EndDate=new DateTime(1968,8,29)},
                            new DataPoint{Value = 1556,EndDate=new DateTime(1968,9,29)},
                            new DataPoint{Value = 1569,EndDate=new DateTime(1968,10,29)},
                            new DataPoint{Value = 1630,EndDate=new DateTime(1968,11,29)},
                            new DataPoint{Value = 1548,EndDate=new DateTime(1968,12,29)}
                        }
                    },
                    new DataGroup
                    {
                        Label = "1969",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value = 1769,EndDate=new DateTime(1969,1,28),IndicatorLine=true},
                            new DataPoint{Value = 1705,EndDate=new DateTime(1969,2,28)},
                            new DataPoint{Value = 1561,EndDate=new DateTime(1969,3,28)},
                            new DataPoint{Value = 1524,EndDate=new DateTime(1969,4,28)},
                            new DataPoint{Value = 1583,EndDate=new DateTime(1969,5,28)},
                            new DataPoint{Value = 1528,EndDate=new DateTime(1969,6,28)},
                            new DataPoint{Value = 1368,EndDate=new DateTime(1969,7,28)},
                            new DataPoint{Value = 1358,EndDate=new DateTime(1969,8,28)},
                            new DataPoint{Value = 1507,EndDate=new DateTime(1969,9,28)},
                            new DataPoint{Value = 1381,EndDate=new DateTime(1969,10,28)},
                            new DataPoint{Value = 1229,EndDate=new DateTime(1969,11,28)},
                            new DataPoint{Value = 1327,EndDate=new DateTime(1969,12,28)}
                        }
                    },
                    new DataGroup
                    {
                        Label = "1970",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint{Value = 1085,EndDate=new DateTime(1970,1,28),IndicatorLine=true},
                            new DataPoint{Value = 1305,EndDate=new DateTime(1970,2,28)},
                            new DataPoint{Value = 1319,EndDate=new DateTime(1970,3,28)},
                            new DataPoint{Value = 1264,EndDate=new DateTime(1970,4,28)},
                            new DataPoint{Value = 1290,EndDate=new DateTime(1970,5,28)},
                            new DataPoint{Value = 1385,EndDate=new DateTime(1970,6,28)}
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
                StartDate = new DateTime(1959, 1, 1)
            };
            #endregion

            #region ShadedLineGraphViewModelYearlyGDP
            ShadedLineGraphViewModelYearlyGDP = new ShadedLineGraphViewModel
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
                HorizontalLinesStartAtEdge = true,
                HorizontalLineColor = SKColors.Black,
                //OddRowHorizontalColor = SKColors.AntiqueWhite,
                //OddRowVerticalColor = SKColors.AntiqueWhite,
                VerticalLineColor = SKColors.Black,
                XLabelAlignment = Xamarin.Forms.TextAlignment.Center,
                XLabelColor = SKColors.Black,
                XLabelPointSize = 20,
                YFirstLabelFormat = "{0:F}%",
                YLabelFormat = "{0:F}",
                YLabelAlignment = Xamarin.Forms.TextAlignment.Start,
                YLabelColor = SKColors.Black,
                YLabelPointSize = 20,
                LineColor = SKColors.Red,
                VerticalLabelPrecision = 0.5M,
                //BottomGraphValue = .05,
                //TopGraphValue = 0.3,
                DataGroups = new List<DataGroup>
                {
                    new DataGroup
                    {
                        Label = "2000",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint
                            {
                                Value=4.1,
                                EndDate = new DateTime(2000, 12, 31)
                            }
                        }
                    },
                    new DataGroup
                    {
                        Label = "2001",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint
                            {
                                Value=1,
                                EndDate = new DateTime(2001, 12, 31)
                            }
                        }
                    },
                    new DataGroup
                    {
                        Label = "2002",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint
                            {
                                Value=1.8,
                                EndDate = new DateTime(2002, 12, 31)
                            }
                        }
                    },
                    new DataGroup
                    {
                        Label = "2003",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint
                            {
                                Value=2.8,
                                EndDate = new DateTime(2003, 12, 31)
                            }
                        }
                    },
                    new DataGroup
                    {
                        Label = "2004",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint
                            {
                                Value=3.8,
                                EndDate = new DateTime(2004, 12, 31)
                            }
                        }
                    },
                    new DataGroup
                    {
                        Label = "2005",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint
                            {
                                Value=3.3,
                                EndDate = new DateTime(2005, 12, 31)
                            }
                        }
                    },
                    new DataGroup
                    {
                        Label = "2006",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint
                            {
                                Value=2.9,
                                EndDate = new DateTime(2006, 12, 31)
                            }
                        }
                    },
                    new DataGroup
                    {
                        Label = "2007",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint
                            {
                                Value=1.9,
                                EndDate = new DateTime(2007, 12, 31)
                            }
                        }
                    },
                    new DataGroup
                    {
                        Label = "2008",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint
                            {
                                Value=-0.2,
                                EndDate = new DateTime(2008, 12, 31)
                            }
                        }
                    },
                    new DataGroup
                    {
                        Label = "2009",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint
                            {
                                Value=-2.5,
                                EndDate = new DateTime(2009, 12, 31)
                            }
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
                VerticalLeftAxisColor = SKColors.Red,
                HorizontalBottomLineColor = SKColors.Red,
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
                HorizontalLinesStartAtEdge = false,
                HorizontalLineColor = SKColors.Black,
                //OddRowHorizontalColor = SKColors.AntiqueWhite,
                //OddRowVerticalColor = SKColors.AntiqueWhite,
                VerticalLineColor = SKColors.Black,
                XLabelAlignment = Xamarin.Forms.TextAlignment.Center,
                XLabelColor = SKColors.Black,
                XLabelPointSize = 20,
                YFirstLabelFormat = "{0:F}%",
                YLabelFormat = "{0:F}",
                YLabelAlignment = Xamarin.Forms.TextAlignment.Start,
                YLabelColor = SKColors.Black,
                YLabelPointSize = 20,
                LineColor = SKColors.Red,
                VerticalLabelPrecision = 3M,
                //BottomGraphValue = .05,
                //TopGraphValue = 0.3,
                DataGroups = new List<DataGroup>
                {
                    new DataGroup
                    {
                        Label = "90's",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint
                            {
                                Value=4.8,
                                EndDate=new DateTime(1999, 12, 31)
                            }
                        }
                    },
                    new DataGroup
                    {
                        Label = "2000's",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint
                            {
                                Value=4.1,
                                EndDate=new DateTime(2000, 12, 31)
                            },
                            new DataPoint
                            {
                                Value=1,
                                EndDate=new DateTime(2001, 12, 31)
                            },
                            new DataPoint
                            {
                                Value=1.8,
                                EndDate=new DateTime(2002, 12, 31)
                            },
                            new DataPoint
                            {
                                Value=2.8,
                                EndDate=new DateTime(2003, 12, 31)
                            },
                            new DataPoint
                            {
                                Value=3.8,
                                EndDate=new DateTime(2004, 12, 31)
                            },
                            new DataPoint
                            {
                                Value=3.3,
                                EndDate=new DateTime(2005, 12, 31),
                                Color = SKColors.Green,
                                Label = new DataPointLabel
                                {
                                    Color = SKColors.Blue,
                                    PointSize = 20,
                                    Text = "2005",
                                    XOffSet = -5,
                                    YOffSet = 0,
                                }
                            },
                            new DataPoint
                            {
                                Value=2.9,
                                EndDate=new DateTime(2006, 12, 31)
                            },
                            new DataPoint
                            {
                                Value=1.9,
                                EndDate=new DateTime(2007, 12, 31)
                            },
                            new DataPoint
                            {
                                Value=-0.2,
                                EndDate=new DateTime(2008, 12, 31),
                                NegativeColor = SKColors.Orange
                            },
                            new DataPoint
                            {
                                Value=-2.5,
                                EndDate=new DateTime(2009, 12, 31)
                            }                            
                        }
                    },
                    new DataGroup
                    {
                        Label = "2010's",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint
                            {
                                Value=2.6,
                                EndDate=new DateTime(2010, 12, 31)
                            },
                            new DataPoint
                            {
                                Value=1.6,
                                EndDate=new DateTime(2011, 12, 31)
                            },
                            new DataPoint
                            {
                                Value=2.2,
                                EndDate=new DateTime(2012, 12, 31)
                            },
                            new DataPoint
                            {
                                Value=1.8,
                                EndDate=new DateTime(2013, 12, 31)
                            },
                            new DataPoint
                            {
                                Value=2.5,
                                EndDate=new DateTime(2014, 12, 31)
                            },
                            new DataPoint
                            {
                                Value=2.9,
                                EndDate=new DateTime(2015, 12, 31)
                            },
                            new DataPoint
                            {
                                Value=1.6,
                                EndDate=new DateTime(2016, 12, 31)
                            },
                            new DataPoint
                            {
                                Value=2.4,
                                EndDate=new DateTime(2017, 12, 31)
                            },
                            new DataPoint
                            {
                                Value=2.9,
                                EndDate=new DateTime(2018, 12, 31)
                            },
                            new DataPoint
                            {
                                Value=2.3,
                                EndDate=new DateTime(2019, 12, 31)
                            }
                        }
                    },
                    new DataGroup
                    {
                        //Label = "2020",
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPoint
                            {
                                Value=-5.9,
                                EndDate=new DateTime(2020, 3, 31)
                            }
                            
                        }
                    }
                },
                ShadedAreaColor = SKColors.LightGray,
                StartDate = new DateTime(1999, 1, 1)
            };
            #endregion

            #region PointGraphViewModel
            PointGraphViewModel = new PointGraphViewModel
            {
                BackgroundColor = SKColors.AliceBlue,
                Title = new Label
                {
                    Text = "Point Graph",
                    PointSize = 25,
                    TextAlignment = Xamarin.Forms.TextAlignment.Center
                },
                //HorizontalLinesStartAtEdge = false,
                HorizontalLineColor = SKColors.Black,
                //OddRowHorizontalColor = SKColors.AntiqueWhite,
                //OddRowVerticalColor = SKColors.AntiqueWhite,
                //VerticalLeftAxisColor = SKColors.Black,
                //VerticalLineColor = SKColors.Black,
                //XLabelAlignment = Xamarin.Forms.TextAlignment.Center,
                //XLabelColor = SKColors.Black,
                //XLabelPointSize = 20,
                //XLabelRotation = LabelRotation.Angle,
                YFirstLabelFormat = "{0:F}%",
                YLabelFormat = "{0:F}",
                YLabelAlignment = Xamarin.Forms.TextAlignment.Start,
                YLabelColor = SKColors.Black,
                YLabelPointSize = 20,
                //LineColor = SKColors.Red,
                VerticalLabelPrecision = 1M,
                CircleType = CircleType.Solid,
                CircleRadius = 10,
                //BottomGraphValue = 0,
                //TopGraphValue = 5,
                PointColor = SKColors.Black,
                MaxXValue = 35,
                DataGroups = new List<DataGroup>
                {
                    new DataGroup
                    {
                        DataPoints = new List<IDataPoint>
                        {
                            new DataPointX{Value=1, XValue=1,Label = new DataPointLabel
                                {
                                    Color = SKColors.Blue,
                                    Bold = true,
                                    PointSize = 15,
                                    Text = "1",
                                    XOffSet = -5,
                                    YOffSet = -15
                                }},
                            new DataPointX{Value=2, XValue=7,Label = new DataPointLabel
                                {
                                    Color = SKColors.Blue,
                                    Bold = true,
                                    PointSize = 15,
                                    Text = "2",
                                    XOffSet = -5,
                                    YOffSet = -15
                                }},
                            new DataPointX{Value=3, XValue=23,Label = new DataPointLabel
                                {
                                    Color = SKColors.Blue,
                                    Bold = true,
                                    PointSize = 15,
                                    Text = "3",
                                    XOffSet = -5,
                                    YOffSet = -15
                                }},
                            new DataPointX{Value=4, XValue=30,Label = new DataPointLabel
                                {
                                    Color = SKColors.Blue,
                                    Bold = true,
                                    PointSize = 15,
                                    Text = "4",
                                    XOffSet = -5,
                                    YOffSet = -15
                                }},
                        }
                    }
                }
            };
            #endregion

            #region MultiLineGraphViewModel
            MultiLineGraphViewModel = new MultiLineGraphViewModel
            {
                BackgroundColor = SKColors.AliceBlue,
                Title = new Label
                {
                    Bold = false,
                    //Color = SKColors.Black,
                    Text = "2020 Treasuries",
                    PointSize = 25,
                    TextAlignment = Xamarin.Forms.TextAlignment.Center
                },
                HorizontalLinesStartAtEdge = true,
                HorizontalLineColor = SKColors.Black,
                //OddRowHorizontalColor = SKColors.AntiqueWhite,
                //OddRowVerticalColor = SKColors.AntiqueWhite,
                //VerticalLeftAxisColor = SKColors.Black,
                VerticalLineColor = SKColors.Black,
                XLabelAlignment = Xamarin.Forms.TextAlignment.Center,
                XLabelColor = SKColors.Black,
                XLabelPointSize = 20,
                XLabelRotation = LabelRotation.Horizontal,
                YFirstLabelFormat = "{0:F}%",
                YLabelFormat = "{0:F}",
                YLabelAlignment = Xamarin.Forms.TextAlignment.Start,
                YLabelColor = SKColors.Black,
                YLabelPointSize = 20,
                HorizontalLabelPrecision = 1M,
                BottomGraphValue = 1,
                TopGraphValue = 2.5,
                DataGroups = new List<MultiLineDataGroup>
                {
                    new MultiLineDataGroup
                    {
                        Label = "January",
                        Lines = new List<Line>
                        {
                            new Line
                            {
                                LineColor = SKColors.Black,
                                DataPoints = new List<IDataPoint>
                                {
                                    new DataPoint{Value = 1.53,IndicatorLine = true},
                                    new DataPoint{Value = 1.52,IndicatorLine = true},
                                    new DataPoint{Value = 1.54,IndicatorLine = true},
                                    new DataPoint{Value = 1.52,IndicatorLine = true},
                                    new DataPoint{Value = 1.5,IndicatorLine = true},
                                }
                            },
                            new Line
                            {
                                LineColor = SKColors.Red,
                                DataPoints = new List<IDataPoint>
                                {
                                    new DataPoint{Value = 1.75},
                                    new DataPoint{Value = 1.75},
                                    new DataPoint{Value = 1.74},
                                    new DataPoint{Value = 1.73},
                                    new DataPoint{Value = 1.73},
                                }
                            },
                            new Line
                            {
                                LineColor = SKColors.Blue,
                                DataPoints = new List<IDataPoint>
                                {
                                    new DataPoint{Value = 1.94},
                                    new DataPoint{Value = 1.92},
                                    new DataPoint{Value = 1.96},
                                    new DataPoint{Value = 1.94},
                                    new DataPoint{Value = 1.94},
                                }
                            },
                            new Line
                            {
                                LineColor = SKColors.Green,
                                DataPoints = new List<IDataPoint>
                                {
                                    new DataPoint{Value = 2.33},
                                    new DataPoint{Value = 2.26},
                                    new DataPoint{Value = 2.28},
                                    new DataPoint{Value = 2.31},
                                    new DataPoint{Value = 2.35},
                                }
                            }
                        }
                    },
                    new MultiLineDataGroup
                    {
                        Label = "February",
                        Lines = new List<Line>
                        {
                            new Line
                            {
                                LineColor = SKColors.Black,
                                DataPoints = new List<IDataPoint>
                                {
                                    new DataPoint{Value = 1.53,IndicatorLine = true},
                                    new DataPoint{Value = 1.52,IndicatorLine = true},
                                    new DataPoint{Value = 1.54,IndicatorLine = true},
                                    new DataPoint{Value = 1.52,IndicatorLine = true},
                                    new DataPoint{Value = 1.5,IndicatorLine = true},
                                }
                            },
                            new Line
                            {
                                LineColor = SKColors.Red,
                                DataPoints = new List<IDataPoint>
                                {
                                    new DataPoint{Value = 1.75},
                                    new DataPoint{Value = 1.75},
                                    new DataPoint{Value = 1.74},
                                    new DataPoint{Value = 1.73},
                                    new DataPoint{Value = 1.73},
                                }
                            },
                            new Line
                            {
                                LineColor = SKColors.Blue,
                                DataPoints = new List<IDataPoint>
                                {
                                    new DataPoint{Value = 1.94},
                                    new DataPoint{Value = 1.92},
                                    new DataPoint{Value = 1.96},
                                    new DataPoint{Value = 1.94},
                                    new DataPoint{Value = 1.94},
                                }
                            },
                            new Line
                            {
                                LineColor = SKColors.Blue,
                                DataPoints = new List<IDataPoint>
                                {
                                    new DataPoint{Value = 2.33},
                                    new DataPoint{Value = 2.26},
                                    new DataPoint{Value = 2.28},
                                    new DataPoint{Value = 2.31},
                                    new DataPoint{Value = 2.35},
                                }
                            }
                        }
                    }
                }
            };
            #endregion

            #region ShadedMultiLineGraphViewModel
            ShadedMultiLineGraphViewModel = new ShadedMultiLineGraphViewModel
            {
                BackgroundColor = SKColors.AliceBlue,
                Title = new Label
                {
                    Bold = false,
                    //Color = SKColors.Black,
                    Text = "2020 Treasuries",
                    PointSize = 25,
                    TextAlignment = Xamarin.Forms.TextAlignment.Center
                },
                HorizontalLinesStartAtEdge = true,
                HorizontalLineColor = SKColors.Black,
                //OddRowHorizontalColor = SKColors.AntiqueWhite,
                //OddRowVerticalColor = SKColors.AntiqueWhite,
                //VerticalLeftAxisColor = SKColors.Black,
                VerticalLineColor = SKColors.Black,
                XLabelAlignment = Xamarin.Forms.TextAlignment.Center,
                XLabelColor = SKColors.Black,
                XLabelPointSize = 20,
                XLabelRotation = LabelRotation.Horizontal,
                YFirstLabelFormat = "{0:F}%",
                YLabelFormat = "{0:F}",
                YLabelAlignment = Xamarin.Forms.TextAlignment.Start,
                YLabelColor = SKColors.Black,
                YLabelPointSize = 20,
                HorizontalLabelPrecision = 1M,
                BottomGraphValue = 1,
                TopGraphValue = 2.5,
                DataGroups = new List<MultiLineDataGroup>
                {
                    new MultiLineDataGroup
                    {
                        Label = "January",
                        Lines = new List<Line>
                        {
                            new Line
                            {
                                LineColor = SKColors.Black,
                                DataPoints = new List<IDataPoint>
                                {
                                    new DataPoint{Value = 1.53,IndicatorLine = true,EndDate=new DateTime(2020,1,2)},
                                    new DataPoint{Value = 1.52,IndicatorLine = true,EndDate=new DateTime(2020,1,3)},
                                    new DataPoint{Value = 1.54,IndicatorLine = true,EndDate=new DateTime(2020,1,4)},
                                    new DataPoint{Value = 1.52,IndicatorLine = true,EndDate=new DateTime(2020,1,5)},
                                    new DataPoint{Value = 1.5,IndicatorLine = true,EndDate=new DateTime(2020,1,6)},
                                }
                            },
                            new Line
                            {
                                LineColor = SKColors.Red,
                                DataPoints = new List<IDataPoint>
                                {
                                    new DataPoint{Value = 1.75},
                                    new DataPoint{Value = 1.75},
                                    new DataPoint{Value = 1.74},
                                    new DataPoint{Value = 1.73},
                                    new DataPoint{Value = 1.73},
                                }
                            },
                            new Line
                            {
                                LineColor = SKColors.Blue,
                                DataPoints = new List<IDataPoint>
                                {
                                    new DataPoint{Value = 1.94},
                                    new DataPoint{Value = 1.92},
                                    new DataPoint{Value = 1.96},
                                    new DataPoint{Value = 1.94},
                                    new DataPoint{Value = 1.94},
                                }
                            },
                            new Line
                            {
                                LineColor = SKColors.Green,
                                DataPoints = new List<IDataPoint>
                                {
                                    new DataPoint{Value = 2.33},
                                    new DataPoint{Value = 2.26},
                                    new DataPoint{Value = 2.28},
                                    new DataPoint{Value = 2.31},
                                    new DataPoint{Value = 2.35},
                                }
                            }
                        }
                    },
                    new MultiLineDataGroup
                    {
                        Label = "February",
                        Lines = new List<Line>
                        {
                            new Line
                            {
                                LineColor = SKColors.Black,
                                DataPoints = new List<IDataPoint>
                                {
                                    new DataPoint{Value = 1.53,IndicatorLine = true,EndDate=new DateTime(2020,2,3)},
                                    new DataPoint{Value = 1.52,IndicatorLine = true,EndDate=new DateTime(2020,2,4)},
                                    new DataPoint{Value = 1.54,IndicatorLine = true,EndDate=new DateTime(2020,2,5)},
                                    new DataPoint{Value = 1.52,IndicatorLine = true,EndDate=new DateTime(2020,2,6)},
                                    new DataPoint{Value = 1.5,IndicatorLine = true,EndDate=new DateTime(2020,2,7)},
                                }
                            },
                            new Line
                            {
                                LineColor = SKColors.Red,
                                DataPoints = new List<IDataPoint>
                                {
                                    new DataPoint{Value = 1.75},
                                    new DataPoint{Value = 1.75},
                                    new DataPoint{Value = 1.74},
                                    new DataPoint{Value = 1.73},
                                    new DataPoint{Value = 1.73},
                                }
                            },
                            new Line
                            {
                                LineColor = SKColors.Blue,
                                DataPoints = new List<IDataPoint>
                                {
                                    new DataPoint{Value = 1.94},
                                    new DataPoint{Value = 1.92},
                                    new DataPoint{Value = 1.96},
                                    new DataPoint{Value = 1.94},
                                    new DataPoint{Value = 1.94},
                                }
                            },
                            new Line
                            {
                                LineColor = SKColors.Green,
                                DataPoints = new List<IDataPoint>
                                {
                                    new DataPoint{Value = 2.33},
                                    new DataPoint{Value = 2.26},
                                    new DataPoint{Value = 2.28},
                                    new DataPoint{Value = 2.31},
                                    new DataPoint{Value = 2.35},
                                }
                            }
                        }
                    }
                },
                StartDate = new DateTime(2020, 1, 1),
                ShadedAreaColor = SKColors.LightGray
            };
            #endregion
        }

        public HorizontalBarGraphViewModel HorizontalBarGraphViewModel { get; set; }
        public LineGraphViewModel LineGraphViewModel { get; set; }
        public LineGraphViewModel LineGraphLeftLabelViewModel { get; set; }
        public LineGraphViewModel LineGraphWithFootersViewModel { get; set; }
        public LineGraphViewModel LineGraphWithMultiLineHeaderViewModel { get; set; }
        public BarGraphViewModel BarGraphViewModel { get; set; }
        public ShadedLineGraphViewModel ShadedLineGraphViewModel { get; set; }
        public ShadedLineGraphViewModel ShadedLineGraphViewModelYearlyGDP { get; set; }
        public ShadedBarGraphViewModel ShadedBarGraphViewModel { get; set; }
        public PointGraphViewModel PointGraphViewModel { get; set; }
        public MultiLineGraphViewModel MultiLineGraphViewModel { get; set; }
        public ShadedMultiLineGraphViewModel ShadedMultiLineGraphViewModel { get; set; }

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
                case "LineGraphLeftLabel":
                    LineGraphLeftLabel = true;
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
                case "PointGraph":
                    PointGraph = true;
                    break;
                case "MultiLineGraph":
                    MultiLineGraph = true;
                    break;
                case "ShadedMultiLineGraph":
                    ShadedMultiLineGraph = true;
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
            LineGraphLeftLabel = false;
            LineGraphWithFooters = false;
            LineGraphWithMultiLineHeader = false;
            BarGraph = false;
            ShadedLineGraph = false;
            ShadedBarGraph = false;
            PointGraph = false;
            MultiLineGraph = false;
            ShadedMultiLineGraph = false;
        }
    }
}