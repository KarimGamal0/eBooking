using eBooking.Models;

namespace eBooking.Services
{
    public interface IClientsService
    {
        Task<IEnumerable<Client>> GetAll();

        Task<Client> GetById(int id);

        Task<Client> Add(Client client);

        Client Update(Client client);

        Client Delete(Client client);
    }
}
