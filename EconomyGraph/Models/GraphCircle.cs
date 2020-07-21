using SkiaSharp;

namespace EconomyGraph.Models
{
    public class GraphCircle : IGraphItem
    {
        public SKColor Color { get; set; }
        public float XPos { get; set; }
        public float YPos { get; set; }
        public float Radius { get; set; }
        public SKPaintStyle PaintStyle { get; set; } // Fill, Stroke, StrokeAndFill???
        public float StrokeWidth { get; set; }
    }
}
