using EconomyGraph.Models;
using EconomyGraph.ViewModels;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using Xamarin.Forms.Xaml;

namespace EconomyGraph.Views.ContentViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BarGraphView : LineGraphView
    {
        public BarGraphView()
        {
            InitializeComponent();
        }

        private void PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            PaintGraph(e);
        }

        protected override void GraphData(float padding, List<IGraphItem> graphItems, float xPos, float yPos, double minimum, List<DataPoint> dataPoints, float labelWidth, float graphHeight, float barWidth, double minimumGraphValue, double maximumGraphValue, List<decimal> hValues, float ySectionHeight, float zeroYPos, float scale)
        {
            var calculatedBarWidth = barWidth; // for bar spacing
            BarGraphViewModel viewModel = ViewModel as BarGraphViewModel;
            if (viewModel.BarDefaultWidth.HasValue && viewModel.BarDefaultWidth.Value < barWidth && viewModel.BarDefaultWidth.Value > 0)
            {
                barWidth = viewModel.BarDefaultWidth.Value;
            }
            else if (barWidth > 2)
            {
                barWidth -= 1;
            }
            else
            {
                barWidth = 1;
            }
            int zeroIndex = -1;
            if (minimumGraphValue < 0)
            {
                for (int i = 0; i < hValues.Count; i++)
                {
                    if (hValues[i] == 0)
                    {
                        zeroIndex = i;
                        break;
                    }
                }
            }

            float barPadding = (calculatedBarWidth - barWidth) / 2;
            foreach (var dataPoint in dataPoints)
            {
                GraphRectangle graphRectangle = null;
                if (dataPoint.Value >= 0)
                {
                    double barRange = graphHeight;
                    if (zeroIndex != -1)
                    {
                        barRange = graphHeight * zeroIndex / (hValues.Count - 1);
                    }
                    double range = maximumGraphValue - minimumGraphValue;
                    double minimumBarValue = minimumGraphValue;
                    if (minimumGraphValue < 0)
                    {
                        range = maximumGraphValue - 0;
                        minimumBarValue = 0;
                    }
                    float barHeight = Convert.ToSingle(barRange * (dataPoint.Value - minimumBarValue) / range);
                    float offset = 0;
                    if (zeroIndex != -1)
                    {
                        offset = hValues.Count - (zeroIndex - 1) * ySectionHeight;
                    }
                    graphRectangle = new GraphRectangle
                    {
                        Color = dataPoint.Color.HasValue ? dataPoint.Color.Value : viewModel.BarColor,
                        Height = barHeight,
                        Style = PaintStyle.Fill,
                        Width = barWidth,
                        XPos = xPos + barPadding,
                        YPos = zeroYPos != -1 ? zeroYPos - barHeight : graphHeight + yPos + padding - barHeight
                    };
                }
                else
                {
                    double barRange = graphHeight;
                    if (zeroIndex != -1)
                    {
                        barRange = graphHeight * (hValues.Count - zeroIndex - 1) / (hValues.Count - 1);
                    }
                    double range = maximumGraphValue - minimumGraphValue;
                    double minimumBarValue = minimumGraphValue;
                    if (minimumGraphValue < 0)
                    {
                        range = 0 - minimumGraphValue;
                        minimumBarValue = 0;
                    }
                    float percent = Convert.ToSingle((dataPoint.Value - minimumBarValue) / range);
                    float barHeight = Convert.ToSingle(barRange * (dataPoint.Value - minimumBarValue) / range);
                    float offset = 0;
                    if (zeroIndex != -1)
                    {
                        offset = hValues.Count - (zeroIndex - 1) * ySectionHeight;
                    }
                    graphRectangle = new GraphRectangle
                    {
                        Color = dataPoint.NegativeColor.HasValue ? dataPoint.NegativeColor.Value : viewModel.NegativeBarColor,
                        Height = barHeight,
                        Style = PaintStyle.Fill,
                        Width = barWidth,
                        XPos = xPos + barPadding,
                        YPos = zeroYPos != -1 ? zeroYPos - barHeight : graphHeight + yPos + padding - barHeight
                    };
                }
                graphItems.Add(graphRectangle);
                if (dataPoint.Label != null)
                {
                    graphItems.Add(new GraphText
                    {
                        Alignment = dataPoint.Label.TextAlignment,
                        Color = dataPoint.Label.Color,
                        PointSize = dataPoint.Label.PointSize * scale,
                        Text = dataPoint.Label.Text,
                        XPos = graphRectangle.XPos + dataPoint.Label.XOffSet * scale,
                        YPos = graphRectangle.YPos + dataPoint.Label.YOffSet * scale,
                        Bold = dataPoint.Label.Bold
                    });
                }
                xPos += calculatedBarWidth;
            }
        }
    }
}