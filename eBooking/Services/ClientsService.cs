using eBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace eBooking.Services
{
    public class ClientsService : IClientsService
    {
        private readonly ApplicationDbContext _context;

        public ClientsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Client> Add(Client client)
        {
            _context.AddAsync(client);
            _context.SaveChanges();

            return client;
        }

        public Client Delete(Client client)
        {
            _context.Remove(client);
            _context.SaveChanges();
            return client;
        }



        public async Task<IEnumerable<Client>> GetAll()
        {
            var clients = await _context.Users.ToListAsync();

            return clients;
            //throw new NotImplementedException();
        }

        public async Task<Client> GetById(int id)
        {
            var client = await _context.Users.FindAsync(id);
            return client;
            //throw new NotImplementedException();
        }

        public Client Update(Client client)
        {
            _context.Update(client);
            _context.SaveChanges();
            return client;
        }
    }
}
