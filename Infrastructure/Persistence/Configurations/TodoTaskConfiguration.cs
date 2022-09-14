using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class TodoTaskConfiguration : IEntityTypeConfiguration<ToDoTask>
    {
        public void Configure(EntityTypeBuilder<ToDoTask> builder)
        {
            builder.HasOne(t => t.Person).WithMany(p => p.Tasks).HasForeignKey(t => t.AssignedPersonId);

            builder.Property(task => task.Name)
                .HasMaxLength(30)
                .IsRequired();
        }
    }
}
