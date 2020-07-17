using SkiaSharp;

namespace EconomyGraph.ViewModels
{
    public class ShadedBarGraphViewModel : ShadedLineGraphViewModel
    {

        // Set BarDefaultWidth to define bar width.  If not set, width is auto-calculated.  If bars of BarDefaultWidth do not fit space, it will
        // be ignored and auto-calculated value used.
        public float? BarDefaultWidth { get; set; }
        public SKColor BarColor { get; set; } = SKColors.Blue;
        public SKColor NegativeBarColor { get; set; } = SKColors.Red;
    }
}