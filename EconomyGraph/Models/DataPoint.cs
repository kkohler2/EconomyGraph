using SkiaSharp;
using System;

namespace EconomyGraph.Models
{
    public class DataPoint : IDataPoint
    {
        public DataPointLabel Label { get; set; }
        public double? Value { get; set; }
        /// <summary>
        /// Set color to override default
        /// </summary>
        public SKColor? Color { get; set; }
        public SKColor? NegativeColor { get; set; }
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Set to true to display short line under X-Axis above any label to indicate where data point is on graph
        /// </summary>
        public bool IndicatorLine { get; set; }
     }
}
