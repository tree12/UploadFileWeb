using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadFileWeb.Entities.Data.Entities
{
    public abstract class BaseEntity<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
    {
        public DateTime? ModifyDate { get; set; }
        public DateTime? InsertDate { get; set; }
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
 
        }
    }
}
