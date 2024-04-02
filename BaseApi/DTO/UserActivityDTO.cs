namespace BaseApi.DTO
{
        public class UserActivityDTO
        {
            public int userId { get; set; }
            public List<EventDTO> Events { get; set; }
        }
        public class EventDTO
        {
            public string Username { get; set; }
            public DateTime Timestamp { get; set; }
            public string Action { get; set; }
        }
        public enum ActionDTO
        {
            Login,
            Logout,
            UsernameChanged
        }
}

