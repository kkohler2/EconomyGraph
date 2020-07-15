using EconomyGraph.Models;
using SkiaSharp;
using System.Collections.Generic;

namespace EconomyGraph.ViewModels
{
    public class LineGraphViewModel
    {
        public SKColor? BackgroundColor { get; set; }
        #region Title
        Label title;
        public Label Title
        {
            get { return title; }
            set
            {
                value.Bold = true;
                title = value;
            }
        }
        #endregion

        public Footer LeftFooter { get; set; }
        public Footer CenterFooter { get; set; }
        public Footer RightFooter { get; set; }
        public string YFirstLabelFormat { get; set; }
        public string YLabelFormat { get; set; }
        public float YLabelPointSize { get; set; }
        public Xamarin.Forms.TextAlignment YLabelAlignment { get; set; } = Xamarin.Forms.TextAlignment.Start;
        public SKColor YLabelColor { get; set; } = SKColors.Black;
        public bool HorizontalLines { get; set; }
        public decimal HorizontalLabelPrecision { get; set; }
        public bool VerticalLines { get; set; }
        public Xamarin.Forms.TextAlignment XLabelAlignment { get; set; } = Xamarin.Forms.TextAlignment.Start;
        /// <summary>
        /// To use shaded rows, set background color to color for "even" horizonal rows (w/ title being essentially row 0).
        /// Then set OddRowHorizontalColor to color to use for "odd" horizontal rows.  Do NOT set OddVerticalColor
        /// </summary>
        public SKColor? OddRowHorizontalColor { get; set; }
        public SKColor XLabelColor { get; set; } = SKColors.Black;
        public float XLabelPointSize { get; set; }
        /// <summary>
        /// Defaults to zero or lowest value if negative.  If higher starting point is desired, set this > 0.
        /// </summary>
        public double BottomGraphValue { get; set; } // Ignored if bottom graph value below BottomGraphValue
        public double TopGraphValue { get; set; } // Ignored if top graph value above TopGraphValue
        /// <summary>
        /// To use shaded rows, set background color to color for "even" vertical rows (w/ labels being essentially row 0).
        /// Then set OddRowVerticalColor to color to use for "odd" vertical rows.  Do NOT set OddRowHorizontalColor
        /// </summary>
        public SKColor? OddRowVerticalColor { get; set; }
        public SKColor LineColor { get; set; }
        public List<DataGroup> DataGroups { get; set; } = new List<DataGroup>();
        public float Padding { get; set; } = 5;
    }
}
