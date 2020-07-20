using SkiaSharp;
using System;

namespace EconomyGraph.Models
{
    public class DataPoint
    {
        public DataPointLabel Label { get; set; }
        public double Value { get; set; }
        /// <summary>
        /// Set color to override default
        /// </summary>
        public SKColor? Color { get; set; }
        public SKColor? NegativeColor { get; set; }
        public DateTime EndDate { get; set; }
    }
}
