using Diplom.DTO.TicketDtos;
using Diplom.Entities;

namespace Diplom.Repositories.Interface
{
    public interface ITicketRepo
    {
        Task<IEnumerable<Ticket>> GetAll(string userId);
        Task<Ticket> GetById(Guid id,string userId);
        Task<Ticket> Create(TicketCreateDto ticket);
        Task<IEnumerable<Ticket>> GetAllAdmin(string userName);
        Task<Ticket> GetForAdmin(Guid id);
        Task Delete(Ticket route);
    }
}
