using Application.Tasks.Queries.Enums;
using Application.Tasks.Queries.GetTasksQuery;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Tasks.Queries.ViewModel
{
    public class TaskView
    {
        public IList<EnumValueDTO> TaskPriorities { get; } =
            Enum.GetValues(typeof(TaskPriority))
            .Cast<TaskPriority>()
            .Select(p => new EnumValueDTO { Value = (int)p, Name = p.ToString() })
            .ToList();



        public IList<EnumValueDTO> TaskStates { get; } =
            Enum.GetValues(typeof(TaskState))
            .Cast<TaskState>()
            .Select(p => new EnumValueDTO { Value = (int)p, Name = p.ToString() })
            .ToList();

        public IList<TaskDTO>? Tasks { get; set; }
    }
}
