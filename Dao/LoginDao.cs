using airLineReservationSystem.DBUtil;
using airLineReservationSystem.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;

namespace airLineReservationSystem.Dao
{
    public class LoginDao
    {
        private static OracleConnection con = null;
        private static OracleCommand cmd = null;

        public static List<User> getAllUsers()
        {
            List<User> users = new List<User>();
            con = OracleDBConnection.getConnection();
            con.Open();
            cmd = OracleDBConnection.getCommand();
            cmd.CommandText = "select * from users";
            cmd.Connection = con;
            OracleDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                Console.WriteLine("Inside Loop");
                users.Add(new User() { UserId = (string)reader["UserId"], Name = (string)reader["Name"], Mobile = (string)reader["Mobile"], Type = (string)reader["Type"] });
            }
            con.Close();
            return users;
            
        }
        public static int AddUser(User user)
        {
            con = OracleDBConnection.getConnection();
            con.Open();
            cmd = OracleDBConnection.getCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into users values('"+user.UserId+ "','"+user.Name+ "','"+user.Mobile+ "','"+user.Password+"','User')";
            cmd.Connection = con;
            int row= cmd.ExecuteNonQuery();
            con.Close();
            return row;
        }

        public static User validteUser(string userName,string password)
        {
            User user1 = null;

            con = OracleDBConnection.getConnection();
            con.Open();
            cmd = OracleDBConnection.getCommand();
            cmd.CommandType =System.Data.CommandType.Text;
            cmd.CommandText = "select * from users where userid='" + userName + "' and password='" + password + "'";
            cmd.Connection = con;
            OracleDataReader reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                user1 = new User() { UserId = (string)reader["UserId"], Name = (string)reader["Name"], Mobile = (string)reader["Mobile"], Type = (string)reader["Type"] };
            }
            con.Close();
            return user1; ;
        }
    }
}
