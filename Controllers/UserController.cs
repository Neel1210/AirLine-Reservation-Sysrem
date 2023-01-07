using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using airLineReservationSystem.Dao;
using airLineReservationSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace airLineReservationSystem.Controllers
{
    public class UserController : Controller
    {
        private static Ticket temp=new Ticket();
        private static User userr=new User();
        // GET: /<controller>/
        public IActionResult Index(User user)
        {
            userr.Name= user.Name;
            userr.Mobile = user.Mobile;
            temp.PessengerName = user.Name;
            temp.Mobile = user.Mobile;
            return View(user);
        }

        public IActionResult SearchFlight()
        {
            return View();
        }

        public IActionResult ViewAvailableFlights(Ticket ticket)
        {
            temp.From = ticket.From;
            temp.To = ticket.To;
            temp.date = ticket.date;

            List<Flight> flights = FlightDao.searchFLights(ticket);
            flights=availableSeats(flights, ticket);
            Console.WriteLine("Size -" + flights.Count);
            return View(flights);
        }

        public List<Flight> availableSeats(List<Flight> flights, Ticket ticket)
        {
            Console.WriteLine("Size -" + flights.Count);
            int i = 0;
            int total = flights.Count;
            while (i < total)
            {
                Flight f = flights.ElementAt(i);
                Console.WriteLine("value of i" + i);
                Console.WriteLine("Inside Loop Before Call" + f.seats);
                Flight F = TicketDao.CheckAvailableSeats(f, ticket);
                Console.WriteLine("Inside Loop After Call" + f.seats);
                flights.RemoveAt(i);
                flights.Insert(i, F);
                i = i + 1;
            }
            return flights;
        }
        
        public IActionResult TicketBooking(Ticket ticket,string flightId,int seats)
        {
            TicketDao.setFlightDetailInTicket(temp,flightId);
            temp.TicketNo = setTicketNo();
            temp.status = "CNF";
            return View(temp);
        }

        public string setTicketNo()
        {   //TK_101_0912
            int count = 0,seatNo=1;
            count = TicketDao.getTicketCount(temp.date);
            seatNo = TicketDao.getSeatNo(temp.date,temp.FlightId);
            temp.seatNo=seatNo+1;
            return "TK_" + (101 + count) + "_"+temp.date;    
        }
         
        public IActionResult ViewAllBookedTickets(User user)
        {
            List<Ticket> tickets = TicketDao.getAllBookedTicket(user.Name, user.Mobile);
            return View(tickets);
        }

        /* public IActionResult ViewAllBookedTickets(string mobile, string name)
         {
             List<Ticket> tickets = TicketDao.getAllBookedTicket(name, mobile);
             return View(tickets);
         }*/

        public IActionResult viewAll()
        {
            return RedirectToAction("ViewAllBookedTickets", userr);
        }

        public IActionResult back()
        {
            return RedirectToAction("Index",userr);
        }
        [HttpPost]
        public IActionResult ViewAllBookedTickets()
        {
            TicketDao.AddTicket(temp);
            List<Ticket> tickets = TicketDao.getAllBookedTicket(temp.PessengerName,temp.Mobile);
            return View(tickets);
        }
    }
}

