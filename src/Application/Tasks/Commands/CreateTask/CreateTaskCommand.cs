using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tasks.Commands.CreateTask
{
    public class CreateTaskCommand:IRequest<int>
    {
        public string? Name { get; set; }

        public bool IsCompleted { get; set; }

        public TaskPriority Priority { get; set; }

        public TaskState State { get; set; }

        public int? AssignedPersonID { get; set; }
    }
}
