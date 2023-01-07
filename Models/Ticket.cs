using System;
namespace airLineReservationSystem.Models
{
	public class Ticket
	{
        public string? TicketNo { get; set; } 
        public string? PessengerName { get; set; }
        public string? Mobile { get; set; }
        public string? FlightId { get; set; }
        public string? date { get; set; }
        public    int? seatNo { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public string? At { get; set; }
        public string? Time { get; set; }
        public string? status { get; set; }

        public Ticket()
        {

        }
    }
}

