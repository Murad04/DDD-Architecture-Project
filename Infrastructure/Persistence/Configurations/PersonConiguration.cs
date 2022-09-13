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
    public class PersonConiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.Property(person => person.Name)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(person => person.Age)
                .IsRequired();

            builder.Property(person => person.SurName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(person => person.Address)
                .IsRequired();
        }
    }
}
