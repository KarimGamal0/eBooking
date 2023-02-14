using eBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace eBooking.Services
{
    public class RoomsService : IRoomService
    {
        private readonly ApplicationDbContext _context;
        public RoomsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Room> Add(Room room)
        {
            _context.AddAsync(room);
            _context.SaveChanges();

            return room;
        }

        public Room Delete(Room room)
        {
            _context.Remove(room);
            _context.SaveChanges();
            return room;
        }

        public async Task<IEnumerable<Room>> GetAll(int HotelId = 0)
        {
            var rooms = await _context.Rooms
                .Include(r => r.Hotel)
                .Where(n => n.HotelId == HotelId || HotelId == 0)
                .ToListAsync();
            return rooms;
        }

        //Check is Working
        public async Task<IEnumerable<Room>> GetAllRoomsByClientId(int clientId)
        {
            var rooms = await _context.Rooms
             .Where(n => n.ClientId == clientId)
             .Include(n => n.Client)
             .ToListAsync();

            return rooms;
        }

        public async Task<IEnumerable<Room>> GetAllRoomsByHotelId(int hotelId)
        {
            var rooms = await _context.Rooms
             .Where(n => n.HotelId == hotelId)
             .Include(n => n.Hotel)
             .ToListAsync();

            return rooms;
        }

        public async Task<Room> GetById(int id)
        {
            var room = await _context.Rooms
                .Include(r => r.Hotel)
                .SingleOrDefaultAsync(r => r.Id == id);

            return room;
        }

        public Room Update(Room room)
        {
            _context.Update(room);
            _context.SaveChanges();
            return room;
        }
    }
}
