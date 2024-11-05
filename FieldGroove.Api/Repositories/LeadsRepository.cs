using FieldGroove.Api.Data;
using FieldGroove.Api.Interfaces;
using FieldGroove.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FieldGroove.Api.Repositories
{
    public class LeadsRepository(ApplicationDbContext dbContext) : ILeadsRepository
    {
        public async Task<bool> Create(LeadsModel leads)
        {
            try
            {
                await dbContext.Leads.AddAsync(leads);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task Delete(LeadsModel lead)
        {
            dbContext.Leads.Remove(lead);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<LeadsModel>> GetAll()
        {
            return await dbContext.Leads.ToListAsync();
        }

        public async Task<LeadsModel> GetById(int id)
        {
            var response = await dbContext.Leads.FindAsync(id);
            return response!;
        }

        public async Task<bool> isAny(int id)
        {
            return await dbContext.Leads.AnyAsync(x => x.Id == id);
        }

        public async Task Update(LeadsModel leads)
        {
            dbContext.Leads.Update(leads);
            await dbContext.SaveChangesAsync();
        }
    }
}
