using Domain.Common.DomainEvents;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class TaskCompletedEvent:BaseDomainEvent
    {
        public ToDoTask CompletedTask { get; set; }

        public TaskCompletedEvent(ToDoTask completedTask)
        {
            CompletedTask = completedTask;
        }
    }
}
