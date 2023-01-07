using airLineReservationSystem.DBUtil;
using airLineReservationSystem.Models;
using Oracle.ManagedDataAccess.Client;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace airLineReservationSystem.Dao
{
	public class FlightDao
	{
        private static OracleConnection con = null;
        private static OracleCommand cmd = null;

        public static int AddFlight(Flight flight)
        {
            con = OracleDBConnection.getConnection();
            con.Open();
            cmd = OracleDBConnection.getCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into flights values('"+getNewFlightId1()+"','"+flight.seats+"','" + flight.From+ "','"+flight.To+ "','"+flight.At+ "','"+flight.Time+"')";
            cmd.Connection = con;
            int rowAffected=cmd.ExecuteNonQuery();
            con.Close();
            return rowAffected;
        }

        public static string getNewFlightId1()
        {
            string id = "";
            con = OracleDBConnection.getConnection();
            cmd = OracleDBConnection.getCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select count(*) from flights";
            cmd.Connection = con;
            OracleDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                id = "FL-" + (100 + (1 + reader.GetInt32(0)));
            }
            else
            {
                id = "FL-101";
            }
            return id;
        }

        public static string getNewFlightId()
        {
            string id = "";
            con = OracleDBConnection.getConnection();
            con.Open();
            cmd = OracleDBConnection.getCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select count(*) from flights";
            cmd.Connection = con;
            OracleDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                id = "FL-" + (100 + (1 + reader.GetInt32(0)));
            }
            else
            {
                id = "FL-101";
            }
            con.Close();
            return id;
        }

        public static List<Flight> getAllFLights()
        {
            List<Flight> flights = new List<Flight>();

            con = OracleDBConnection.getConnection();
            con.Open();
            cmd = OracleDBConnection.getCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from flights order by flightid";
            cmd.Connection = con;
            OracleDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                flights.Add(new Flight() { FlightId = reader.GetString(0),seats= reader.GetInt32(1), From = reader.GetString(2), To = reader.GetString(3), At = reader.GetString(4), Time = reader.GetString(5) });
            }
            con.Close();
            return flights;
        }

        public static Flight GetFlightDetailsById(string flightId)
        {
            Flight flight = null;
            con = OracleDBConnection.getConnection();
            con.Open();
            cmd = OracleDBConnection.getCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from flights where flightid='"+flightId+"'";
            OracleDataReader reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                flight= new Flight() { FlightId = reader.GetString(0), seats = reader.GetInt32(1), From = reader.GetString(2), To = reader.GetString(3), At = reader.GetString(4), Time = reader.GetString(5) };
            }
            con.Close();
            return flight;
        }

        public static int EditFlight(Flight flight)
        {
            flight.toString();
            con = OracleDBConnection.getConnection();
            con.Open();
            cmd = OracleDBConnection.getCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "update flights set seats="+flight.seats+",boardingfrom='" + flight.From + "',boardingto='" + flight.To + "',boardingtime='" + flight.At + "',flighttime='" + flight.Time + "' where flightid='"+flight.FlightId+"'";
            cmd.Connection = con;
            int rowAffected = cmd.ExecuteNonQuery();
            con.Close();
            return rowAffected;
        }

        public static List<Flight> searchFLights(Ticket booking)
        {
            Console.WriteLine("Inside search flight "+booking.From+"-"+booking.To);
            List<Flight> flights = new List<Flight>();

            con = OracleDBConnection.getConnection();
            con.Open();
            cmd = OracleDBConnection.getCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from flights where boardingfrom='"+ booking.From +"' and boardingto='" + booking.To + "' order by flightid";
            cmd.Connection = con;
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                flights.Add(new Flight() { FlightId = reader.GetString(0), seats = reader.GetInt32(1), From = reader.GetString(2), To = reader.GetString(3), At = reader.GetString(4), Time = reader.GetString(5) });
            }
            con.Close();
            return flights;
        }
    }
}

