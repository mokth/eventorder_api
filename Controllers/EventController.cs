using EventDemoAPI.Interface;
using EventDemoAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace EventDemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : Controller
    {
        private IEventBookingHelper _helper;
        private ILogger<EventController> _logger;
        public EventController(IEventBookingHelper helper, ILogger<EventController> logger)
        {
            _helper = helper;
            _logger = logger;
        }

        /// <summary>
        /// Get all events 
        /// </summary>
        /// <returns>json</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var result = await _helper.GetAllEvents();
            return Json(new
            {
                ok = result.ok,
                data = result.result,
                error = result.error
            });
        }

        /// <summary>
        /// Get all events - include seats ordered
        /// </summary>
        /// <returns>json</returns>
        [HttpGet, Route("events")]
        public async Task<IActionResult> GetAllEventViews()
        {
            var result = await _helper.GetAllEventViews();
            return Json(new
            {
                ok = result.ok,
                data = result.result,
                error = result.error
            });
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>json</returns>
        [HttpGet, Route("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _helper.GetAllUsers();
            return Json(new
            {
                ok = result.ok,
                data = result.result,
                error = result.error
            });
        }

        /// <summary>
        /// Post event order (booking)
        /// </summary>
        /// <returns>json</returns>
        [HttpPost, Route("booking")]
        public async Task<IActionResult> AddEventBooking([FromBody] Booking eventBooking)
        {
            if (ModelState.IsValid)
            {
                var result = await _helper.AddBooking(eventBooking);
                return Json(new
                {
                    ok = result.ok,
                    data = result.result,
                    error = result.error
                });
            }

            return Json(new
            {
                ok = "no",
                data = "",
                error = "Invalid event booking detail"
            });
        }
    }
}
