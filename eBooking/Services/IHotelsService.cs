using eBooking.Dtos;
using eBooking.Models;

namespace eBooking.Services
{
    public interface IHotelsService
    {
        Task<IEnumerable<Hotel>> GetAll();

        Task<Hotel> GetById(int id);

        Task<Hotel> Add(Hotel hotel);

        Hotel Update(Hotel hotel);

        Hotel Delete(Hotel hotel);
    }
}
