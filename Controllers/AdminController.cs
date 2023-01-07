using airLineReservationSystem.Dao;
using airLineReservationSystem.DBUtil;
using airLineReservationSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace airLineReservationSystem.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index(User user)
        {
            return View(user);
        }

        public IActionResult EditFlight()
        {
            return View();
        }

        public IActionResult EditFlight1(string flightId)
        { 
            Flight flight = FlightDao.GetFlightDetailsById(flightId);
            return View(flight);
        }

        public IActionResult EditFlight2(Flight flight ,string flightId)
        {
            flight.FlightId = flightId;
            FlightDao.EditFlight(flight);
            return RedirectToAction("EditFlight", "Admin");
        }

        public IActionResult ViewAllFlights()
        {
            return View();
        }

        public IActionResult AddFlight()
        {
            Flight flight = new Flight() { FlightId= FlightDao.getNewFlightId() };
            return View(flight);
        }

        [HttpPost]
        public IActionResult AddFlightInDB(Flight flight)
        {
            
            int x = FlightDao.AddFlight(flight);
            if (x>0)
            {
                return RedirectToAction("ViewAllFlights","Admin");
            }

            return RedirectToAction("Index", "Admin");
        }
    }
}
