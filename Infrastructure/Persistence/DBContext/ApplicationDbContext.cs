using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.DBContext
{
    public class ApplicationDbContext:DbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;

        public ApplicationDbContext(ICurrentUserService currentUserService, IMediator mediator,DbContextOptions options):base(options)
        {
            _currentUserService = currentUserService;
            _mediator = mediator;
        }

        public DbSet<Person> Persons { get; set; } = null!;
        public DbSet<ToDoTask> Tasks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

    }
}
