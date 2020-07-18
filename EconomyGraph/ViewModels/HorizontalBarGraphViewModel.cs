using EconomyGraph.Models;
using SkiaSharp;
using System.Collections.Generic;

namespace EconomyGraph.ViewModels
{
    public class HorizontalBarGraphViewModel
    {
        public SKColor? BackgroundColor { get; set; }
        public Label Title { get; set; }
        public Label SubTitle { get; set; }
        public Label LeftFooter { get; set; }
        #region RightFooter
        Label rightFooter;
        public Label RightFooter 
        { 
            get { return rightFooter; }
            set
            {
                value.TextAlignment = Xamarin.Forms.TextAlignment.End;
                rightFooter = value;
            }
        }
        #endregion
        public List<DataPoint> DataPoints { get; set; } = new List<DataPoint>();
    }
}
