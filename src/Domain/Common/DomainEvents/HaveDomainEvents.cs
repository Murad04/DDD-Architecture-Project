using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.DomainEvents
{
    public abstract class HaveDomainEvents
    {
        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
    }
}
