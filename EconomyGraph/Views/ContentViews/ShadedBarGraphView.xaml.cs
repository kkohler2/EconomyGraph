using EconomyGraph.Models;
using EconomyGraph.ViewModels;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms.Xaml;

namespace EconomyGraph.Views.ContentViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShadedBarGraphView : LineGraphView
    {
        public ShadedBarGraphView()
        {
            InitializeComponent();
        }

        private void PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            PaintGraph(e);
        }

        protected override void DrawShadedSections(List<IGraphItem> graphItems, float graphHeight, float xPos, float yPos, float padding, float labelWidth)
        {
            ShadedLineGraphViewModel viewModel = ViewModel as ShadedLineGraphViewModel;
            DateTime StartDate = new DateTime();
            DateTime EndDate = new DateTime();
            bool first = true;
            float xDataPointStart = xPos;
            float xDataPointEnd = xDataPointStart;
            GraphRectangle previousGraphRectangle = null;
            foreach (DataGroup dg in viewModel.DataGroups)
            {
                foreach(var dataPoint in dg.DataPoints)
                {
                    // Calculate graph X start/end for period here!
                    StartDate = StartDate.Year == 1 ? viewModel.StartDate : EndDate + new TimeSpan(1,0,0,0);
                    EndDate = dataPoint.EndDate;
                    Debug.Assert(StartDate < EndDate);
                    if (!first)
                    {
                        xDataPointStart = xDataPointEnd;
                    }
                    first = false;
                    xDataPointEnd += dg.GroupWidth / dg.DataPoints.Count;
                    foreach(var shadePeriod in viewModel.ShadePeriods)
                    {
                        float shadeXStart, barWidth;
                        DateTime shadeStartDate, shadeEndDate;
                        if (EndDate < shadePeriod.StartDate)
                            continue; // data before period
                        if (StartDate > shadePeriod.EndDate)
                            continue; // data after period
                        if (shadePeriod.StartDate <= StartDate && EndDate <= shadePeriod.EndDate)
                        {
                            shadeStartDate = StartDate;
                            shadeEndDate = EndDate;
                        }
                        else
                        {
                            if (shadePeriod.StartDate <= StartDate)
                            {
                                shadeStartDate = StartDate;
                            }
                            else
                            {
                                shadeStartDate = shadePeriod.StartDate;
                            }
                            if (shadePeriod.EndDate >= EndDate)
                            {
                                shadeEndDate = EndDate;
                            }
                            else
                            {
                                shadeEndDate = shadePeriod.EndDate;
                            }
                        }
                        if (shadeStartDate == StartDate)
                        {
                            shadeXStart = xDataPointStart;
                        }
                        else
                        {
                            float dpDays = (EndDate - StartDate).Days + 1;
                            float startDate = (shadeStartDate - StartDate).Days;
                            shadeXStart = (startDate / dpDays) * (dg.GroupWidth / dg.DataPoints.Count) + xDataPointStart - 1;
                        }
                        barWidth = (float)((shadeEndDate - shadeStartDate).Days + 1) / ((EndDate - StartDate).Days + 1) * (xDataPointEnd - xDataPointStart + 1);
                        GraphRectangle graphRectangle = new GraphRectangle
                        {
                            Color = viewModel.ShadedAreaColor,
                            Height = graphHeight,
                            Style = PaintStyle.Fill,
                            Width = barWidth,
                            XPos = shadeXStart,
                            YPos = graphHeight + yPos + padding - graphHeight
                        };
                        if (previousGraphRectangle != null)
                        {
                            if (graphRectangle.XPos - previousGraphRectangle.XPos - previousGraphRectangle.Width <= 1)
                            {
                                graphRectangle.XPos -= 1;
                            }
                        }
                        graphItems.Add(graphRectangle);
                        previousGraphRectangle = graphRectangle;
                    }
                }
            }
        }

        /// <summary>
        /// Same as BarGraphView.GraphData, but copied, since not derived from BarGraphView
        /// </summary>
        protected override void GraphData(float padding, List<IGraphItem> graphItems, float xPos, float yPos, double minimum, List<DataPoint> dataPoints, float labelWidth, float graphHeight, float barWidth, double minimumGraphValue, double maximumGraphValue, List<decimal> hValues, float ySectionHeight, float zeroYPos, float scale)
        {
            var calculatedBarWidth = barWidth; // for bar spacing
            ShadedBarGraphViewModel viewModel = ViewModel as ShadedBarGraphViewModel;
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
                    graphItems.Add(new GraphRectangle
                    {
                        Color = dataPoint.Color.HasValue ? dataPoint.Color.Value : viewModel.BarColor,
                        Height = barHeight,
                        Style = PaintStyle.Fill,
                        Width = barWidth,
                        XPos = xPos + barPadding,
                        YPos = zeroYPos != -1 ? zeroYPos - barHeight : graphHeight + yPos + padding - barHeight
                    });
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
                    graphItems.Add(new GraphRectangle
                    {
                        Color = dataPoint.NegativeColor.HasValue ? dataPoint.NegativeColor.Value : viewModel.NegativeBarColor,
                        Height = barHeight,
                        Style = PaintStyle.Fill,
                        Width = barWidth,
                        XPos = xPos + barPadding,
                        YPos = zeroYPos != -1 ? zeroYPos - barHeight : graphHeight + yPos + padding - barHeight
                    });
                }
                if (dataPoint.IndicatorLine)
                {
                    graphItems.Add(new GraphLine
                    {
                        Color = ViewModel.VerticalLineColor.Value,
                        StrokeWidth = 2,
                        XPosStart = xPos + barPadding + barWidth / 2,
                        YPosStart = yPos + graphHeight + padding,
                        XPosEnd = xPos + barPadding + barWidth / 2,
                        YPosEnd = yPos + graphHeight + padding + ViewModel.InidicatorLineLength * scale
                    });
                }
                xPos += calculatedBarWidth;
            }
        }
    }
}