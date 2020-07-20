using EconomyGraph.Models;
using SkiaSharp;
using System.Collections.Generic;

namespace EconomyGraph.ViewModels
{
    public class LineGraphViewModel
    {
        public SKColor? BackgroundColor { get; set; }
        public Label Title { get; set; }
        public Footer LeftFooter { get; set; }
        public Footer CenterFooter { get; set; }
        public Footer RightFooter { get; set; }
        /// <summary>
        /// Displayed vertically bottom to top, if defined.
        /// </summary>
        public Label LeftLabel { get; set; }
        public string YFirstLabelFormat { get; set; }
        public string YLabelFormat { get; set; }
        public float YLabelPointSize { get; set; }
        public Xamarin.Forms.TextAlignment YLabelAlignment { get; set; } = Xamarin.Forms.TextAlignment.Start;
        /// <summary>
        /// Set VerticalLeftAxisColor to draw Y-Axis line at left of graph.  This will override first Vertical line, if vertical lines are 
        /// displayed and will display be displayed, even if not using Vertical Lines.
        /// </summary>
        public SKColor? VerticalLeftAxisColor { get; set; }
        /// <summary>
        /// Set VerticalLineColor to draw Y-Axis lines at start of graph and end of every data group.  First line can be overridden by VerticalLeftAxisColor. 
        /// </summary>
        public SKColor? VerticalLineColor { get; set; }
        public SKColor YLabelColor { get; set; } = SKColors.Black;
        /// <summary>
        /// If true and displaying horizontal lines, they start at left edit of canvas, i.e. under label.  If false, they start at edit of graph.
        /// </summary>
        public bool HorizontalLinesStartAtEdge = true;
        public SKColor? HorizontalLineColor { get; set; }
        /// <summary>
        /// Set HorizontalBottomLineColor to draw X-Axis line at bottom of graph.  This will override first bottom horizontal line, if horizontal lines are 
        /// displayed and will display be displayed, even if not using Horizontal Lines.
        /// </summary>
        public SKColor? HorizontalBottomLineColor { get; set; }
        public decimal HorizontalLabelPrecision { get; set; }
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
