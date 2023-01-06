using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventDemoAPI.Model
{
    [Table("User")]
    public class User
    {
        [Key]
        public int userId { get; set; }
        public string userName { get; set; }
    }
}
