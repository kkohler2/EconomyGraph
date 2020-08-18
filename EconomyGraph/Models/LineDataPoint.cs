namespace EconomyGraph.Models
{
    public class LineDataPoint : DataPoint
    {
        public CircleType CircleType { get; set; } = CircleType.None;
        /// <summary>
        /// Only used if CircleType != None
        /// </summary>
        public float CircleRadius { get; set; }
    }
}
