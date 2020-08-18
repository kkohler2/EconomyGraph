using SkiaSharp;
using System;

namespace EconomyGraph.Models
{
    public enum CircleType { None, Donut, Solid }
    public interface IDataPoint
    {
        DataPointLabel Label { get; set; }
        double? Value { get; set; }
        /// <summary>
        /// Set color to override default
        /// </summary>
        SKColor? Color { get; set; }
        SKColor? NegativeColor { get; set; }
        DateTime EndDate { get; set; }
        /// <summary>
        /// Set to true to display short line under X-Axis above any label to indicate where data point is on graph
        /// </summary>
        bool IndicatorLine { get; set; }
    }
}
