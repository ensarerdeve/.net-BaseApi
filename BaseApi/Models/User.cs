using System.ComponentModel.DataAnnotations.Schema;

namespace BaseApi.Models
{
    [Table("Users")]
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; }
        public string Phone { get; set; }
        public decimal Age { get; set; }
    }
}
