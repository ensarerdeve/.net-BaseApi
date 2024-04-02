namespace BaseApi.Models
{
    public class UserActivity
    {
        public int userId { get; set; }
        public int Id { get; set; }
        public List<Event> Events { get; set; }
    }

    public class Event
    {
        public string Username { get; set; }
        public DateTime Timestamp { get; set; }
        public Action? Action { get; set; }
        public string Details { get; set; }
    }

    public enum Action
    {
        Login,
        Logout,
        UsernameChanged
    }
}
