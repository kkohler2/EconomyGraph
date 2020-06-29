using SkiaSharp;
using Xamarin.Forms;

namespace EconomyGraph
{
    public class GraphText : IGraphItem
    {
        public SKColor Color { get; set; }
        public float PointSize { get; set; }
        public TextAlignment Alignment { get; set; }
        public string Text { get; set; }
        public float XPos { get; set; }
        public float YPos { get; set; }
        /// <summary>
        /// Default = 0 for no rotation
        /// </summary>
        public float Rotation { get; set; }
        public bool Bold { get; set; }
    }
}