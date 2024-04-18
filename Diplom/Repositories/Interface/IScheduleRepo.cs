using Diplom.Entities;

namespace Diplom.Repositories.Interface
{
    public interface IScheduleRepo
    {
        Task<IEnumerable<Schedule>> GetAll();
        Task<Schedule> GetById(Guid id);
        Task Create(Schedule schedule);
        Task Update(Schedule schedule);
        Task Delete(Schedule schedule);
        Task SaveAsync();
    }
}
