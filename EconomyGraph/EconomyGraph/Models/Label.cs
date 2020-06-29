using SkiaSharp;
using System.Collections.Generic;
using Xamarin.Forms;

namespace EconomyGraph.Models
{
    public class Label
    {
        public string Text { get; set; }
        public SKColor Color { get; set; }
        public int PointSize { get; set; } = -1;
        public bool Bold { get; set; }
        public TextAlignment TextAlignment { get; set; } = TextAlignment.Start;
        private float _scale = 1;
        public float Scale
        {
            set { _scale = value; }
        }
        public float Height
        {
            get { return PointSize * 1.25f * _scale; }
        }
        // Top - xPos (of drawing point) - PointSiuze * _scale
        public float Top(float xPos) { return xPos - PointSize * _scale; }
    }
}
