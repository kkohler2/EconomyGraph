using SkiaSharp;
using System;

namespace EconomyGraph.Models
{
    public enum CircleType { None, Donut, Solid }
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
        /// <summary>
        /// Set to true to display short line under X-Axis above any label to indicate where data point is on graph
        /// </summary>
        public bool IndicatorLine { get; set; }
 
        #region Line Graphs Only
        public CircleType CircleType { get; set; } = CircleType.None;
        /// <summary>
        /// Only used if CircleType != None
        /// </summary>
        public float CircleRadius { get; set; }
        #endregion
    }
}
