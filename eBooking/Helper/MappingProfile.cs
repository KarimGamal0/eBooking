using AutoMapper;
using eBooking.Dtos;
using eBooking.Models;

namespace eBooking.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Hotel, HotelDetailsDto>();
            CreateMap<HotelDto, Hotel>();
                //.ForMember(src => src.Image, opt => opt.Ignore());
            CreateMap<Room, RoomDetailsDto>();
            // .ForMember(src => src.HotelName, opt => opt.Ignore());
            CreateMap<RoomDto, Room>()
            .ForMember(src => src.HotelId, opt => opt.Ignore());
            CreateMap<Client, ClientDetailsDto>();
            CreateMap<ClientDto, Client>();
        }
    }
}
