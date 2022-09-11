using Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EventHandlers
{
    public class TaskCompletedEmailNotificationHandler:INotificationHandler<TaskCompletedEvent>
    {
        public Task Handle(TaskCompletedEvent notification,CancellationToken cancellationToken)
        {
            if(notification==null)
            {
                throw new ArgumentNullException(nameof(notification));
            }
            return Task.CompletedTask;
        }
    }
}
