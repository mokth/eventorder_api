using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EventDemoAPI.Model
{
    [Table("EventBooking")]
    public class EventBooking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public int eventId { get; set; }
        [Required]
        public int userId { get; set; }
        [Required]
        public DateTime? dateBook { get; set; }
    }

    public class Booking
    {
        [Required]
        public int eventId { get; set; }
        public int userId { get; set; }
        [Required]
        public string userName { get; set; }
        [Required]
        public DateTime? dateBook { get; set; }
    }
}
