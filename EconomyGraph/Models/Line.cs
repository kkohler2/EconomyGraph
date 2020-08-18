using SkiaSharp;
using System.Collections.Generic;

namespace EconomyGraph.Models
{
    public class Line
    {
        public SKColor LineColor { get; set; }
        public List<IDataPoint> DataPoints { get; set; } = new List<IDataPoint>();
    }
}