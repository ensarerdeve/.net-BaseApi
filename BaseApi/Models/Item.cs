using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseApi.Models
{
    [Table("Items")]
    public class Item : IModel
    {
        public Guid Id { get; set; }
        [Required]
        public string itemName { get; set; }
        [Required]
        public string itemPrice { get; set; }
        [Required]
        public DateTime createdAt { get; set; }
        [Required]
        public string itemOwner { get; set; }
    }
}
