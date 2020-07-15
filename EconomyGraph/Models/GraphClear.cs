using SkiaSharp;

namespace EconomyGraph.Models
{
    /// <summary>
    /// Must be first item in IGraphItem list to work properly.
    /// </summary>
    public class GraphClear : IGraphItem
    {
        public SKColor Color { get; set; }
    }
}
