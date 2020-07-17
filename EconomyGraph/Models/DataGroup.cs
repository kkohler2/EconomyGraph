using System;
using System.Collections.Generic;

namespace EconomyGraph.Models
{
    public class DataGroup
    {
        public string Label { get; set; }
        // Required for ShadingGraphs.  Must have date per data point and be in calendar order!
        public List<DateTime> EndDates { get; set; }
        public List<double> DataPoints { get; set; } = new List<double>();
        /// <summary>
        /// Date range needed for "recession" shading.
        /// </summary>
        // For internal use.  Do NOT set.
        public float GroupWidth { get; set; }
    }
}
