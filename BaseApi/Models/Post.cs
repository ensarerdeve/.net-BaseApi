using System.ComponentModel.DataAnnotations.Schema;

namespace BaseApi.Models
{
    [Table("Posts")]
    public class Post : IModel
    {
        public Guid Id {  get; set; }
        public string content { get; set; }
        public string title { get; set; }
        public DateTime createdAt { get; set; }
        public string userName { get; set; }
    }
}
