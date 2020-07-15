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
            PaintGraph(e);
        }

        protected virtual void PaintGraph(SKPaintSurfaceEventArgs e)
        {
            #region Define/Initialize Variables
            float canvasWidth = e.Info.Width;
            float canvasHeight = e.Info.Height;
            float graphWidth;
            float scale = canvasHeight / (float)GraphHeight;
            float padding = ViewModel.Padding * scale;
            List<IGraphItem> graphItems = new List<IGraphItem>();
            float yPos;
            float footerHeight;

            double minimum;
            List<double> dataPoints;
            List<decimal> hValues;
            decimal hValue;

            List<string> YLabels = new List<string>();

            float labelWidth, graphHeight, ySectionHeight, lineYPos, pointWidth;
            int horizontalRow;
            #endregion

            SetBackgroundColor(graphItems);

            yPos = SetTitle(canvasWidth, scale, graphItems, padding);

            footerHeight = SetFooter(canvasHeight, canvasWidth, scale, graphItems, padding);

            YAxisRangeAndSections(out minimum, out dataPoints, out hValues, out hValue);

            DefineYAxisLabelText(hValues, YLabels);

            DefineGraphAndGroupWidths(canvasWidth, canvasHeight, footerHeight, out graphWidth, scale, padding, yPos, dataPoints, YLabels, out labelWidth, out graphHeight, out ySectionHeight, out lineYPos, out horizontalRow, out pointWidth);

            DrawVerticalShading(padding, graphItems, yPos, YLabels, labelWidth, ySectionHeight);

            DrawHorizontalLinesAndShading(canvasWidth, padding, graphItems, YLabels, labelWidth, ySectionHeight, ref lineYPos, ref horizontalRow);

            DrawYAxisLabels(scale, padding, graphItems, yPos, YLabels, labelWidth, ySectionHeight);

            DrawVerticalLines(padding, graphItems, yPos, YLabels, labelWidth, ySectionHeight);

            // Heart of the graph - define lines from one data point to the next.
            GraphData(padding, graphItems, yPos, minimum, dataPoints, hValue, labelWidth, graphHeight, pointWidth);

            DrawXAxisLabels(canvasHeight - footerHeight, scale, padding, graphItems, labelWidth);

            graphEngine.Draw(e.Surface, graphItems);
        }

        protected virtual void DrawXAxisLabels(float canvalHeight, float scale, float padding, List<IGraphItem> graphItems, float labelWidth)
        {
            float labelYPos = canvalHeight - padding;
            float labelXPos = padding * 2 + labelWidth;
            foreach (var dg in ViewModel.DataGroups)
            {
                if (string.IsNullOrWhiteSpace(dg.Label))
                    continue;

                float alignedLabelXPos = labelXPos;
                switch (ViewModel.XLabelAlignment)
                {
                    case TextAlignment.Start:
                        break;
                    case TextAlignment.Center:
                        alignedLabelXPos = labelXPos + dg.GroupWidth / 2;
                        break;
                    case TextAlignment.End:
                        alignedLabelXPos = labelXPos + dg.GroupWidth;
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
                labelXPos += dg.GroupWidth;
            }
        }

        protected virtual void GraphData(float padding, List<IGraphItem> graphItems, float yPos, double minimum, List<double> dataPoints, decimal hValue, float labelWidth, float graphHeight, float pointWidth)
        {
            float xDP = padding * 2 + labelWidth; // Essentially, this is X zero for graph on the canvas!
            float yDP = -1;
            float lastXdp = -1;
            float lastYdp = -1;
            //float graphWidth = canvasWidth - xDP - padding; // Canvas width less xPos of first point less padding on right side
            double range = Convert.ToDouble(hValue) - minimum;
            foreach (var dataPoint in dataPoints)
            {
                if (yDP == -1) // First data point
                {
                    yDP = graphHeight - Convert.ToSingle(graphHeight * (dataPoint - minimum) / range);
                }
                else
                {
                    lastXdp = xDP;
                    lastYdp = yDP;
                    xDP += pointWidth;
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
        }

        protected virtual void DefineGraphAndGroupWidths(float canvasWidth, float canvalHeight, float footerHeight, out float graphWidth, float scale, float padding, float yPos, List<double> dataPoints, List<string> YLabels, out float labelWidth, out float graphHeight, out float ySectionHeight, out float lineYPos, out int horizontalRow, out float pointWidth)
        {
            SKPaint yLabelBrush;
            CreateYLabelBrush(scale, out yLabelBrush); // Needed to determine graph width, taking into account Y-Label max width.
            labelWidth = 0;
            foreach (string ylabel in YLabels)
            {
                float textWidth = yLabelBrush.MeasureText(ylabel);
                if (textWidth > labelWidth) labelWidth = textWidth;
            }
            graphHeight = canvalHeight - yPos - padding * 2 - ViewModel.XLabelPointSize * 1.25f * scale - footerHeight;
            ySectionHeight = graphHeight / YLabels.Count;
            lineYPos = yPos + padding;
            horizontalRow = 0;
            graphWidth = canvasWidth - padding - (padding * 2 + labelWidth);
            pointWidth = graphWidth / dataPoints.Count;
            foreach (DataGroup dg in ViewModel.DataGroups)
            {
                dg.GroupWidth = pointWidth * dg.DataPoints.Count;
            }
        }

        protected virtual void DrawVerticalShading(float padding, List<IGraphItem> graphItems, float yPos, List<string> YLabels, float labelWidth, float ySectionHeight)
        {
            if (ViewModel.OddRowVerticalColor.HasValue)
            {
                float lineXPos = padding * 2 + labelWidth;
                float yPosStart = yPos + padding;
                float yPosEnd = yPos + padding + ySectionHeight * YLabels.Count;
                int verticalRow = 0;
                for (int i = 0; i < ViewModel.DataGroups.Count; i++)
                {
                    verticalRow++;
                    if (verticalRow % 2 == 1)
                    {
                        graphItems.Add(new GraphRectangle
                        {
                            Color = ViewModel.OddRowVerticalColor.Value,
                            Height = yPosEnd - yPosStart,
                            Style = PaintStyle.Fill,
                            Width = ViewModel.DataGroups[i].GroupWidth,
                            XPos = lineXPos,
                            YPos = yPosStart
                        });
                    }
                }
            }
        }

        protected virtual void DrawHorizontalLinesAndShading(float canvasWidth, float padding, List<IGraphItem> graphItems, List<string> YLabels, float labelWidth, float ySectionHeight, ref float lineYPos, ref int horizontalRow)
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
                if (ViewModel.HorizontalLines)
                {
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
        }

        protected virtual void DrawYAxisLabels(float scale, float padding, List<IGraphItem> graphItems, float yPos, List<string> YLabels, float labelWidth, float ySectionHeight)
        {
            float lineYPos = yPos + padding;
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
                if (string.IsNullOrWhiteSpace(ylabel))
                    continue;
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
        }

        protected virtual void DrawVerticalLines(float padding, List<IGraphItem> graphItems, float yPos, List<string> YLabels, float labelWidth, float ySectionHeight)
        {
            if (ViewModel.VerticalLines)
            {
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
                    lineXPos += ViewModel.DataGroups[i].GroupWidth;
                }
            }
        }

        protected virtual void CreateYLabelBrush(float scale, out SKPaint yLabelBrush)
        {
            yLabelBrush = graphEngine.GetTextBrush(new GraphText
            {
                Alignment = ViewModel.YLabelAlignment,
                Color = ViewModel.YLabelColor,
                PointSize = ViewModel.YLabelPointSize * scale,
                Bold = false
            });
        }

        protected virtual void DefineYAxisLabelText(List<decimal> hValues, List<string> YLabels)
        {
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
        }

        protected virtual void YAxisRangeAndSections(out double minimum, out List<double> dataPoints, out List<decimal> hValues, out decimal hValue)
        {
            minimum = ViewModel.BottomGraphValue;
            double maximum = ViewModel.TopGraphValue != 0 ? ViewModel.TopGraphValue : minimum;
            dataPoints = new List<double>();
            foreach (var dg in ViewModel.DataGroups)
            {
                dataPoints.AddRange(dg.DataPoints);
                foreach (var value in dg.DataPoints)
                {
                    if (value < minimum) { minimum = value; }
                    if (value > maximum) { maximum = value; }
                }
            }
            hValues = new List<decimal>();
            hValue = (decimal)minimum;
            while (hValue <= (decimal)maximum)
            {
                hValues.Add(Convert.ToDecimal(hValue));
                hValue += ViewModel.HorizontalLabelPrecision;
            }
            hValues.Add(Convert.ToDecimal(hValue));
        }

        protected virtual void SetBackgroundColor(List<IGraphItem> graphItems)
        {
            if (ViewModel.BackgroundColor.HasValue)
            {
                graphItems.Add(new GraphClear { Color = ViewModel.BackgroundColor.Value });
            }
        }

        protected virtual float SetTitle(float canvasWidth, float scale, List<IGraphItem> graphItems, float padding)
        {
            float yPos = padding;
            if (ViewModel.Title != null && !string.IsNullOrWhiteSpace(ViewModel.Title.Text))
            {
                ViewModel.Title.Scale = scale;
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
                bool first = true;
                foreach(string line in ViewModel.Title.Text.Split('\n'))
                {
                    if (!first) yPos += padding;
                    yPos += ViewModel.Title.Height;
                    graphItems.Add(new GraphText
                    {
                        Alignment = ViewModel.Title.TextAlignment,
                        Color = ViewModel.Title.Color,
                        PointSize = ViewModel.Title.PointSize * scale,
                        Text = line,
                        XPos = xPos,
                        YPos = yPos,
                        Bold = ViewModel.Title.Bold // true;
                    });
                    first = false;
                }
            }
            yPos += padding;

            return yPos;
        }

        protected virtual float SetFooter(float canvasHeight, float canvalWidth, float scale, List<IGraphItem> graphItems, float padding)
        {
            float footerHeight = 0;
            if (ViewModel.LeftFooter != null && !string.IsNullOrWhiteSpace(ViewModel.LeftFooter.Text))
            {
                ViewModel.LeftFooter.Scale = scale;
                footerHeight = ViewModel.LeftFooter.Height;
            }
            if (ViewModel.CenterFooter != null && !string.IsNullOrWhiteSpace(ViewModel.CenterFooter.Text))
            {
                if (ViewModel.CenterFooter.Height > footerHeight)
                {
                    ViewModel.CenterFooter.Scale = scale;
                    footerHeight = ViewModel.CenterFooter.Height;
                }
            }
            if (ViewModel.RightFooter != null && !string.IsNullOrWhiteSpace(ViewModel.RightFooter.Text))
            {
                if (ViewModel.RightFooter.Height > footerHeight)
                {
                    ViewModel.RightFooter.Scale = scale;
                    footerHeight = ViewModel.RightFooter.Height;
                }
            }

            float yPos = canvasHeight - padding;
            if (ViewModel.LeftFooter != null && !string.IsNullOrWhiteSpace(ViewModel.LeftFooter.Text))
            {
                graphItems.Add(new GraphText
                {
                    Alignment = TextAlignment.Start,
                    Color = ViewModel.LeftFooter.Color,
                    PointSize = ViewModel.LeftFooter.PointSize * scale,
                    Text = ViewModel.LeftFooter.Text,
                    XPos = padding,
                    YPos = yPos,
                    Bold = ViewModel.LeftFooter.Bold
                });
            }
            if (ViewModel.CenterFooter != null && !string.IsNullOrWhiteSpace(ViewModel.CenterFooter.Text))
            {
                graphItems.Add(new GraphText
                {
                    Alignment = TextAlignment.Center,
                    Color = ViewModel.CenterFooter.Color,
                    PointSize = ViewModel.CenterFooter.PointSize * scale,
                    Text = ViewModel.CenterFooter.Text,
                    XPos = canvalWidth / 2,
                    YPos = yPos,
                    Bold = ViewModel.CenterFooter.Bold
                });
            }
            if (ViewModel.RightFooter != null && !string.IsNullOrWhiteSpace(ViewModel.RightFooter.Text))
            {
                graphItems.Add(new GraphText
                {
                    Alignment = TextAlignment.End,
                    Color = ViewModel.RightFooter.Color,
                    PointSize = ViewModel.RightFooter.PointSize * scale,
                    Text = ViewModel.RightFooter.Text,
                    XPos = canvalWidth - padding,
                    YPos = yPos,
                    Bold = ViewModel.RightFooter.Bold
                });
            }

            if (footerHeight > 0)
            {
                footerHeight += padding;
            }

            return footerHeight;
        }
    }
}