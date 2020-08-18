using EconomyGraph.Models;
using SkiaSharp;
using System.Collections.Generic;

namespace EconomyGraph.ViewModels
{
    public class MultiLineGraphViewModel
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
        /// Only Horizontal rotation supports multiple lines
        /// </summary>
        public LabelRotation XLabelRotation { get; set; } = LabelRotation.Horizontal;
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
        public List<MultiLineDataGroup> DataGroups { get; set; } = new List<MultiLineDataGroup>();
        public float Padding { get; set; } = 5;
        public float InidicatorLineLength { get; set; } = 10;
        public int DataPointCount
        {
            get
            {
                int count = 0;
                foreach (var dataGroup in DataGroups)
                {
                    count += dataGroup.Lines[0].DataPoints.Count;
                }
                return count;
            }
        }
    }
}
