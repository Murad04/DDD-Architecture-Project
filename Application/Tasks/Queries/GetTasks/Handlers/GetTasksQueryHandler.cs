using Application.Tasks.Queries.GetTasks;
using Application.Tasks.Queries.GetTasks.ViewModel;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tasks.Queries.GetTasks.Handlers
{
    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, TaskView>
    {
        private readonly IRepository<ToDoTask, int> _taskRepository;
        private readonly IMapper _mapper;

        public GetTasksQueryHandler(IRepository<ToDoTask, int> taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<TaskView> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            var vm = new TaskView
            {
                Tasks = await _taskRepository.GetAll()
                    .Where(t => t.Name.Contains(request.Name))
                    .ProjectTo<TaskDTO>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Name)
                    .ToListAsync(cancellationToken)
            };
            return vm;
        }
    }
}
