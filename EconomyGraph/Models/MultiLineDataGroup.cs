using System.Collections.Generic;

namespace EconomyGraph.Models
{
    public class MultiLineDataGroup
    {
        public string Label { get; set; }
        public List<Line> Lines { get; set; } = new List<Line>();
        public float GroupWidth { get; set; }
    }
}