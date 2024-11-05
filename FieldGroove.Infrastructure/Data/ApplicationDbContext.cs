using FieldGroove.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FieldGroove.Infrastructure.Data
{
    //DbContext for Database Connection and create DB Table
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<RegisterModel> UserData { get; set; }

        public DbSet<LeadsModel> Leads { get; set; }
    }
}
