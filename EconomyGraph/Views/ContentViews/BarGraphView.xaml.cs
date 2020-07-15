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

        protected override void GraphData(float padding, List<IGraphItem> graphItems, float yPos, double minimum, List<double> dataPoints, decimal hValue, float labelWidth, float graphHeight, float barWidth)
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

            float xPos = padding * 2 + labelWidth; // Essentially, this is X zero for graph on the canvas!
            float barPadding = (calculatedBarWidth - barWidth) / 2;
            double range = Convert.ToDouble(hValue) - minimum;
            foreach (var dataPoint in dataPoints)
            {
                float barHeight = Convert.ToSingle(graphHeight * (dataPoint - minimum) / range);
                graphItems.Add(new GraphRectangle
                {
                    Color = viewModel.BarColor,
                    Height = barHeight,
                    Style = PaintStyle.Fill,
                    Width = barWidth,
                    XPos = xPos + barPadding,
                    YPos = graphHeight + yPos + padding - barHeight
                });
                xPos += calculatedBarWidth;
            }
        }
    }
}