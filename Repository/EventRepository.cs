using AutoMapper;
using EventDemoAPI.DBContext;
using EventDemoAPI.Interface;
using EventDemoAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace EventDemoAPI.Repository
{
    public class EventRepository : IEventRepository
    {
        protected DbEventContext _context;
        IMapper _mapper;
        public EventRepository(DbEventContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Events

        public async Task<List<EventDemo>> GetAllEvents()
        {

            var events = await _context.EventDemos
                                .AsNoTracking()
                             .ToListAsync();

            return events;
        }

        public async Task<List<EventView>> GetAllEventViews()
        {

            var events = await _context.EventViews
                                .AsNoTracking()
                             .ToListAsync();

            return events;
        }

        public async Task<EventDemo?> GetEventById(int eventId)
        {

            var eventDemo = await _context.EventDemos
                               .Where(x => x.eventId == eventId)
                                .AsNoTracking()
                             .FirstOrDefaultAsync();

            return eventDemo;
        }

        public async Task<EventView?> GetEventViewById(int eventId)
        {

            var eventView = await _context.EventViews
                               .Where(x => x.eventId == eventId)
                                .AsNoTracking()
                             .FirstOrDefaultAsync();

            return eventView;
        }

        #endregion Events

        #region users

        public async Task<List<User>> GetAllUsers()
        {

            var users = await _context.Users
                                .AsNoTracking()
                             .ToListAsync();

            return users;
        }

        public async Task<User?> GetUserById(int userId)
        {

            var users = await _context.Users.Where(x => x.userId == userId)
                                .AsNoTracking()
                             .FirstOrDefaultAsync();

            return users;
        }

        public async Task<User?> GetUserByname(string name)
        {

            var users = await _context.Users.Where(x => x.userName.ToLower() == name.ToLower())
                                .AsNoTracking()
                             .FirstOrDefaultAsync();

            return users;
        }

        #endregion users

        #region Booking

        public async Task<EventBooking?> GetEventBooking(int eventId, int userId)
        {
            EventBooking? booking = await _context.EventBookings
                                       .Where(x => x.eventId == eventId && x.userId == userId)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync();

            return booking;
        }
        public async Task<GeneralResult<Booking>> AddBooking(Booking bookEvent)
        {
            GeneralResult<Booking> result = new GeneralResult<Booking>();
            EventBooking? booking = _mapper.Map<EventBooking>(bookEvent);
            if (booking != null)
            {
                User newUser = new User();
                newUser.userName = bookEvent.userName;
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
                int userId = newUser.userId;
                booking.userId = userId;
                await _context.EventBookings.AddAsync(booking!);
                await _context.SaveChangesAsync();
                result.ok = "yes";
                result.error = "Successfully add booking.";
            }
            else
            {
                result.ok = "no";
                result.error = "Internal error. Mapping error";
            }

            return result;
        }

        #endregion Booking
    }
}
