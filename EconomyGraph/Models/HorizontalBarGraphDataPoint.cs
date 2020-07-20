using SkiaSharp;

namespace EconomyGraph.Models
{
    /// <summary>
    /// For HorizontalBarGraph
    /// </summary>
    public class HorizontalBarGraphDataPoint
    {
        public string Label { get; set; }
        public SKColor TextColor { get; set; }
        public bool Bold { get; set; }
        public float PointSize { get; set; }
        public int Value { get; set; }
        public SKColor Color { get; set; }
    }
}
