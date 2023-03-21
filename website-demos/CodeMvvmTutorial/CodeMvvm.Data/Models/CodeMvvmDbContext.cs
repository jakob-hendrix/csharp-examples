using CodeMvvm.Data.DefaultData;
using CodeMvvm.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeMvvm.Data.Models
{
    public partial class CodeMvvmDbContext : DbContext
    {
        public CodeMvvmDbContext(DbContextOptions<CodeMvvmDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new DbInitializer(modelBuilder).Seed();
        }

        public virtual DbSet<Condition> Conditions { get; set; }
    }
}
