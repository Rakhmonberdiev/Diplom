using Diplom.DTO.TicketDtos;
using Diplom.Entities;

namespace Diplom.Repositories.Interface
{
    public interface ITicketRepo
    {
        Task<Ticket> GetById(Guid id,string userId);
        Task<Ticket> Create(TicketCreateDto ticket);
    }
}
