using EconomyGraph.Models;
using SkiaSharp;
using System.Collections.Generic;

namespace EconomyGraph.ViewModels
{
    public class HorizontalBarGraphViewModel
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
