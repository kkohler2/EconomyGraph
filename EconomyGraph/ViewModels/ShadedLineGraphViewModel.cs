using EconomyGraph.Models;
using SkiaSharp;
using System;
using System.Collections.Generic;

namespace EconomyGraph.ViewModels
{
    public class ShadedLineGraphViewModel : LineGraphViewModel
    {
        public ShadedLineGraphViewModel()
        {
            // U.S. Recessions https://en.wikipedia.org/wiki/List_of_recessions_in_the_United_States
            ShadePeriods = new List<ShadePeriod>
            {
                new ShadePeriod
                {
                    StartDate = new DateTime(1929,8,1),
                    EndDate = new DateTime(1933,2,28)
                },
                new ShadePeriod
                {
                    StartDate = new DateTime(1937,5,1),
                    EndDate = new DateTime(1938,5,31)
                },
                new ShadePeriod
                {
                    StartDate = new DateTime(1945,2,1),
                    EndDate = new DateTime(1945,9,30)
                },
                new ShadePeriod
                {
                    StartDate = new DateTime(1948,10,1),
                    EndDate = new DateTime(1949,9,30)
                },
                new ShadePeriod
                {
                    StartDate = new DateTime(1953,7,1),
                    EndDate = new DateTime(1954,4,30)
                },
                new ShadePeriod
                {
                    StartDate = new DateTime(1957,8,1),
                    EndDate = new DateTime(1958,3,31)
                },
                new ShadePeriod
                {
                    StartDate = new DateTime(1960,4,1),
                    EndDate = new DateTime(1961,1,31)
                },
                new ShadePeriod
                {
                    StartDate = new DateTime(1969,12,1),
                    EndDate = new DateTime(1970,9,30)
                },
                new ShadePeriod
                {
                    StartDate = new DateTime(1973,11,1),
                    EndDate = new DateTime(1975,2,28)
                },
                new ShadePeriod
                {
                    StartDate = new DateTime(1980,1,1),
                    EndDate = new DateTime(1980,6,30)
                },
                new ShadePeriod
                {
                    StartDate = new DateTime(1981,7,1),
                    EndDate = new DateTime(1982,10,31)
                },
                new ShadePeriod
                {
                    StartDate = new DateTime(1990,7,1),
                    EndDate = new DateTime(1991,2,28)
                },
                new ShadePeriod
                {
                    StartDate = new DateTime(2001,3,1),
                    EndDate = new DateTime(2001,10,31)
                },
                new ShadePeriod
                {
                    StartDate = new DateTime(2007,12,1),
                    EndDate = new DateTime(2009,5,31)
                },
                new ShadePeriod
                {
                    StartDate = new DateTime(2020,2,1),
                    EndDate = new DateTime(2020,4,1)
                }
            };
        }
        /// <summary>
        /// Required only for ShadedBarGraph.  Not used otherwise.
        /// </summary>
        public DateTime StartDate { get; set; }
        public List<ShadePeriod> ShadePeriods { get; set; }
        public SKColor ShadedAreaColor { get; set; }
    }
}