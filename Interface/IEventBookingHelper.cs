using EventDemoAPI.Model;

namespace EventDemoAPI.Interface
{
    public interface IEventBookingHelper
    {
        public Task<GeneralResult<Booking>> AddBooking(Booking bookEvent);
        public Task<GeneralResult<List<EventDemo>>> GetAllEvents();
        public Task<GeneralResult<List<EventView>>> GetAllEventViews();
        public Task<GeneralResult<List<User>>> GetAllUsers();
        public Task<GeneralResult<User>> GetUserById(int userId);
    }
}