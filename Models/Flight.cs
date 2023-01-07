using System;
namespace airLineReservationSystem.Models
{
    public class Flight
    {
        public string? FlightId { get; set; }
        public int? seats { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public string? At { get; set; }
        public string? Time { get; set; }


        public void toString()
        {
            Console.WriteLine("Flightid= "+this.FlightId+" - Flightid= " + this.seats + " - Flightid = " + this.At);
        }       
	}
}

