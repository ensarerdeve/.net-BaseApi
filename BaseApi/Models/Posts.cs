﻿namespace BaseApi.Models
{
    public class Posts
    {
        public Guid Id {  get; set; }
        public string content { get; set; }
        public string title { get; set; }
        public DateTime createdAt { get; set; }
        public string userName { get; set; }
    }
}
