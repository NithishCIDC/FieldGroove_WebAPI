using FieldGroove.Infrastructure.Data;
using FieldGroove.Api.Interfaces;

namespace FieldGroove.Api.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;

            UserRepository = new UserRepository(dbContext);

            LeadsRepository = new LeadsRepository(dbContext);

        }
        public IUserRepository UserRepository { get; private set; }

        public ILeadsRepository LeadsRepository { get; private set; }
    }
}
