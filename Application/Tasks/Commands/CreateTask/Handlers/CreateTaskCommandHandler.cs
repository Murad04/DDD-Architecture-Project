using Application.Tasks.Commands.CreateTask;
using AutoMapper;
using Domain.Common.Interfaces;
using Domain.Entities;
using Domain.Services.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tasks.Commands.Handlers
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, int>
    {
        private readonly IRepository<ToDoTask, int> _taskRepository;
        private readonly IRepository<Person, int> _personRepository;
        private readonly ITaskManager _taskManager;
        private readonly IMapper _mapper;

        public CreateTaskCommandHandler(IRepository<ToDoTask, int> taskRepository, IRepository<Person, int> personRepository, ITaskManager taskManager, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _personRepository = personRepository;
            _taskManager = taskManager;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = _mapper.Map<ToDoTask>(request);

            if(request.AssignedPersonID !=null)
            {
                var person = await _personRepository.GetFirst(request.AssignedPersonID.Value);
                await _taskManager.AssignToPerson(task, person);
            }

            await _taskRepository.Add(task);

            await _taskRepository.Commit(cancellationToken);

            return task.Id;
        }
    }
}
