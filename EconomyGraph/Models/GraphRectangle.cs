using SkiaSharp;

namespace EconomyGraph.Models
{
    public class GraphRectangle : IGraphItem
    {
        public PaintStyle Style { get; set; }
        public SKColor Color { get; set; }
        public float XPos { get; set; }
        public float YPos { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
    }
}