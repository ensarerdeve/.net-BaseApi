using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseApi.Models
{
    [Table("Follow")]
    public class Follow
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string followedUsername { get; set; }
        [Required]
        public string user { get; set;}
    }
}
