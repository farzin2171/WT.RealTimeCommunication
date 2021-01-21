using System;

namespace WT.RealTime.Domain.Models
{
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime RealDateTime { get; set; }
        public DateTime RecivedDateTime { get; set; }
        public string Identifier { get; set; }

    }
}
