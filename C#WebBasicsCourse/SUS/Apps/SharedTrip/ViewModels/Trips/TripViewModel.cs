using System;
using System.Globalization;

namespace SharedTrip.ViewModels.Trips
{
    public class TripViewModel
    {
        public string Id { get; set; }
        
        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        public DateTime DepartureTime { get; set; }

        public string DepartureTimeAsString => DepartureTime.ToString(CultureInfo.GetCultureInfo("bg-BG"));

        public int Seats { get; set; }

        public int AvailableSeats => this.Seats - this.UsedSeats;

        public int UsedSeats { get; set; }
    }
}
