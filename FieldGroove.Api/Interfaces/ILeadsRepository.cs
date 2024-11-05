﻿using FieldGroove.Api.Models;

namespace FieldGroove.Api.Interfaces
{
    public interface ILeadsRepository
    {
        Task<List<LeadsModel>> GetAll();
        Task<LeadsModel> GetById(int id);
        Task<bool> Create(LeadsModel leads);
        Task Update(LeadsModel leads);
        Task Delete(LeadsModel leads);
        Task<bool> isAny(int id);
    }
}
