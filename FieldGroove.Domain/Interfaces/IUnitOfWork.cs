namespace FieldGroove.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public ILeadsRepository LeadsRepository { get; }
    }
}
