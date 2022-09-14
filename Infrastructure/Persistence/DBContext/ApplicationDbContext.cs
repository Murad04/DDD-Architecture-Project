using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Domain.Common.DomainEvents;
using Domain.Common.Entities;
using Domain.Entities;
using Application.Common.Interfaces;
using Domain.Common.DomainEvents;
using Domain.Common.Entities.Interface;
using Domain.Common.Entities.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Persistence.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        private static readonly MethodInfo ConfigureGlobalFiltersMethodInfo = typeof(ApplicationDbContext)
                                                                                 .GetMethod(nameof(ConfigureGlobalFilters), BindingFlags.Instance | BindingFlags.NonPublic);

        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;

        public ApplicationDbContext(DbContextOptions options, ICurrentUserService currentUserService, IMediator mediator) : base(options)
        {
            _currentUserService = currentUserService;
            _mediator = mediator;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<IAudited>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        SetCreationAuditProperties(entry);
                        break;
                    case EntityState.Modified:
                        SetModificationAuditProperties(entry);
                        break;
                    case EntityState.Deleted:
                        CancelDeletionForSoftDelete(entry);
                        SetDeletionAuditProperties(entry);
                        break;
                }
            }

            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // ignore events if no dispatcher provided
            if (_mediator == null) return result;

            // dispatch events only if save was successful
            var entitiesWithEvents = ChangeTracker.Entries<HaveDomainEvents>()
                .Select(e => e.Entity)
                .Where(e => e.Events.Any())
                .ToArray();

            foreach (var entity in entitiesWithEvents)
            {
                var events = entity.Events.ToArray();
                entity.Events.Clear();
                foreach (var domainEvent in events)
                {
                    await _mediator.Publish(domainEvent, cancellationToken).ConfigureAwait(false);
                }
            }

            return result;
        }

        public DbSet<ToDoTask> ToDoTasks { get; set; } = null!;

        public DbSet<Person> Persons { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                ConfigureGlobalFiltersMethodInfo
                    .MakeGenericMethod(entityType.ClrType)
                    .Invoke(this, new object[] { modelBuilder, entityType });
            }
        }

        #region Configure Global Filters

        protected void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder, IMutableEntityType entityType) where TEntity : class
        {
            if (ShouldFilterEntity<TEntity>(entityType))
            {
                var filterExpression = CreateFilterExpression<TEntity>();
                if (filterExpression != null)
                {
                    modelBuilder.Entity<TEntity>().HasQueryFilter(filterExpression);
                }
            }
        }

        protected virtual bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType) where TEntity : class
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                return true;
            }

            return false;
        }

        protected virtual Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>() where TEntity : class
        {
            Expression<Func<TEntity, bool>> expression = null;

            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> softDeleteFilter = e => !((ISoftDelete)e).IsDeleted;
                expression = softDeleteFilter;
            }

            return expression;
        }

        #endregion

        #region Configure Audit Properties

        protected virtual void SetCreationAuditProperties(EntityEntry entry)
        {
            if (!(entry.Entity is IHasCreationTime hasCreationTimeEntity)) return;

            if (hasCreationTimeEntity.CreateTime == default)
            {
                hasCreationTimeEntity.CreateTime = DateTime.Now;
            }

            if (!(entry.Entity is ICreationAudited creationAuditedEntity)) return;

            if (creationAuditedEntity.CreatedUserID != null)
            {
                //CreatedUserId is already set
                return;
            }

            creationAuditedEntity.CreatedUserID = _currentUserService.UserID;
        }

        protected virtual void SetModificationAuditProperties(EntityEntry entry)
        {
            if (!(entry.Entity is IHasModificationTime hasModificationTimeEntity)) return;

            if (hasModificationTimeEntity.LastModifiedTime == default)
            {
                hasModificationTimeEntity.LastModifiedTime = DateTime.Now;
            }

            if (!(entry.Entity is IModificationAudited modificationAuditedEntity)) return;

            if (modificationAuditedEntity.LastModifiedUserID != null)
            {
                //LastModifiedUserId is already set
                return;
            }

            modificationAuditedEntity.LastModifiedUserID = _currentUserService.UserID;
        }

        protected virtual void SetDeletionAuditProperties(EntityEntry entry)
        {

            if (!(entry.Entity is IHasDeletionTime hasDeletionTimeEntity)) return;

            if (hasDeletionTimeEntity.DeletedDate == default)
            {
                hasDeletionTimeEntity.DeletedDate = DateTime.Now;
            }

            if (!(entry.Entity is IDeletionAudited deletionAuditedEntity)) return;

            deletionAuditedEntity.DeletedUserID = _currentUserService.UserID;
            deletionAuditedEntity.DeletedDate = DateTime.Now;
        }

        protected virtual void CancelDeletionForSoftDelete(EntityEntry entry)
        {
            if (!(entry.Entity is ISoftDelete))
            {
                return;
            }

            entry.Reload();
            entry.State = EntityState.Modified;
            ((ISoftDelete)entry.Entity).IsDeleted = true;
        }

        #endregion
    }
}