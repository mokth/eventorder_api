using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventDemoAPI.Model
{
    [Table("EventDemo")]
    public class EventDemo
    {
        [Key]
        public int eventId { get; set; }
        public string eventName { get; set; }
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
        public int? maxSeats { get; set; }

    }

    public class EventView
    {

        public int eventId { get; set; }
        public string eventName { get; set; }
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
        public int? maxSeats { get; set; }
        public int? seatBook { get; set; }

    }
}
