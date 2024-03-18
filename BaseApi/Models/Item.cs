using System.ComponentModel.DataAnnotations.Schema;

namespace BaseApi.Models
{
    [Table("Items")]
    public class Item
    {
        public Guid Id { get; set; }
        public string itemName { get; set; }
        public string itemPrice { get; set; }
        public DateTime createdAt { get; set; }
        public string itemOwner { get; set; }
    }
}
