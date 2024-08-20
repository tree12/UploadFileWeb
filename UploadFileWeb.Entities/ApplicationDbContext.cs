using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadFileWeb.Entities.Data.Entities;

namespace UploadFileWeb.Entities
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Transaction).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FileLog).Assembly);
        }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<FileLog> FileLogs { get; set; }
    }
}
