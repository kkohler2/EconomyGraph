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

        protected override void DrawShadedSections(List<IGraphItem> graphItems, float graphHeight, float yPos, float padding, float labelWidth)
        {
            ShadedLineGraphViewModel viewModel = ViewModel as ShadedLineGraphViewModel;
            DateTime StartDate = new DateTime();
            DateTime EndDate = new DateTime();
            bool first = true;
            float xDataPointStart = padding * 2 + labelWidth;
            float xDataPointEnd = xDataPointStart;
            foreach(DataGroup dg in viewModel.DataGroups)
            {
                Debug.Assert(dg.EndDates.Count == dg.DataPoints.Count, "DataGroup.EndDates.Count != DataGroup.DataPoints.Counts");
                for(var i = 0; i < dg.DataPoints.Count; i++)
                {
                    var dataPoint = dg.DataPoints[i];
                    // Calculate graph X start/end for period here!

                    StartDate = StartDate.Year == 1 ? viewModel.StartDate : EndDate + new TimeSpan(1,0,0,0);
                    EndDate = dg.EndDates[i];
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
                        graphItems.Add(new GraphRectangle
                        {
                            Color = viewModel.ShadedAreaColor,
                            Height = graphHeight,
                            Style = PaintStyle.Fill,
                            Width = barWidth,
                            XPos = shadeXStart,
                            YPos = graphHeight + yPos + padding - graphHeight
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Same as BarGraphView.GraphData, but copied, since not derived from BarGraphView
        /// </summary>
        protected override void GraphData(float padding, List<IGraphItem> graphItems, float xPos, float yPos, double minimum, List<double> dataPoints, float labelWidth, float graphHeight, float barWidth, double minimumGraphValue, double maximumGraphValue, List<decimal> hValues, float ySectionHeight, float zeroYPos)
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
                if (dataPoint >= 0)
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
                    float barHeight = Convert.ToSingle(barRange * (dataPoint - minimumBarValue) / range);
                    float offset = 0;
                    if (zeroIndex != -1)
                    {
                        offset = hValues.Count - (zeroIndex - 1) * ySectionHeight;
                    }
                    graphItems.Add(new GraphRectangle
                    {
                        Color = viewModel.BarColor,
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
                    float percent = Convert.ToSingle((dataPoint - minimumBarValue) / range);
                    float barHeight = Convert.ToSingle(barRange * (dataPoint - minimumBarValue) / range);
                    float offset = 0;
                    if (zeroIndex != -1)
                    {
                        offset = hValues.Count - (zeroIndex - 1) * ySectionHeight;
                    }
                    graphItems.Add(new GraphRectangle
                    {
                        Color = viewModel.NegativeBarColor,
                        Height = barHeight,
                        Style = PaintStyle.Fill,
                        Width = barWidth,
                        XPos = xPos + barPadding,
                        YPos = zeroYPos != -1 ? zeroYPos - barHeight : graphHeight + yPos + padding - barHeight
                    });
                }
                xPos += calculatedBarWidth;
            }
        }
    }
}