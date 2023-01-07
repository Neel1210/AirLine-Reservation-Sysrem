using System;
using airLineReservationSystem.DBUtil;
using System.Drawing;
using airLineReservationSystem.Models;
using Oracle.ManagedDataAccess.Client;
using System.Reflection;

namespace airLineReservationSystem.Dao
{
	public class TicketDao
	{
        private static OracleConnection con = null;
        private static OracleCommand cmd = null;

        public static void setFlightDetailInTicket(Ticket temp,string flightId)
		{
			Flight flight = FlightDao.GetFlightDetailsById(flightId);
			temp.FlightId = flight.FlightId;
			temp.From = flight.From;
            temp.To = flight.To;
            temp.At = flight.At;
            temp.Time = flight.Time;
        }


        public static Flight CheckAvailableSeats(Flight f,Ticket ticket)
        {
            string date = ticket.date;
            con = OracleDBConnection.getConnection();
            con.Open();
            cmd = OracleDBConnection.getCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select count(*) from ticket where boardingfrom='" + f.From + "'" +
            "and boardingto='" + f.To + "'" +
            "and boardingdate='" + date + "'" +
            "and flightid='" + f.FlightId + "'";
            cmd.Connection = con;
            OracleDataReader reader = cmd.ExecuteReader();
            if(reader.Read())
                f.seats = f.seats - reader.GetInt32(0);
            con.Close();
            return f;
        }

        public static int getTicketCount(string date)
        {
            int no = 0;
            con = OracleDBConnection.getConnection();
            con.Open();
            cmd = OracleDBConnection.getCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select count(*) from ticket where "+
            "boardingdate='" + date + "'";
            cmd.Connection = con;
            OracleDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
               no=int.Parse(reader.GetString(0));
            con.Close();
            return no;
        }

        public static int getSeatNo(string date,string flightId)
        {
            int no = 0;
            con = OracleDBConnection.getConnection();
            con.Open();
            cmd = OracleDBConnection.getCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select count(*) from ticket where " +
            "boardingdate='" + date + "'"+
            " and flightid='" + flightId + "'"; ;
            cmd.Connection = con;
            OracleDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                no = int.Parse(reader.GetString(0));
            con.Close();
            return no;
        }

        public static int AddTicket(Ticket ticket)
        {
            con = OracleDBConnection.getConnection();
            con.Open();
            cmd = OracleDBConnection.getCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into ticket values(" +
                "'" + ticket.TicketNo + "'," +
                "'" + ticket.PessengerName + "'," +
                "'" + ticket.Mobile + "'," +
                "'" + ticket.FlightId + "'," +
                "'" + ticket.date + "'," +
                "'" + ticket.seatNo + "'," +
                "'" + ticket.From + "'," +
                "'" + ticket.To + "'," +
                "'" + ticket.At + "'," +
                "'" + ticket.Time + "'," +
                "'" + ticket.status + "')";
            cmd.Connection = con;
            int rowAffected = cmd.ExecuteNonQuery();
            con.Close();
            return rowAffected;
        }

        public static List<Ticket> getAllBookedTicket(string pessengerName,string phone)
        {
            List<Ticket> tickets = new List<Ticket>();

            con = OracleDBConnection.getConnection();
            con.Open();
            cmd = OracleDBConnection.getCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from Ticket where pessengername='"+ pessengerName + "' and mobile='"+ phone + "' order by boardingdate";
            cmd.Connection = con;
            OracleDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                Ticket t = new Ticket();
                t.TicketNo = reader.GetString(0);
                t.PessengerName = reader.GetString(1);
                t.Mobile = reader.GetString(2);
                t.FlightId = reader.GetString(3);
                t.date = reader.GetString(4);
                t.seatNo =int.Parse(reader.GetString(5));
                t.From = reader.GetString(6);
                t.To = reader.GetString(7);
                t.At = reader.GetString(8);
                t.Time = reader.GetString(9);
                t.status = reader.GetString(10);
                tickets.Add(t);
            }
            con.Close();
            return tickets;
        }

    }
}

