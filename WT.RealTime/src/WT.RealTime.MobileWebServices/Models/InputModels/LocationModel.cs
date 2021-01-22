using System;

namespace WT.RealTime.MobileWebServices.Models.InputModels
{
    public class LocationModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime RealDateTime { get; set; }
        public DateTime RecivedDateTime { get; set; }
        public string Identifier { get; set; }
    }
}
