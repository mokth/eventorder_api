using EventDemoAPI.Model;

namespace EventDemoAPI.Interface
{
    public interface IEventRepository
    {
        #region event
        public Task<List<EventDemo>> GetAllEvents();
        public Task<List<EventView>> GetAllEventViews();
        public Task<EventDemo?> GetEventById(int eventId);
        public Task<EventView?> GetEventViewById(int eventId);
        #endregion event

        #region user
        public Task<List<User>> GetAllUsers();
        public Task<User?> GetUserById(int userId);
        public Task<User?> GetUserByname(string name);
        #endregion user

        #region Booking
        public Task<EventBooking?> GetEventBooking(int eventId, int userId);
        public Task<GeneralResult<Booking>> AddBooking(Booking bookEvent);
        #endregion Booking
    }
}