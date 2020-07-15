using SkiaSharp;

namespace EconomyGraph.Models
{
    public class GraphLine : IGraphItem
    {
        public SKColor Color { get; set; }
        public float StrokeWidth { get; set; }
        public float XPosStart { get; set; }
        public float YPosStart { get; set; }
        public float XPosEnd { get; set; }
        public float YPosEnd { get; set; }
    }
}