using EconomyGraph.Models;
using EconomyGraph.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using Xamarin.Forms.Xaml;

namespace EconomyGraph.Views.ContentViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PointGraphView : LineGraphView
    {
        public PointGraphView()
        {
            InitializeComponent();
        }

        private void PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            PaintGraph(e);
        }
        protected override void ValidateData()
        {
            PointGraphViewModel vm = ViewModel as PointGraphViewModel;
            if (vm.CircleType == CircleType.None)
            {
                throw new ArgumentException("Invalid CircleType");
            }
            if (vm.CircleRadius <= 0)
            {
                throw new ArgumentException("CircleRadius must be set");
            }
            if (vm.DataGroups == null || vm.DataGroups.Count != 1)
            {
                throw new ArgumentException("Point graph must have only one DataGroup");
            }
            foreach (var dataGroup in ViewModel.DataGroups)
            {
                foreach (var dataPoint in dataGroup.DataPoints)
                {
                    var dataPointX = dataPoint as DataPointX;
                    if (dataPointX is null)
                    {
                        throw new ArgumentException("Point graph data points must be of type DataPointX");
                    }
                    if (!dataPoint.Value.HasValue)
                    {
                        throw new ArgumentException("Point graph data points must have a value");
                    }
                    if (vm.PointColor == null && dataPointX.Color == null)
                    {
                        throw new ArgumentException("PointColor for viewmodel or Color for data point must be set.");
                    }
                }
            }
        }

        protected override void DefineGraphAndGroupWidths(float canvasWidth, float graphHeight, float scale, float padding, float yPos, List<IDataPoint> dataPoints, List<string> YLabels, out float labelWidth, out float ySectionHeight, out float lineYPos, out float pointWidth, out float xPos, out float xLabelWidth)
        {
            SKPaint yLabelBrush;
            pointWidth = 0; // Unused for Point Graph!
            CreateYLabelBrush(scale, out yLabelBrush); // Needed to determine graph width, taking into account Y-Label max width.
            labelWidth = 0;
            foreach (string ylabel in YLabels)
            {
                float textWidth = yLabelBrush.MeasureText(ylabel);
                if (textWidth > labelWidth) labelWidth = textWidth;
            }
            xPos = labelWidth > 0 ? 2 * padding + labelWidth : padding;
            xLabelWidth = 0;
            if (ViewModel.LeftLabel != null && !string.IsNullOrWhiteSpace(ViewModel.LeftLabel.Text))
            {
                ViewModel.LeftLabel.Scale = scale;
                xPos += ViewModel.LeftLabel.Height;
                xLabelWidth = ViewModel.LeftLabel.Height;
            }
            ySectionHeight = graphHeight / YLabels.Count;
            lineYPos = yPos + padding;
            float graphWidth = canvasWidth - padding - xPos;
            foreach (DataGroup dg in ViewModel.DataGroups)
            {
                dg.GroupWidth = graphWidth;
            }
        }

        protected override void GraphData(float padding, List<IGraphItem> graphItems, float xPos, float yPos, double minimum, List<IDataPoint> dataPoints, float labelWidth, float graphHeight, float pointWidth, double minimumGraphValue, double maximumGraphValue, List<decimal> hValues, float ySectionHeight, float zeroYPos, float scale)
        {
            PointGraphViewModel vm = ViewModel as PointGraphViewModel;
            float xDP = xPos; // Essentially, this is X zero for graph on the canvas!
            float yDP = -1;
            double min = 0;
            double max = 0;
            bool first = true;
            foreach (var dataPoint in dataPoints)
            {
                DataPointX dp = dataPoint as DataPointX;
                if (first)
                {
                    min = dp.XValue;
                    max = dp.XValue;
                    first = false;
                    continue;
                }
                if (dp.XValue > max)
                {
                    max = dp.XValue;
                }
                else if (dp.XValue < min)
                {
                    min = dp.XValue;
                }
            }
            if (vm.MaxXValue.HasValue && vm.MaxXValue.Value > max)
            {
                max = vm.MaxXValue.Value;
            }

            double yRange = maximumGraphValue - minimumGraphValue;
            double xRange = max - min;
            foreach (var dataPoint in dataPoints)
            {
                DataPointX dp = dataPoint as DataPointX;
                yDP = graphHeight - Convert.ToSingle(graphHeight * (dataPoint.Value - minimumGraphValue) / yRange);
                xDP = Convert.ToSingle((dp.XValue - min) / xRange * vm.DataGroups[0].GroupWidth) + xPos;
                float strokeWidth = 3;
                graphItems.Add(new GraphCircle
                {
                    Color = dataPoint.Color != null ? dataPoint.Color.Value : vm.PointColor,
                    XPos = xDP,
                    YPos = yPos + padding + yDP,
                    Radius = vm.CircleRadius * scale,
                    PaintStyle = SKPaintStyle.Fill,
                    StrokeWidth = strokeWidth,
                });
                if (vm.CircleType == CircleType.Donut)
                {
                    graphItems.Add(new GraphCircle
                    {
                        Color = ViewModel.BackgroundColor.HasValue ? ViewModel.BackgroundColor.Value : SKColors.White,
                        XPos = xDP,
                        YPos = yPos + padding + yDP,
                        Radius = (vm.CircleRadius - strokeWidth) * scale,
                        PaintStyle = SKPaintStyle.Fill,
                        StrokeWidth = strokeWidth,
                    });
                }
                if (dataPoint.Label != null)
                {
                    graphItems.Add(new GraphText
                    {
                        Alignment = dataPoint.Label.TextAlignment,
                        Color = dataPoint.Label.Color,
                        PointSize = dataPoint.Label.PointSize * scale,
                        Text = dataPoint.Label.Text,
                        XPos = xDP + dataPoint.Label.XOffSet * scale,
                        YPos = yPos + padding + yDP + dataPoint.Label.YOffSet * scale,
                        Bold = dataPoint.Label.Bold
                    });
                }
                if (dataPoint.IndicatorLine)
                {
                    SKColor color;
                    if (ViewModel.VerticalLineColor.HasValue)
                    {
                        color = ViewModel.VerticalLineColor.Value;
                    }
                    else if (ViewModel.HorizontalBottomLineColor.HasValue)
                    {
                        color = ViewModel.HorizontalBottomLineColor.Value;
                    }
                    else if (ViewModel.HorizontalLineColor.HasValue)
                    {
                        color = ViewModel.HorizontalLineColor.Value;
                    }
                    else color = SKColors.Black;
                    graphItems.Add(new GraphLine
                    {
                        Color = color,
                        StrokeWidth = 2,
                        XPosStart = xDP,
                        YPosStart = yPos + graphHeight + padding,
                        XPosEnd = xDP,
                        YPosEnd = yPos + graphHeight + padding + ViewModel.InidicatorLineLength * scale
                    });
                }
            }
        }
    }
}