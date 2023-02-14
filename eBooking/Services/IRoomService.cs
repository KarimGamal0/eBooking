using eBooking.Models;

namespace eBooking.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAll(int HotelId = 0);

        Task<IEnumerable<Room>> GetAllRoomsByHotelId(int hotelId);

        Task<IEnumerable<Room>> GetAllRoomsByClientId(int clientId);

        Task<Room> GetById(int id);

        Task<Room> Add(Room room);

        Room Update(Room room);

        Room Delete(Room room);
    }
}
