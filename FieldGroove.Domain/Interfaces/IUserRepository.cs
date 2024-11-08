﻿using FieldGroove.Domain.Models;

namespace FieldGroove.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> Create(RegisterModel entity);
        Task<bool> IsValid(LoginModel enitity);
        Task<bool> IsRegistered(RegisterModel enitity);

    }
}
