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
    public partial class ShadedLineGraphView : LineGraphView
    {
        public ShadedLineGraphView()
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
                    if (first)
                    {
                        first = false;
                        continue; // Don't draw shaded area for first point on line graph!
                    }
                    xDataPointStart = xDataPointEnd;
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
    }
}