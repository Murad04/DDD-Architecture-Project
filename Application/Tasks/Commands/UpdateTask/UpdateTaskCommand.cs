using Application.Common.Mappings;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommand:IRequest,IMapTo<ToDoTask>
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public TaskPriority Priority { get; set; }
    }
}
