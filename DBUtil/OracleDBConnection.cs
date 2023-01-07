using System;
using Oracle.ManagedDataAccess.Client;
namespace airLineReservationSystem.DBUtil
{
    public class OracleDBConnection
    {
        private static OracleConnection con = null;
        private static OracleCommand cmd = null;

        
        private static string connectionString = " User Id = airlines ; password=Reservation123456;Connection Timeout = 30; " +
            "Data Source=" +
            "(description=(address=(protocol=tcps)(port=1522)" +
            "(host=adb.ap-mumbai-1.oraclecloud.com))" +
            "(connect_data=(service_name=g052c5865a130cd_oracledb2_high.adb.oraclecloud.com))" +
            ")";

        static OracleDBConnection()
        {
            //OracleConfiguration.TnsAdmin = @"C:\Users\ADMIN\OneDrive\Desktop\Project\CollegeProject\src\resources\Wallet_OracleDB2";
            OracleConfiguration.TnsAdmin = @"/Users/Neel_Esh/Downloads/Wallet_OracleDB2";
            OracleConfiguration.WalletLocation = OracleConfiguration.TnsAdmin;
            con = new OracleConnection(connectionString);
            cmd = con.CreateCommand();
        }

        public static OracleConnection getConnection()
        {
            return con;
        }

        public static OracleCommand getCommand()
        {
            return cmd;
        }
    } 
}