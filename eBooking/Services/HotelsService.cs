using eBooking.Dtos;
using eBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace eBooking.Services
{
    public class HotelsService : IHotelsService
    {
        private readonly ApplicationDbContext _context;
        public HotelsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Hotel> Add(Hotel hotel)
        {
            _context.AddAsync(hotel);
            _context.SaveChanges();

            return hotel;
        }

        public Hotel Delete(Hotel hotel)
        {
            _context.Remove(hotel);
            _context.SaveChanges();
            return hotel;
        }

        public async Task<IEnumerable<Hotel>> GetAll()
        {
            var hotels =  await _context.Hotels.OrderBy(h => h.Name).ToListAsync();

            return hotels;
        }

        public async Task<Hotel> GetById(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            return hotel;
        }

        public Hotel Update(Hotel hotel)
        {
            _context.Update(hotel);
            _context.SaveChanges();
            return hotel;
        }
    }
}
