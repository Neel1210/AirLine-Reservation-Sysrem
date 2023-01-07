namespace airLineReservationSystem.Models
{
    public class User
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }

        public User()
        {
            UserId = "";
            Name = "";
            Mobile = "";
            Password = "";
            Type = "";
        }
    }
}
