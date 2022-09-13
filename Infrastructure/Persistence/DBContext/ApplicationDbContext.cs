using Application.Common.Interfaces;
using Domain.Common.DomainEvents;
using Domain.Common.Entities.Interfaces;
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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach(var entry in ChangeTracker.Entries<IAudited>())
            {
                switch(entry.State)
                {
                    case EntityState.Deleted:
                        break;
                    case EntityState.Added:
                        break;
                    case EntityState.Modified:
                        break;
                }
            }

            int result=await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // ignore events if no dispatcher provided
            if (_mediator == null) return result;

            // dispatch events only if save was successful
            var entitiesWithEvents = ChangeTracker.Entries<HaveDomainEvents>()
                .Select(e => e.Entity)
                .Where(e => e.Events.Any())
                .ToArray();

            foreach(var entity in entitiesWithEvents)
            {
                var events = entity.Events.ToArray();
                entity.Events.Clear();
                foreach(var domainEvent in events)
                {
                    await _mediator.Publish(domainEvent,cancellationToken).ConfigureAwait(false);
                }
            }

            return result;
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
