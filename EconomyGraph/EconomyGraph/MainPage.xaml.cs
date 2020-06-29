using EconomyGraph.ViewModels;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EconomyGraph
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        GraphEngine graphEngine;
        public MainPage()
        {
            graphEngine = new GraphEngine();
            BindingContext = new MainPageViewModel();
            InitializeComponent();
        }

        private void canvalView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            List<IGraphItem> graphItems = new List<IGraphItem>();
            graphItems.Add(new GraphClear { Color = SKColors.AliceBlue});
            graphItems.Add(new GraphLine
            {
                Color = SKColors.Black,
                StrokeWidth = 25.1f,
                XPosStart = 0,
                YPosStart = 200,
                XPosEnd = 500,
                YPosEnd = 200
            });
            graphItems.Add(new GraphRectangle
            {
                Color = SKColors.Red,
                Height = 40,
                Width = e.Info.Width - 20,
                Style = PaintStyle.Fill,
                XPos = 0,
                YPos = 300
            });
            graphItems.Add(new GraphText
            {
                Alignment = TextAlignment.Start,
                Color = SKColors.Green,
                PointSize = 30,
                Text = "This is a test",
                XPos = 50,
                YPos = 150,
                Rotation = -45
            });
            graphEngine.Draw(e.Surface, graphItems);
        }
    }
}