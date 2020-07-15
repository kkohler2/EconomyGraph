using System;
using System.Collections.Generic;

namespace EconomyGraph.Models
{
    public class DataGroup
    {
        public string Label { get; set; }
        public List<double> DataPoints { get; set; } = new List<double>();
        /// <summary>
        /// Date range needed for "recession" shading.
        /// </summary>
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        // For internal use.  Do NOT set.
        public float GroupWidth { get; set; }
    }
}
