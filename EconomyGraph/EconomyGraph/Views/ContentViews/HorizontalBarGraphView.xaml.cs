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
    public partial class HorizontalBarGraphView : ContentView
    {
        public static readonly BindableProperty GraphWidthProperty =
            BindableProperty.Create("GraphWidth", typeof(double), typeof(HorizontalBarGraphView), null, BindingMode.OneTime);

        public static readonly BindableProperty GraphHeightProperty =
            BindableProperty.Create("GraphHeight", typeof(double), typeof(HorizontalBarGraphView), null, BindingMode.OneTime);

        public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create("ViewModel", typeof(HorizontalBarGraphViewModel), typeof(HorizontalBarGraphView), null, BindingMode.OneTime);

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

        public HorizontalBarGraphViewModel ViewModel
        {
            get { return (HorizontalBarGraphViewModel)GetValue(ViewModelProperty); }
            set { SetValue(GraphHeightProperty, value); }
        }

        readonly GraphEngine graphEngine;
        public HorizontalBarGraphView()
        {
            graphEngine = new GraphEngine();
            InitializeComponent();
        }

        private void PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            float width = e.Info.Width;
            float height = e.Info.Height;
            float scale = height / (float)GraphHeight;
            List<IGraphItem> graphItems = new List<IGraphItem>();
            if (ViewModel.BackgroundColor.HasValue)
            {
                graphItems.Add(new GraphClear { Color = ViewModel.BackgroundColor.Value });
            }
            if (ViewModel.Title != null && !string.IsNullOrWhiteSpace(ViewModel.Title.Text))
            {
                int xPos = 0;
                switch(ViewModel.Title.TextAlignment)
                {
                    case TextAlignment.Start:
                        xPos = 40;
                        break;
                    case TextAlignment.Center:
                        xPos = Convert.ToInt32(width / 2);
                        break;
                    case TextAlignment.End:
                        xPos = Convert.ToInt32(width - 40);
                        break;
                }
                graphItems.Add(new GraphText
                {
                    Alignment = ViewModel.Title.TextAlignment,
                    Color = ViewModel.Title.Color,
                    PointSize = ViewModel.Title.PointSize * scale,
                    Text = ViewModel.Title.Text, // "Do you support or oppose 'defund the police'?",
                    XPos = xPos,
                    YPos = 45,
                    Bold = ViewModel.Title.Bold // true;
                });
            }
            if (ViewModel.SubTitle != null && !string.IsNullOrWhiteSpace(ViewModel.SubTitle.Text))
            {
                int xPos = 0;
                switch (ViewModel.Title.TextAlignment)
                {
                    case TextAlignment.Start:
                        xPos = 40;
                        break;
                    case TextAlignment.Center:
                        xPos = Convert.ToInt32(width / 2);
                        break;
                    case TextAlignment.End:
                        xPos = Convert.ToInt32(width - 40);
                        break;
                }
                graphItems.Add(new GraphText
                {
                    Alignment = ViewModel.SubTitle.TextAlignment,
                    Color = ViewModel.SubTitle.Color,
                    PointSize = ViewModel.SubTitle.PointSize * scale,
                    Text = ViewModel.SubTitle.Text,
                    XPos = xPos,
                    YPos = 85,
                    Bold = ViewModel.SubTitle.Bold
                });
            }
            if (ViewModel.LeftFooter != null && !string.IsNullOrWhiteSpace(ViewModel.LeftFooter.Text))
            {
                graphItems.Add(new GraphText
                {
                    Alignment = ViewModel.LeftFooter.TextAlignment,
                    Color = ViewModel.LeftFooter.Color,
                    PointSize = ViewModel.LeftFooter.PointSize * scale,
                    Text = ViewModel.LeftFooter.Text,
                    XPos = 40,
                    YPos = Convert.ToInt32(height * 0.9),
                    Bold = ViewModel.LeftFooter.Bold
                });
            }
            if (ViewModel.RightFooter != null && !string.IsNullOrWhiteSpace(ViewModel.RightFooter.Text))
            {
                graphItems.Add(new GraphText
                {
                    Alignment = ViewModel.RightFooter.TextAlignment,
                    Color = ViewModel.RightFooter.Color,
                    PointSize = ViewModel.RightFooter.PointSize * scale,
                    Text = ViewModel.RightFooter.Text,
                    XPos = Convert.ToInt32(width * 0.9),
                    YPos = Convert.ToInt32(height * 0.9),
                    Bold = ViewModel.RightFooter.Bold
                });
            }
            graphItems.Add(new GraphLine
            {
                Color = SKColors.Black,
                StrokeWidth = 2,
                XPosStart = 40,
                YPosStart = 140,
                XPosEnd = Convert.ToInt32(width * 0.9),
                YPosEnd = 140
            });
            int index = 0;
            int max = 0;
            foreach(var dataPoint in ViewModel.DataPoints)
            {
                index++;
                if (dataPoint.Value > max) max = dataPoint.Value;
                graphItems.Add(new GraphText
                {
                    Alignment = TextAlignment.End,
                    Color = dataPoint.TextColor,
                    PointSize = dataPoint.PointSize * scale,
                    Text = dataPoint.Label,
                    XPos = 145,
                    YPos = Convert.ToInt32(height * (0.25 + 0.2 * index)),
                    Bold = dataPoint.Bold
                });
            }
            int lineMax = (Convert.ToInt32((max + 10) / 10) * 10);
            int lines = lineMax / 10;
            float lineWidth = Convert.ToInt32(width * 0.9) - 180 + 1;
            float lineSpacing = lineWidth / lines;
            AddLines(graphItems, 180, 140, Convert.ToInt32(height * 0.8), lineMax, lineSpacing, (float)scale);
            index = 0;
            foreach (var dataPoint in ViewModel.DataPoints)
            {
                index++;
                graphItems.Add(new GraphLine
                {
                    Color = dataPoint.Color,
                    StrokeWidth = 40 * scale,
                    XPosStart = 180,
                    YPosStart = Convert.ToInt32(height * (0.25 + 0.2 * index)),
                    XPosEnd = lineWidth * dataPoint.Value / lineMax + 180,
                    YPosEnd = Convert.ToInt32(height * (0.25 + 0.2 * index)),
                });
                graphItems.Add(new GraphText
                {
                    Alignment = TextAlignment.Start,
                    Color = dataPoint.Color,
                    PointSize = 20 * scale,
                    Text = dataPoint.Value.ToString() + "%",
                    XPos = lineWidth * dataPoint.Value / lineMax + 190,
                    YPos = Convert.ToInt32(height * (0.25 + 0.2 * index)),
                    Bold = true
                });
            }
            graphEngine.Draw(e.Surface, graphItems);
        }

        void AddLines(List<IGraphItem> graphItems, int xStart, int yStart, int yEnd, int lineMax, float width, float scaleY)
        {
            int line = 0;
            for (int i = 0; i < lineMax; i += 10)
            {
                graphItems.Add(new GraphText
                {
                    Alignment = TextAlignment.Center,
                    Color = SKColors.Black,
                    PointSize = 20 * scaleY,
                    Text = i.ToString(),
                    XPos = xStart + (line * width),
                    YPos = yStart - 20,
                });
                graphItems.Add(new GraphLine
                {
                    Color = SKColors.Black,
                    StrokeWidth = 2,
                    XPosStart = xStart + (line * width),
                    YPosStart = yStart,
                    XPosEnd = xStart + (line * width),
                    YPosEnd = yEnd
                });
                line++;
            }
        }
    }
}