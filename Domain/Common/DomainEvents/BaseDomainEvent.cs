using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.DomainEvents
{
    public abstract class BaseDomainEvent:INotification
    {
        public DateTime DateOccured { get; protected set; } = DateTime.UtcNow;
    }
}
