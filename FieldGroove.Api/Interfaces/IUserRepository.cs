using FieldGroove.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace FieldGroove.Api.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> Create(RegisterModel entity);
        Task<bool> IsValid(LoginModel enitity);
        Task<bool> IsRegistered(RegisterModel enitity);

    }
}
