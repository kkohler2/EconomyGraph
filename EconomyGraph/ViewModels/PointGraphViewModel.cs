using EconomyGraph.Models;
using SkiaSharp;

namespace EconomyGraph.ViewModels
{
    public class PointGraphViewModel : LineGraphViewModel
    {
        public CircleType CircleType { get; set; } = CircleType.None;
        /// <summary>
        /// Only used if CircleType != None
        /// </summary>
        public float CircleRadius { get; set; }
        public SKColor PointColor { get; set; }
        public double? MaxXValue { get; set; }
    }
}
