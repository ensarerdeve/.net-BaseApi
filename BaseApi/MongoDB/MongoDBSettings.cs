using System.ComponentModel.DataAnnotations;

namespace BaseApi.MongoDB
{
    public class MongoDBSettings
    {
        [Required]
        public string ConnectionString { get; set; }
        [Required]
        public string DatabaseName { get; set; }
        [Required]
        public string UsersCollectionName { get; set; }
    }
}
