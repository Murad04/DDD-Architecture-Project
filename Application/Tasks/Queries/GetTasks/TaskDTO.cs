using Application.Common.Mappings;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tasks.Queries.GetTasks
{
    public class TaskDTO:IMapFrom<ToDoTask>
    {
        public int ID { get; set; }

        public string? Name { get; set; }

        public TaskPriority? Priority { get; set; }

        public TaskState? State { get; set; }
    }
}
