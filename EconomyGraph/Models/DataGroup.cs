using System;
using System.Collections.Generic;

namespace EconomyGraph.Models
{
    public class DataGroup
    {
        public string Label { get; set; }
        /// <summary>
        /// Required for ShadingGraphs.  Must have date per data point and be in calendar order! 
        /// </summary>
        public List<IDataPoint> DataPoints { get; set; } = new List<IDataPoint>();
        /// <summary>
        /// Date range needed for "recession" shading.
        /// </summary>
        // For internal use.  Do NOT set.
        public float GroupWidth { get; set; }
    }
}
