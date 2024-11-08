﻿using FieldGroove.Infrastructure.Data;
using FieldGroove.Domain.Models;
using Microsoft.EntityFrameworkCore;
using FieldGroove.Domain.Interfaces;

namespace FieldGroove.Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
    {
        public async Task<bool> Create(RegisterModel entity)
        {
            try
            {
                await dbContext.UserData.AddAsync(entity);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex) 
            { 
               return false;
            }
            
        }

        public async Task<bool> IsRegistered(RegisterModel entity)
        {
            try
            {
                return await dbContext.UserData.AsQueryable().AnyAsync(x => x.Email == entity.Email!);
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public async Task<bool> IsValid(LoginModel entity)
        {
            try
            {
                return await dbContext.UserData.AsQueryable().AnyAsync(x => x.Email == entity.Email!);
            }
            catch (Exception ex)
            {
                return false;
            }
           
        }
    }
}
