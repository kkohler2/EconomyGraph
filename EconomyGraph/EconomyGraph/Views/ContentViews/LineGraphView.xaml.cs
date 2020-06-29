using EconomyGraph.Models;
using EconomyGraph.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EconomyGraph.Views.ContentViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LineGraphView : ContentView
    {
        #region Bindings
        public static readonly BindableProperty GraphWidthProperty =
            BindableProperty.Create("GraphWidth", typeof(double), typeof(LineGraphView), null, BindingMode.OneTime);

        public static readonly BindableProperty GraphHeightProperty =
            BindableProperty.Create("GraphHeight", typeof(double), typeof(LineGraphView), null, BindingMode.OneTime);

        public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create("ViewModel", typeof(LineGraphViewModel), typeof(LineGraphView), null, BindingMode.OneTime);

        public double GraphWidth
        {
            get { return (double)GetValue(GraphWidthProperty); }
            set { SetValue(GraphWidthProperty, value); }
        }

        public double GraphHeight
        {
            get { return (double)GetValue(GraphHeightProperty); }
            set { SetValue(GraphHeightProperty, value); }
        }

        public LineGraphViewModel ViewModel
        {
            get { return (LineGraphViewModel)GetValue(ViewModelProperty); }
            set { SetValue(GraphHeightProperty, value); }
        }
        #endregion

        readonly GraphEngine graphEngine;
        public LineGraphView()
        {
            graphEngine = new GraphEngine();
            InitializeComponent();
        }

        private void PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            float groupWidth = 0;
            float canvasWidth = e.Info.Width;
            float canvalHeight = e.Info.Height;
            float scale = canvalHeight / (float)GraphHeight;
            float padding = ViewModel.Padding * scale;
            List<IGraphItem> graphItems = new List<IGraphItem>();
            float yPos = padding;
            SetBackgroundColor(graphItems);
            yPos = SetTitle(canvasWidth, scale, graphItems, padding);

            double minimum = ViewModel.BottomGraphValue;
            double maximum = minimum;
            foreach (var dg in ViewModel.DataGroups)
            {
                foreach (var value in dg.DataPoints)
                {
                    if (value < minimum) { minimum = value; }
                    if (value > maximum) { maximum = value; }
                }
            }
            List<decimal> hValues = new List<decimal>();
            decimal hValue = (decimal)minimum;
            while (hValue <= (decimal)maximum)
            {
                hValues.Add(Convert.ToDecimal(hValue));
                hValue += ViewModel.HorizontalLabelPrecision;
            }
            hValues.Add(Convert.ToDecimal(hValue));
            List<string> YLabels = new List<string>();
            bool first = true;
            bool second = false;
            hValues.Reverse();
            foreach (var hv in hValues)
            {
                if (first)
                {
                    first = false;
                    second = true;
                }
                else if (second)
                {
                    second = false;
                    YLabels.Add(string.Format(ViewModel.YFirstLabelFormat, hv));
                }
                else
                {
                    YLabels.Add(string.Format(ViewModel.YLabelFormat, hv));
                }
            }
            var yLabelBrush = graphEngine.GetTextBrush(new GraphText
            {
                Alignment = ViewModel.YLabelAlignment,
                Color = ViewModel.YLabelColor,
                PointSize = ViewModel.YLabelPointSize * scale,
                Bold = false
            });
            var XLabelBrush = graphEngine.GetTextBrush(new GraphText
            {
                Alignment = ViewModel.XLabelAlignment,
                Color = ViewModel.XLabelColor,
                PointSize = ViewModel.XLabelPointSize * scale,
                Bold = false
            });
            float labelWidth = 0;
            foreach (string ylabel in YLabels)
            {
                float textWidth = yLabelBrush.MeasureText(ylabel);
                if (textWidth > labelWidth) labelWidth = textWidth;
            }
            float graphHeight = canvalHeight - yPos - padding * 2 - ViewModel.XLabelPointSize * 1.25f * scale;
            float ySectionHeight = graphHeight / YLabels.Count;
            float lineYPos = yPos + padding;
            int horizontalRow = 0;

            if (ViewModel.VerticalLines)
            {
                groupWidth = (canvasWidth - padding - (padding * 2 + labelWidth)) / ViewModel.DataGroups.Count;
                float lineXPos = padding * 2 + labelWidth;
                float yPosStart = yPos + padding;
                float yPosEnd = yPos + padding + ySectionHeight * YLabels.Count;
                int verticalRow = 0;
                for (int i = 0; i < ViewModel.DataGroups.Count; i++)
                {
                    if (ViewModel.OddRowVerticalColor.HasValue)
                    {
                        verticalRow++;
                        if (verticalRow % 2 == 1)
                        {
                            graphItems.Add(new GraphRectangle
                            {
                                Color = ViewModel.OddRowVerticalColor.Value,
                                Height = yPosEnd - yPosStart,
                                Style = PaintStyle.Fill,
                                Width = groupWidth,
                                XPos = lineXPos,
                                YPos = yPosStart
                            });
                        }
                    }
                }
            }

            if (ViewModel.HorizontalLines)
            {
                foreach (string ylabel in YLabels)
                {
                    lineYPos += ySectionHeight;
                    if (ViewModel.OddRowHorizontalColor.HasValue)
                    {
                        if (horizontalRow % 2 == 0)
                        {
                            graphItems.Add(new GraphRectangle
                            {
                                Color = ViewModel.OddRowHorizontalColor.Value,
                                Height = ySectionHeight,
                                Style = PaintStyle.Fill,
                                Width = canvasWidth - padding,
                                XPos = padding * 2 + labelWidth,
                                YPos = lineYPos - ySectionHeight
                            });
                        }
                        horizontalRow++;
                    }
                    graphItems.Add(new GraphLine
                    {
                        Color = SKColors.Black,
                        StrokeWidth = 2,
                        XPosStart = padding,
                        YPosStart = lineYPos,
                        XPosEnd = canvasWidth - padding,
                        YPosEnd = lineYPos
                    });
                }
            }

            lineYPos = yPos + padding;
            float xLabelPos = 0;
            switch (ViewModel.YLabelAlignment)
            {
                case TextAlignment.Start:
                    xLabelPos = padding;
                    break;
                case TextAlignment.Center:
                    xLabelPos = Convert.ToInt32(labelWidth / 2) + padding;
                    break;
                case TextAlignment.End:
                    xLabelPos = labelWidth + padding * 2;
                    break;
            }
            foreach (string ylabel in YLabels)
            {
                lineYPos += ySectionHeight;
                graphItems.Add(new GraphText
                {
                    Alignment = ViewModel.YLabelAlignment,
                    Color = ViewModel.YLabelColor,
                    PointSize = ViewModel.YLabelPointSize * scale,
                    Text = ylabel,
                    XPos = xLabelPos,
                    YPos = lineYPos - padding,
                    Bold = false
                });
            }

            if (ViewModel.VerticalLines)
            {
                groupWidth = (canvasWidth - padding - (padding * 2 + labelWidth)) / ViewModel.DataGroups.Count;
                float lineXPos = padding * 2 + labelWidth;
                float yPosStart = yPos + padding * 2;
                float yPosEnd = yPos + padding + ySectionHeight * YLabels.Count;
                for (int i = 0; i < ViewModel.DataGroups.Count; i++)
                {
                    graphItems.Add(new GraphLine
                    {
                        Color = SKColors.Black,
                        StrokeWidth = 2,
                        XPosStart = lineXPos,
                        YPosStart = yPosStart,
                        XPosEnd = lineXPos,
                        YPosEnd = yPosEnd
                    });
                    lineXPos += groupWidth;
                }
            }
            float xDP = padding * 2 + labelWidth;
            float yDP = -1;
            float lastXdp = -1;
            float lastYdp = -1;
            List<double> dataPoints = new List<double>();
            float graphWidth = canvasWidth - xDP - padding; // Canvas width less xPos of first point less padding on right side
            int maxGroupDataPoints = 0;
            foreach (var dg in ViewModel.DataGroups)
            {
                if (dg.DataPoints.Count > maxGroupDataPoints) maxGroupDataPoints = dg.DataPoints.Count;
                dataPoints.AddRange(dg.DataPoints);
            }
            float dataPointSpacing = graphWidth / ViewModel.DataGroups.Count / maxGroupDataPoints;
            double range = Convert.ToDouble(hValue) - minimum;
            foreach(var dataPoint in dataPoints)
            {
                if (yDP == -1) // First data point
                {
                    yDP = graphHeight - Convert.ToSingle(graphHeight * (dataPoint - minimum) / range);
                }
                else
                {
                    lastXdp = xDP;
                    lastYdp = yDP;
                    xDP += dataPointSpacing;
                    yDP = graphHeight - Convert.ToSingle(graphHeight * (dataPoint - minimum) / range);
                    graphItems.Add(new GraphLine
                    {
                        Color = ViewModel.LineColor,
                        StrokeWidth = 3,
                        XPosStart = lastXdp,
                        YPosStart = yPos + padding + lastYdp,
                        XPosEnd = xDP,
                        YPosEnd = yPos + padding + yDP
                    });
                }
            }

            groupWidth = (canvasWidth - padding - (padding * 2 + labelWidth)) / ViewModel.DataGroups.Count;
            float labelYPos = canvalHeight - padding;
            float labelXPos = padding * 2 + labelWidth;
            foreach (var dg in ViewModel.DataGroups)
            {
                float alignedLabelXPos = labelXPos;
                switch (ViewModel.XLabelAlignment)
                {
                    case TextAlignment.Start:
                        break;
                    case TextAlignment.Center:
                        alignedLabelXPos = labelXPos + groupWidth / 2;
                        break;
                    case TextAlignment.End:
                        alignedLabelXPos = labelXPos + groupWidth;
                        break;
                }
                graphItems.Add(new GraphText
                {
                    Alignment = ViewModel.XLabelAlignment,
                    Color = ViewModel.XLabelColor,
                    PointSize = ViewModel.XLabelPointSize * scale,
                    Text = dg.Label,
                    XPos = alignedLabelXPos,
                    YPos = labelYPos,
                    Bold = false
                });
                labelXPos += groupWidth;
            }

            graphEngine.Draw(e.Surface, graphItems);
        }

        private void SetBackgroundColor(List<IGraphItem> graphItems)
        {
            if (ViewModel.BackgroundColor.HasValue)
            {
                graphItems.Add(new GraphClear { Color = ViewModel.BackgroundColor.Value });
            }
        }

        private float SetTitle(float canvasWidth, float scale, List<IGraphItem> graphItems, float padding)
        {
            float yPos = padding;
            if (ViewModel.Title != null && !string.IsNullOrWhiteSpace(ViewModel.Title.Text))
            {
                float xPos = 0;
                switch (ViewModel.Title.TextAlignment)
                {
                    case TextAlignment.Start:
                        xPos = 40;
                        break;
                    case TextAlignment.Center:
                        xPos = Convert.ToInt32(canvasWidth / 2);
                        break;
                    case TextAlignment.End:
                        xPos = Convert.ToInt32(canvasWidth - 40);
                        break;
                }
                yPos += ViewModel.Title.Height;
                graphItems.Add(new GraphText
                {
                    Alignment = ViewModel.Title.TextAlignment,
                    Color = ViewModel.Title.Color,
                    PointSize = ViewModel.Title.PointSize * scale,
                    Text = ViewModel.Title.Text,
                    XPos = xPos,
                    YPos = yPos,
                    Bold = ViewModel.Title.Bold // true;
                });
            }
            yPos += padding;

            return yPos;
        }
    }
}