using AutoMapper;
using EventDemoAPI.Model;

namespace EventDemoAPI.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Booking, EventBooking>();


        }
    }
}
