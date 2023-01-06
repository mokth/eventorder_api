using EventDemoAPI.Interface;
using EventDemoAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace EventDemoAPI.Helper
{
    public class EventBookingHelper : IEventBookingHelper
    {
        IEventRepository _eventRepository;
        private ILogger<EventBookingHelper> _logger;
        public EventBookingHelper(IEventRepository eventRepository, ILogger<EventBookingHelper> logger)
        {
            _eventRepository = eventRepository;
            _logger = logger;
        }

        #region Events

        public async Task<GeneralResult<List<EventDemo>>> GetAllEvents()
        {
            GeneralResult<List<EventDemo>> result = new GeneralResult<List<EventDemo>>();
            try
            {
                var list = await _eventRepository.GetAllEvents();
                if (list != null && list.Count > 0)
                {

                    result.ok = "yes";
                    result.result = list;
                }
                else
                {
                    result.ok = "no";
                    result.error = "No events found...";
                }
            }
            catch (Exception ex)
            {
                result.ok = "no";
                result.error = "Internal Server error...";
                _logger.LogError(ex.Message, ex);
            }
            return result;
        }

        public async Task<GeneralResult<List<EventView>>> GetAllEventViews()
        {
            GeneralResult<List<EventView>> result = new GeneralResult<List<EventView>>();
            try
            {
                var list = await _eventRepository.GetAllEventViews();
                if (list != null && list.Count > 0)
                {

                    result.ok = "yes";
                    result.result = list;
                }
                else
                {
                    result.ok = "no";
                    result.error = "No events found...";
                }
            }
            catch (Exception ex)
            {
                result.ok = "no";
                result.error = "Internal Server error...";
                _logger.LogError(ex.Message, ex);
            }
            return result;
        }

        #endregion Events

        #region users

        public async Task<GeneralResult<List<User>>> GetAllUsers()
        {
            GeneralResult<List<User>> result = new GeneralResult<List<User>>();
            try
            {
                var list = await _eventRepository.GetAllUsers();
                if (list != null && list.Count > 0)
                {

                    result.ok = "yes";
                    result.result = list;
                }
                else
                {
                    result.ok = "no";
                    result.error = "No user found...";
                }
            }
            catch (Exception ex)
            {
                result.ok = "no";
                result.error = "Internal Server error...";
                _logger.LogError(ex.Message, ex);
            }
            return result;

        }
        public async Task<GeneralResult<User>> GetUserById(int userId)
        {
            //return await GetUserById(userId);
            GeneralResult<User> result = new GeneralResult<User>();
            try
            {
                result = await GetUserById(userId);

            }
            catch (Exception ex)
            {
                result.ok = "no";
                result.error = "Internal Server error...";
                _logger.LogError(ex.Message, ex);
            }
            return result;
        }

        #endregion users

        #region Booking

        public async Task<GeneralResult<Booking>> AddBooking(Booking bookEvent)
        {
            GeneralResult<Booking> result = new GeneralResult<Booking>();
            var user = await _eventRepository.GetUserByname(bookEvent.userName);
            //if (user == null)
            //{
            //    result.error = "Invalid user Id " + bookEvent.userId.ToString();
            //    return result;
            //}

            var eventDemo = await _eventRepository.GetEventViewById(bookEvent.eventId);
            if (eventDemo == null)
            {
                result.error = "Invalid Event Id " + bookEvent.eventId.ToString();
                return result;
            }
            if (eventDemo.maxSeats != null)
            {
                if (eventDemo.seatBook >= eventDemo.maxSeats)
                {
                    result.error = "Sorry, This event was fully booked.";
                    return result;
                }
            }

            if (user != null)
            {
                var booking = await _eventRepository.GetEventBooking(bookEvent.eventId, bookEvent.userId);
                if (booking != null)
                {
                    result.error = "You have booked this event before. Please book other event.";
                    return result;
                }
            }

            try
            {
                result = await _eventRepository.AddBooking(bookEvent);

            }
            catch (Exception ex)
            {
                result.ok = "no";
                result.error = "Internal Server error...";
                _logger.LogError(ex.Message, ex);
            }



            return result;
        }

        #endregion Booking
    }
}
