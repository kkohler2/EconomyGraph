using EconomyGraph.Models;
using EconomyGraph.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EconomyGraph.Views.ContentViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MultiLineGraphView : ContentView
    {
        #region Bindings
        public static readonly BindableProperty GraphWidthProperty =
            BindableProperty.Create("GraphWidth", typeof(double), typeof(LineGraphView), null, BindingMode.OneTime);

        public static readonly BindableProperty GraphHeightProperty =
            BindableProperty.Create("GraphHeight", typeof(double), typeof(LineGraphView), null, BindingMode.OneTime);

        public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create("ViewModel", typeof(MultiLineGraphViewModel), typeof(MultiLineGraphView), null, BindingMode.OneTime);

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

        public MultiLineGraphViewModel ViewModel
        {
            get { return (MultiLineGraphViewModel)GetValue(ViewModelProperty); }
            set { SetValue(GraphHeightProperty, value); }
        }
        #endregion

        readonly GraphEngine graphEngine;
        public MultiLineGraphView()
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
            float scale = canvasHeight / (float)GraphHeight;
            float padding = ViewModel.Padding * scale;
            List<IGraphItem> graphItems = new List<IGraphItem>();
            float yPos, xPos, xLabelWidth;
            float footerHeight;
            float xLabelYPos;

            double minimum;
            List<IDataPoint> dataPoints;
            List<decimal> hValues;
            decimal hValue;

            List<string> YLabels = new List<string>();

            float labelWidth, graphHeight, ySectionHeight, lineYPos, pointWidth;
            int horizontalRow = 0;

            double minimumGraphValue, maximumGraphValue;
            float zeroYPos = -1;
            #endregion

            ValidateData();

            SetBackgroundColor(graphItems);

            yPos = SetTitle(canvasWidth, scale, graphItems, padding);

            footerHeight = SetFooter(canvasHeight, canvasWidth, scale, graphItems, padding);

            YAxisRangeAndSections(out minimum, out dataPoints, out hValues, out hValue, out minimumGraphValue, out maximumGraphValue);

            DefineYAxisLabelText(hValues, YLabels);

            graphHeight = CalculateGraphHeight(canvasHeight, yPos, padding, footerHeight, scale, out xLabelYPos);

            DefineGraphAndGroupWidths(canvasWidth, graphHeight, scale, padding, yPos, dataPoints, YLabels, out labelWidth, out ySectionHeight, out lineYPos, out pointWidth, out xPos, out xLabelWidth);

            DrawLeftLabel(graphItems, graphHeight, scale, yPos);

            DrawShadedSections(graphItems, graphHeight, xPos, yPos, padding, labelWidth);

            DrawVerticalShading(padding, graphItems, yPos, YLabels, labelWidth, ySectionHeight, xPos);

            DrawHorizontalLinesAndShading(xPos, xLabelWidth, canvasWidth, padding, graphItems, YLabels, labelWidth, ySectionHeight, ref lineYPos, ref horizontalRow, hValues, ref zeroYPos);

            DrawYAxisLabels(scale, padding, graphItems, yPos, YLabels, labelWidth, ySectionHeight, xLabelWidth);

            DrawVerticalLines(padding, xPos, graphItems, yPos, YLabels, labelWidth, ySectionHeight);

            // Heart of the graph - define lines from one data point to the next.
            GraphData(padding, graphItems, xPos, yPos, minimum, dataPoints, labelWidth, graphHeight, pointWidth, minimumGraphValue, maximumGraphValue, hValues, ySectionHeight, zeroYPos, scale);

            DrawXAxisLabels(xLabelYPos, scale, padding, graphItems, labelWidth, xPos);

            graphEngine.Draw(e.Surface, graphItems);
        }

        protected virtual void ValidateData()
        {
            int lines = -1;
            foreach (var dataGroup in ViewModel.DataGroups)
            {
                if (lines == -1)
                {
                    lines = dataGroup.Lines.Count;
                    if (lines == 0) throw new ArgumentException("There must be at least one line per data group.");
                }
                else if (lines != dataGroup.Lines.Count)
                {
                    throw new ArgumentException("Each data group needs the same number of lines.");
                }

                int dataPoints = -1;
                foreach (var line in dataGroup.Lines)
                {
                    if (dataPoints == -1)
                    {
                        dataPoints = line.DataPoints.Count;
                        if (dataPoints == 0) throw new ArgumentException("There must be at least one line per data group.");
                    }
                    else if (dataPoints != line.DataPoints.Count)
                    {
                        throw new ArgumentException("Each line needs the same number of data points.");
                    }
                }
            }
        }

        protected virtual void DrawXAxisLabels(float labelYPos, float scale, float padding, List<IGraphItem> graphItems, float labelWidth, float xPos)
        {
            float labelXPos = xPos;
            foreach (var dg in ViewModel.DataGroups)
            {
                if (string.IsNullOrWhiteSpace(dg.Label))
                {
                    labelXPos += dg.GroupWidth;
                    continue;
                }

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
                float yPos = labelYPos;
                SKPaint xLabelBrush = CreateXLabelBrush(scale);
                float textWidth = xLabelBrush.MeasureText(dg.Label);
                switch (ViewModel.XLabelRotation)
                {
                    case LabelRotation.Horizontal:
                        foreach (string line in dg.Label.Split('\n'))
                        {
                            yPos += ViewModel.XLabelPointSize * scale * 1.25f;
                            graphItems.Add(new GraphText
                            {
                                Alignment = ViewModel.XLabelAlignment,
                                Color = ViewModel.XLabelColor,
                                PointSize = ViewModel.XLabelPointSize * scale,
                                Text = line,
                                XPos = alignedLabelXPos,
                                YPos = yPos,
                                Bold = false
                            });
                        }
                        break;
                    case LabelRotation.Angle:
                        graphItems.Add(new GraphText
                        {
                            Alignment = TextAlignment.End,
                            Color = ViewModel.XLabelColor,
                            PointSize = ViewModel.XLabelPointSize * scale,
                            Text = dg.Label,
                            XPos = alignedLabelXPos,
                            YPos = yPos + ViewModel.XLabelPointSize * scale,
                            Bold = false,
                            Rotation = 315
                        });
                        break;
                    case LabelRotation.Vertical:
                        graphItems.Add(new GraphText
                        {
                            Alignment = TextAlignment.Start,
                            Color = ViewModel.XLabelColor,
                            PointSize = ViewModel.XLabelPointSize * scale,
                            Text = dg.Label,
                            XPos = alignedLabelXPos + ViewModel.XLabelPointSize * scale - padding,
                            YPos = yPos + textWidth,
                            Bold = false,
                            Rotation = 270
                        });
                        break;
                }
                labelXPos += dg.GroupWidth;
            }
        }

        protected virtual void GraphData(float padding, List<IGraphItem> graphItems, float xPos, float yPos, double minimum, List<IDataPoint> dataPoints, float labelWidth, float graphHeight, float pointWidth, double minimumGraphValue, double maximumGraphValue, List<decimal> hValues, float ySectionHeight, float zeroYPos, float scale)
        {
            for(var iLine = 0; iLine < ViewModel.DataGroups[0].Lines.Count; iLine++)
            {
                SKColor lineColor = ViewModel.DataGroups[0].Lines[iLine].LineColor;
                dataPoints.Clear();
                foreach (var dataGroup in ViewModel.DataGroups)
                {
                    dataPoints.AddRange(dataGroup.Lines[iLine].DataPoints);
                }
                float xDP = xPos; // Essentially, this is X zero for graph on the canvas!
                float yDP = -1;
                float lastXdp;
                float lastYdp;

                double range = maximumGraphValue - minimumGraphValue;
                IDataPoint previousDataPoint = null;
                foreach (var dataPoint in dataPoints)
                {
                    if (yDP == -1 || !dataPoint.Value.HasValue) // First data point or current data point does not have a value.
                    {
                        yDP = graphHeight - Convert.ToSingle(graphHeight * (dataPoint.Value - minimumGraphValue) / range);
                        if (yDP != -1 && !dataPoint.Value.HasValue)
                        {
                            xDP += pointWidth;
                        }
                    }
                    else
                    {
                        lastXdp = xDP;
                        lastYdp = yDP;
                        xDP += pointWidth;
                        yDP = graphHeight - Convert.ToSingle(graphHeight * (dataPoint.Value - minimumGraphValue) / range);
                        GraphLine graphLine = new GraphLine
                        {
                            Color = lineColor,
                            StrokeWidth = 3,
                            XPosStart = lastXdp,
                            YPosStart = yPos + padding + lastYdp,
                            XPosEnd = xDP,
                            YPosEnd = yPos + padding + yDP
                        };
                        if (previousDataPoint != null && previousDataPoint.Value.HasValue) // If previous data point exists, but is null, then skip drawing line
                        {
                            graphItems.Add(graphLine);
                        }
                        var lineDataPoint = dataPoint as LineDataPoint;
                        if (lineDataPoint != null && lineDataPoint.CircleType != CircleType.None)
                        {
                            float strokeWidth = 3;
                            graphItems.Add(new GraphCircle
                            {
                                Color = dataPoint.Color != null ? dataPoint.Color.Value : lineColor,
                                XPos = graphLine.XPosEnd,
                                YPos = graphLine.YPosEnd,
                                Radius = lineDataPoint.CircleRadius * scale,
                                PaintStyle = SKPaintStyle.Fill,
                                StrokeWidth = strokeWidth,
                            });
                            if (lineDataPoint.CircleType == CircleType.Donut)
                            {
                                graphItems.Add(new GraphCircle
                                {
                                    Color = ViewModel.BackgroundColor.HasValue ? ViewModel.BackgroundColor.Value : SKColors.White,
                                    XPos = graphLine.XPosEnd,
                                    YPos = graphLine.YPosEnd,
                                    Radius = (lineDataPoint.CircleRadius - strokeWidth) * scale,
                                    PaintStyle = SKPaintStyle.Fill,
                                    StrokeWidth = strokeWidth,
                                });
                            }
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
                    previousDataPoint = dataPoint;
                }
            }
        }

        protected virtual void DefineGraphAndGroupWidths(float canvasWidth, float graphHeight, float scale, float padding, float yPos, List<IDataPoint> dataPoints, List<string> YLabels, out float labelWidth, out float ySectionHeight, out float lineYPos, out float pointWidth, out float xPos, out float xLabelWidth)
        {
            SKPaint yLabelBrush;
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
            pointWidth = graphWidth / (ViewModel.DataPointCount - 1);
            foreach (var dg in ViewModel.DataGroups)
            {
                dg.GroupWidth = pointWidth * dg.Lines[0].DataPoints.Count;
            }
        }

        private float CalculateGraphHeight(float canvasHeight, float yPos, float padding, float footerHeight, float scale, out float xLabelYPos)
        {
            double labelHeight = 0;
            float maxLabelHeight = 0;
            bool usePointIdicators = false;

            foreach (var dataGroup in ViewModel.DataGroups)
            {
                if (string.IsNullOrWhiteSpace(dataGroup.Label))
                    continue;

                labelHeight = 0;
                SKPaint xLabelBrush = CreateXLabelBrush(scale); // Needed to determine graph width, taking into account Y-Label max width.
                switch (ViewModel.XLabelRotation)
                {
                    case LabelRotation.Vertical:
                        labelHeight = xLabelBrush.MeasureText(dataGroup.Label);
                        if (labelHeight > maxLabelHeight) maxLabelHeight = Convert.ToSingle(labelHeight);
                        break;
                    case LabelRotation.Angle:
                        float width = Convert.ToSingle(ViewModel.XLabelPointSize * 1.25 * scale);
                        float textWidth = xLabelBrush.MeasureText(dataGroup.Label);
                        labelHeight = Math.Sqrt(textWidth * textWidth / 2) + Math.Sqrt(width * width / 2);
                        if (labelHeight > maxLabelHeight) maxLabelHeight = Convert.ToSingle(labelHeight);
                        break;
                    case LabelRotation.Horizontal:
                    default:
                        foreach (string line in dataGroup.Label.Split('\n'))
                        {
                            labelHeight += (ViewModel.XLabelPointSize * 1.25f * scale);
                        }
                        if (labelHeight > maxLabelHeight) maxLabelHeight = Convert.ToSingle(labelHeight);
                        break;
                }
                usePointIdicators = usePointIdicators || dataGroup.Lines[0].DataPoints.Where(dp => dp.IndicatorLine == true).Any();
            }
            float graphHeight = canvasHeight - yPos - maxLabelHeight - padding - footerHeight - (usePointIdicators ? ViewModel.InidicatorLineLength * scale : 0);
            xLabelYPos = yPos + graphHeight + (usePointIdicators ? ViewModel.InidicatorLineLength * scale : 0);
            return graphHeight;
        }

        protected virtual void DrawLeftLabel(List<IGraphItem> graphItems, float graphHeight, float scale, float yPos)
        {
            if (ViewModel.LeftLabel == null || string.IsNullOrWhiteSpace(ViewModel.LeftLabel.Text))
                return;

            SKPaint leftLabelBrush = CreateLeftLabelBrush(scale); // Needed to determine graph width, taking into account Y-Label max width.
            var graphText = new GraphText
            {
                Rotation = 270,
                Alignment = ViewModel.LeftLabel.TextAlignment,
                Color = ViewModel.LeftLabel.Color,
                PointSize = ViewModel.LeftLabel.PointSize * scale,
                Text = ViewModel.LeftLabel.Text,
                XPos = ViewModel.LeftLabel.Height,
                Bold = ViewModel.LeftLabel.Bold
            };
            switch (ViewModel.LeftLabel.TextAlignment)
            {
                case TextAlignment.Start:
                    graphText.YPos = yPos + graphHeight;
                    break;
                case TextAlignment.Center:
                    graphText.YPos = yPos + graphHeight / 2;
                    break;
                case TextAlignment.End:
                    graphText.YPos = yPos;
                    break;
            }
            graphItems.Add(graphText);
        }

        protected virtual void DrawShadedSections(List<IGraphItem> graphItems, float graphHeight, float xPos, float yPos, float padding, float labelWidth)
        {
        }

        protected virtual void DrawVerticalShading(float padding, List<IGraphItem> graphItems, float yPos, List<string> YLabels, float labelWidth, float ySectionHeight, float xPos)
        {
            if (ViewModel.OddRowVerticalColor.HasValue)
            {
                float lineXPos = xPos;
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
                    lineXPos += ViewModel.DataGroups[i].GroupWidth;
                }
            }
        }

        protected virtual void DrawHorizontalLinesAndShading(float xPos, float xLabelWidth, float canvasWidth, float padding, List<IGraphItem> graphItems, List<string> YLabels, float labelWidth, float ySectionHeight, ref float lineYPos, ref int horizontalRow, List<decimal> hValues, ref float zeroYPos)
        {
            int i = 1;
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
                            XPos = xPos,
                            YPos = lineYPos - ySectionHeight
                        });
                    }
                    horizontalRow++;
                }
                if (ViewModel.HorizontalLineColor.HasValue)
                {
                    graphItems.Add(new GraphLine
                    {
                        Color = ViewModel.HorizontalLineColor.Value,
                        StrokeWidth = 2,
                        XPosStart = ViewModel.HorizontalLinesStartAtEdge ? padding + xLabelWidth : xPos,
                        YPosStart = lineYPos,
                        XPosEnd = canvasWidth - padding,
                        YPosEnd = lineYPos
                    });
                }
                if (hValues[i] == 0)
                {
                    zeroYPos = lineYPos;
                }
                i++;
            }
            if (ViewModel.HorizontalBottomLineColor.HasValue)
            {
                graphItems.Add(new GraphLine
                {
                    Color = ViewModel.HorizontalBottomLineColor.Value,
                    StrokeWidth = 2,
                    XPosStart = ViewModel.HorizontalLinesStartAtEdge ? padding + xLabelWidth : xPos,
                    YPosStart = lineYPos,
                    XPosEnd = canvasWidth - padding,
                    YPosEnd = lineYPos
                });
            }
        }

        protected virtual void DrawYAxisLabels(float scale, float padding, List<IGraphItem> graphItems, float yPos, List<string> YLabels, float labelWidth, float ySectionHeight, float xLabelWidth)
        {
            float lineYPos = yPos + padding;
            if (!ViewModel.HorizontalLinesStartAtEdge)
            {
                lineYPos += padding;
            }
            float xLabelPos = padding;
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
            xLabelPos += xLabelWidth;
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

        protected virtual void DrawVerticalLines(float padding, float xPos, List<IGraphItem> graphItems, float yPos, List<string> YLabels, float labelWidth, float ySectionHeight)
        {
            if (ViewModel.VerticalLineColor.HasValue)
            {
                float lineXPos = xPos;
                float yPosStart = yPos + padding * 2;
                float yPosEnd = yPos + padding + ySectionHeight * YLabels.Count;
                for (int i = 0; i < ViewModel.DataGroups.Count; i++)
                {
                    graphItems.Add(new GraphLine
                    {
                        Color = ViewModel.VerticalLineColor.Value,
                        StrokeWidth = 2,
                        XPosStart = lineXPos,
                        YPosStart = yPosStart,
                        XPosEnd = lineXPos,
                        YPosEnd = yPosEnd
                    });
                    lineXPos += ViewModel.DataGroups[i].GroupWidth;
                }
            }
            if (ViewModel.VerticalLeftAxisColor.HasValue)
            {
                float lineXPos = xPos;
                float yPosStart = yPos + padding * 2;
                float yPosEnd = yPos + padding + ySectionHeight * YLabels.Count;
                graphItems.Add(new GraphLine
                {
                    Color = ViewModel.VerticalLeftAxisColor.Value,
                    StrokeWidth = 2,
                    XPosStart = lineXPos,
                    YPosStart = yPosStart,
                    XPosEnd = lineXPos,
                    YPosEnd = yPosEnd
                });
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

        protected virtual SKPaint CreateXLabelBrush(float scale)
        {
            return graphEngine.GetTextBrush(new GraphText
            {
                Alignment = TextAlignment.Start,
                Color = ViewModel.XLabelColor,
                PointSize = ViewModel.XLabelPointSize * scale,
                Bold = false
            });
        }

        protected virtual SKPaint CreateLeftLabelBrush(float scale)
        {
            return graphEngine.GetTextBrush(new GraphText
            {
                Alignment = ViewModel.LeftLabel.TextAlignment,
                Color = ViewModel.LeftLabel.Color,
                PointSize = ViewModel.LeftLabel.PointSize * scale,
                Bold = ViewModel.LeftLabel.Bold
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

        protected virtual void YAxisRangeAndSections(out double minimum, out List<IDataPoint> dataPoints, out List<decimal> hValues, out decimal hValue, out double minimumGraphValue, out double maximumGraphValue)
        {
            minimum = ViewModel.BottomGraphValue;
            double maximum = ViewModel.TopGraphValue != 0 ? ViewModel.TopGraphValue : minimum;
            dataPoints = new List<IDataPoint>();
            minimumGraphValue = minimum;
            maximumGraphValue = 0;
            foreach (var dg in ViewModel.DataGroups)
            {
                foreach(var line in dg.Lines)
                {
                    foreach(var dp in line.DataPoints)
                    {
                        if (dp.Value.HasValue && dp.Value.Value < minimum) { minimum = dp.Value.Value; }
                        if (dp.Value.HasValue && dp.Value.Value > maximum) { maximum = dp.Value.Value; }
                    }
                }
            }
            hValues = new List<decimal>();
            hValue = 0;
            if (0 < minimum) hValue = Convert.ToDecimal(minimum);
            while (hValue <= (decimal)maximum)
            {
                hValues.Add(Convert.ToDecimal(hValue));
                hValue += ViewModel.HorizontalLabelPrecision;
            }
            hValues.Add(Convert.ToDecimal(hValue));
            maximumGraphValue = Convert.ToDouble(hValue);
            if (minimum < 0)
            {
                hValue = 0;
                while (true)
                {
                    if (hValue != 0)
                    {
                        hValues.Insert(0, Convert.ToDecimal(hValue));
                        minimumGraphValue = Convert.ToDouble(hValue);
                        if (hValue <= Convert.ToDecimal(minimum)) 
                            break;
                    }
                    hValue -= ViewModel.HorizontalLabelPrecision;
                }
            }
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
                float XPos = 0;
                switch (ViewModel.Title.TextAlignment)
                {
                    case TextAlignment.Start:
                        XPos = 40;
                        break;
                    case TextAlignment.Center:
                        XPos = Convert.ToInt32(canvasWidth / 2);
                        break;
                    case TextAlignment.End:
                        XPos = Convert.ToInt32(canvasWidth - 40);
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
                        XPos = XPos,
                        YPos = yPos,
                        Bold = ViewModel.Title.Bold
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