using Domain.Common.Exceptions;
using Domain.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class TaskMeneger : ITaskManager
    {
        public const int MaxActiveTaskCountForAPerson = 3;

        private readonly IRepository<ToDoTask, int> _taskRepository;

        public TaskMeneger(IRepository<ToDoTask, int> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task AssignToPerson(ToDoTask task, Person person)
        {
            if (task.AssignedPersonId == person.Id)
            {
                return;
            }

            if(task.State!=TaskState.Active)
            {
                throw new ApplicationException("Can not assign a task to a person when task is not active");
            }

            if(await HasPersonMaximumAssignedTask(person))
            {
                throw new UserFriendlyExceptions($"{person.Name} already has at most {MaxActiveTaskCountForAPerson} active tasks");
            }
        }

        private async Task<bool> HasPersonMaximumAssignedTask(Person person)
        {
            var assignedTaskCount=await _taskRepository.Count(t=>t.State==TaskState.Active && t.AssignedPersonId==person.Id);
            return assignedTaskCount>= MaxActiveTaskCountForAPerson;
        }
    }
}
