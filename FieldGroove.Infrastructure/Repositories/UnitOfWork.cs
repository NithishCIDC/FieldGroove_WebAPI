using FieldGroove.Infrastructure.Data;
using FieldGroove.Domain.Interfaces;

namespace FieldGroove.Infrastructure.Repositories
{
    public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
    {
        public IUserRepository UserRepository { get; private set; } = new UserRepository(dbContext);

        public ILeadsRepository LeadsRepository { get; private set; } = new LeadsRepository(dbContext);
    }
}
