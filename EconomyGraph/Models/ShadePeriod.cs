using System;

namespace EconomyGraph.Models
{
    /// <summary>
    /// Essentially, the date range marks time period on graph for shaded area (i.e. recession),
    /// if date range is partially/completely in graph time period.
    /// </summary>
    public class ShadePeriod
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
